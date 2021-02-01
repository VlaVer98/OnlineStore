namespace Shop.Domain.Constants
{
    public class UserRoles
    {
        public const string Admin = "Admin";
        public const string Buyer = "Buyer";

        public static string[] GetAll()
        {
            return new string[] {
                Admin,
                Buyer,
            };
        }
    }
}
