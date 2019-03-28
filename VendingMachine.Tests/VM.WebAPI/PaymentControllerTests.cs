using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.WebAPI.Controllers;
using Xunit;

namespace VendingMachine.Tests.VM.WebAPI
{
    public class PaymentControllerTests
    {
        private readonly PaymentController _controller;
        private Mock<IPaymentService> _mockService;

        public PaymentControllerTests()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                 new Claim("name", "John Doe")
            }));

            // Arrange
            _mockService = new Mock<IPaymentService>();
            _mockService.Setup(repo => repo.BuyProduct(It.IsAny<Guid>(), It.IsAny<Core.Models.TypeProduct>()))
                .ReturnsAsync(GetProduct());

            _controller = new PaymentController(_mockService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }

        [Fact]
        public async Task BuyProductPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("TypeProduct", "Required");
            var param = new CreatorProductDTO();

            // Act
            var response = await _controller.BuyProduct(param);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(response);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task BuyProductPost_ReturnsBadRequestResult_WhenModelStateIsValid()
        {
            var param = new CreatorProductDTO {
                Availability = 10,
                Product = new ProductDTO { Name = "Coffee", Price = 10 },
                TypeProduct = Core.Models.TypeProduct.Coffee
            };

            // Act
            var response = await _controller.BuyProduct(param);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(response);
            var returnValue = Assert.IsType<ProductDTO>(actionResult.Value);
            _mockService.Verify();
            Assert.Equal("Coffee", returnValue.Name);
            Assert.Equal(10, returnValue.Price);
        }

        private ProductDTO GetProduct()
        {
            return new ProductDTO { Name = "Coffee", Price = 10 };
        }
    }
}
