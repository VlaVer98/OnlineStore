using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.Client.Core.Exceptions;
using Shop.Client.Core.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Shop.Resources;
using System.Threading.Tasks;

namespace Shop.Client.Core
{
    class ShopApiClient
    {
        #region [ Private Fields ]

        private Uri _baseApiUri;
        private string _otac;
        private string _clientId;
        private string _clientSecret;
        private string _scope;
        private string _accessToken;
        private string _refreshToken;
        private string _username;
        private string _password;

        #endregion // [ Private Fields ]

        #region [ Constructors / Destructors ]

        public ShopApiClient(Uri baseApiUri, string clientId, string clientSecret)
        {
            _baseApiUri = baseApiUri;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scope = String.Empty;
        }

        public ShopApiClient(string baseApiUri, string clientId, string clientSecret)
            : this(new Uri(baseApiUri), clientId, clientSecret) { }

        public ShopApiClient(Uri baseApiUri, string clientId, string clientSecret, string email, string password)
            : this(baseApiUri, clientId, clientSecret)
        {
            _username = email;
            _password = password;
        }

        public ShopApiClient(string baseApiUri, string clientId, string clientSecret, string email, string password)
            : this(new Uri(baseApiUri), clientId, clientSecret, email, password) { }

        public ShopApiClient(Uri baseApiUri, string clientId, string clientSecret, string otac)
            : this(baseApiUri, clientId, clientSecret)
        {
            _otac = otac;
        }

        public ShopApiClient(string baseApiUri, string clientId, string clientSecret, string otac)
            : this(new Uri(baseApiUri), clientId, clientSecret, otac) { }

        #endregion // [ Constructors / Destructors ]

        #region [ Private Methods ]

        private async Task AccessTokenAsync()
        {
            FormUrlEncodedContent formContent = null;

            if (!String.IsNullOrEmpty(_otac))
            {
                formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "otac"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("otac", _otac),
                });
            }
            else if (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_password))
            {
                formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("username", _username),
                    new KeyValuePair<string, string>("password", _password)
                });
            }
            else
            {
                formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("scope", _scope),
                });
            }

            await TokenRequestAsync(formContent);
        }

        private async Task RefreshAccessTokenAsync()
        {
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("scope", _scope),
                    new KeyValuePair<string, string>("refresh_token", _refreshToken)
            });

            await TokenRequestAsync(formContent);
        }

        private async Task TokenRequestAsync(FormUrlEncodedContent formContent)
        {
            using (HttpClient httpClient = GetUnauthorizedHttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync("oauth/token", formContent);
                string responseJson = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(responseJson);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new AccessTokenException(jObject.GetValue("error").ToString());
                }

                _accessToken = jObject.GetValue("access_token").ToString();
                _refreshToken = jObject.GetValue("refresh_token").ToString();
            }
        }

        private async Task<ShopApiResponse<T>> SendGetAsync<T>(string requestPath)
        {
            if (String.IsNullOrEmpty(_accessToken))
            {
                try
                {
                    await AccessTokenAsync();
                }
                catch (AccessTokenException)
                {
                    return new ShopApiResponse<T>
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = Messages.InvalidClient
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            using (HttpClient httpClient = GetAuthorizedHttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestPath);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshAccessTokenAsync();
                    return await SendGetAsync<T>(requestPath);
                }

                return await HandleHttpResponseMessageAsync<T>(response);
            }
        }

        private async Task<ShopApiResponse<T>> SendPostAsync<T>(string requestPath, HttpContent content)
        {
            if (String.IsNullOrEmpty(_accessToken))
            {
                try
                {
                    await AccessTokenAsync();
                }
                catch (AccessTokenException)
                {
                    return new ShopApiResponse<T>
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = Messages.InvalidClient
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            using (HttpClient httpClient = GetAuthorizedHttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(requestPath, content);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshAccessTokenAsync();
                    return await SendPostAsync<T>(requestPath, content);
                }

                return await HandleHttpResponseMessageAsync<T>(response);
            }
        }

        private async Task<ShopApiResponse<T>> HandleHttpResponseMessageAsync<T>(HttpResponseMessage response)
        {
            string responseJson = await response.Content.ReadAsStringAsync();

            ShopApiResponse<T> apiResponse = null;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                apiResponse = JsonConvert.DeserializeObject<ShopApiResponse<T>>(responseJson);
            }
            else
            {
                apiResponse = new ShopApiResponse<T>
                {
                    ResponseObject = JsonConvert.DeserializeObject<T>(responseJson)
                };
            }

            apiResponse.StatusCode = response.StatusCode;
            return apiResponse;
        }

        private HttpClient GetUnauthorizedHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = _baseApiUri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private HttpClient GetAuthorizedHttpClient()
        {
            HttpClient httpClient = GetUnauthorizedHttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            return httpClient;
        }

        private HttpContent CreateHttpContent(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        #endregion // [ Private Methods ]

        #region [ Public Methods ]

        
        

        #endregion // [ Public Methods ]
    }
}
