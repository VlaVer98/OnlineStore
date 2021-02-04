using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Models;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.enums;
using Shop.Domain.Enums;
using Shop.Domain.Models.Dtos.Cart;
using Shop.Domain.Models.Dtos.Order;
using Shop.Domain.Models.Dtos.Product;
using Shop.Domain.Models.Entities;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Models;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Logic.BLL.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public ServiceResponse<ICollection<ProductInCartDto>> AddProduct(
            ICollection<ProductInCartDto> productsDto, Guid idProduct)
        {
            Product product = _unitOfWork.Products.GetById(idProduct);
            if (product == null)
                return new ServiceResponse<ICollection<ProductInCartDto>>
                    (false, $"Product with id {idProduct} is not exist", productsDto);
            if(product.Status == ProductStatus.NotAvailable)
                return new ServiceResponse<ICollection<ProductInCartDto>>
                    (false, $"Product {product.Title} out of stock", productsDto);
            if (productsDto == null)
                productsDto = new List<ProductInCartDto>();

            var productInCart = productsDto.FirstOrDefault(x => x.Product.Id == product.Id);
            if (productInCart == null)
                productsDto.Add(new ProductInCartDto { 
                    Product = _mapper.Map<ProductDto>(product),
                    Count = 1
                });
            else
            {
                productsDto.Remove(productInCart);
                productInCart.Count++;
                productsDto.Add(productInCart);
            }

            return new ServiceResponse<ICollection<ProductInCartDto>>
                (true, $"Product added to cart", productsDto);
        }

        public ServiceResponse<ICollection<ProductInCartDto>> DeleteProduct(
            ICollection<ProductInCartDto> productsDto, Guid idProduct)
        {
            Product product = _unitOfWork.Products.GetById(idProduct);
            if (product == null)
                return new ServiceResponse<ICollection<ProductInCartDto>>
                    (false, $"Product with id {idProduct} is not exist", productsDto);
            if (product.Status == ProductStatus.NotAvailable)
                return new ServiceResponse<ICollection<ProductInCartDto>>
                    (false, $"Product {product.Title} out of stock", productsDto);

            var productInCart = productsDto.FirstOrDefault(x => x.Product.Id == product.Id);
            if (productInCart == null)
                return new ServiceResponse<ICollection<ProductInCartDto>>
                    (false, $"product with id {product.Id} is not in cart", productsDto);

            if(productInCart.Count > 1)
            {
                productsDto.Remove(productInCart);
                productInCart.Count--;
                productsDto.Add(productInCart);
            }
            else
            {
                productsDto.Remove(productInCart);
            }

            return new ServiceResponse<ICollection<ProductInCartDto>>
                (true, $"Product delete from cart", productsDto);
        }

        public ServiceResponse<CartDto> CountCart(ICollection<ProductInCartDto> productsDto)
        {
            if(productsDto == null)
                return new ServiceResponse<CartDto>(false, $"Cart is empty", null);

            CartDto cartDto = UpdateCart(productsDto);
            return new ServiceResponse<CartDto>
                (true, $"Cart", cartDto);
        }

        public ServiceResponse<OrderDto> MakeOrder(Guid userId, 
            ICollection<ProductInCartDto> productsDto)
        {
            CartDto updatedCartDto = UpdateCart(productsDto);
            if (updatedCartDto.TotalSum <= 0)
                return new ServiceResponse<OrderDto>(false, "Cart is empty", null);

            User user = _unitOfWork.Users.GetById(userId);
            if(user == null)
                return new ServiceResponse<OrderDto>(false, "User is not exist", null);

            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach (var item in updatedCartDto.Products)
            {
                Product product = _unitOfWork.Products.Get()
                    .FirstOrDefault(x => x.Id == item.Product.Id);
                if(product != null)
                {
                    orderProducts.Add(new OrderProduct
                    {
                        Id = Guid.NewGuid(),
                        PricePurchase = product.Price,
                        Product = product
                    });
                }
            }

            Order order = new Order
            {
                Id = Guid.NewGuid(),
                Products = orderProducts,
                Amount = updatedCartDto.TotalSum,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Status = OrderStatus.Processing,
                User = user
            };

            _unitOfWork.Orders.Add(order);
            _unitOfWork.Commit();

            return new ServiceResponse<OrderDto>(
                true, "Order successfully created", _mapper.Map<OrderDto>(order));
        }

        private CartDto UpdateCart(ICollection<ProductInCartDto> productsDto)
        {
            List<IProductInCart> productInCarts = UpdateProduct(productsDto);
            ICartModel cartModel = new CartModel(productInCarts);
            cartModel.CountSumTotal();
            return _mapper.Map<CartDto>(cartModel);
        }

        private List<IProductInCart> UpdateProduct(ICollection<ProductInCartDto> productsDto)
        {
            List<IProductInCart> productInCarts = new List<IProductInCart>();

            foreach (var item in productsDto)
            {
                Product product = _unitOfWork.Products.
                    Get().FirstOrDefault(x => x.Id == item.Product.Id);
                if (product != null && product.Status == ProductStatus.Available)
                {
                    productInCarts.Add(new ProductInCart
                    {
                        Count = item.Count,
                        Product = product
                    });
                }
            }

            return productInCarts;
        }
    }
}
