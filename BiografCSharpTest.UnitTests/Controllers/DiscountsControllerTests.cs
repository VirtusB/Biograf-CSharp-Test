using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Controllers;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Models;
using Moq;
using Xunit;

namespace BiografCSharpTest.UnitTests.Controllers
{
    public class DiscountsControllerTests
    {
        [Fact]
        public async Task GetDiscount_DiscountExists_ReturnsDiscount() {
            
            var mockDiscountRepo = new Mock<IDiscountRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var mockUserRepo = new Mock<IUserRepository>();

            mockDiscountRepo.Setup(repo => repo.GetDiscount(1))
                .ReturnsAsync(GetTestDiscount());
            var controller = new DiscountsController(mockDiscountRepo.Object, mockAutoMapper.Object, mockUserRepo.Object);

            
            var result = await controller.GetDiscount(1);

            
            var discountResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
            var discount = Assert.IsType<Discount>(discountResult.Value);
        }

        [Fact]
        public async Task GetDiscounts_ReturnsListOfDiscounts() {
            
            var mockDiscountRepo = new Mock<IDiscountRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var mockUserRepo = new Mock<IUserRepository>();

            mockDiscountRepo.Setup(repo => repo.GetDiscounts())
                .ReturnsAsync(GetTestDiscountsList());
            var controller = new DiscountsController(mockDiscountRepo.Object, mockAutoMapper.Object, mockUserRepo.Object);

            
            var result = await controller.GetDiscounts();

            
            var discountsResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
            var discount = Assert.IsType<List<Discount>>(discountsResult.Value);
            Assert.Equal(2, discount.Count);
        }

        

        private Discount GetTestDiscount()
        {
            return new Discount {
                Id = 1,
                RequiredBookings = 75,
                Amount = 15
            };
        }


        private List<Discount> GetTestDiscountsList()
        {
            return new List<Discount> {
                new Discount {
                    Id = 1,
                    RequiredBookings = 75,
                    Amount = 15
                },
                new Discount {
                    Id = 2,
                    RequiredBookings = 125,
                    Amount = 20
                }
            };
        }
    }
}