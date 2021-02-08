using LearnApiUsingMiddleware.Controllers;
using LearnApiUsingMiddleware.Filters;
using LearnApiUsingMiddleware.Models;
using LearnApiUsingMiddleware.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitApi
{
    public class ShoppingCartTest
    {

        ShoppingCartController _controller;
        IShoppingCartService _service;

        public ShoppingCartTest()
        {
            _service = new ShoppingCartService();
            _controller = new ShoppingCartController(_service);
           
        }

        [Fact]
        public void Get_WhenCalled_WhenOkResults()
        {

            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);

        }

        [Fact]
        public void Get_WhenCountResult_WhenOkResult()
        {
            var okResult = _controller.Get().Result as OkObjectResult;

            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknowGuiPased_ReturnNotFoundResult()
        {
            var notFoundResult = _controller.GetById(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnOkResult()
        {
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var existingId = _controller.GetById(id);
            Assert.IsType<OkObjectResult>(existingId.Result);

        }

        [Fact]
        public void GetById_ExistingGuIdPassed_ReturnRightItem()
        {
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var okResult = _controller.GetById(id).Result as OkObjectResult;

            Assert.IsType<ShoppingItem>(okResult.Value);
            Assert.Equal(id, (okResult.Value as ShoppingItem).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.PostShoppingItem(nameMissingItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            ShoppingItem createItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            var createdPost = _controller.PostShoppingItem(createItem);

            Assert.IsType<CreatedAtActionResult>(createdPost);

        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            ShoppingItem testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            var createdResponse = _controller.PostShoppingItem(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ShoppingItem;

            Assert.IsType<ShoppingItem>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Name);
        }


        [Fact]
        public void Remove_NotExistingGuIdPassed_ReturnNotFoundResponse()
        {
            var notExistingGuid = Guid.NewGuid();

            var removeData = _controller.Remove(notExistingGuid);

            Assert.IsType<NotFoundResult>(removeData);
        }

        [Fact]
        public void Remove_ExistingGuIdPassed_ReturnOkResult()
        {
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            var removeData = _controller.Remove(existingGuid);
            Assert.IsType<OkResult>(removeData);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var removeOneItem = _controller.Remove(existingGuid);

            Assert.Equal(2, _service.GetAllItems().Count());
        }

    }
}
