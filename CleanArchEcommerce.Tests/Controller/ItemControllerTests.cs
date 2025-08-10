using CleanArchEcommerce.API.Controllers;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Items.Query.GetAllItem;
using CleanArchEcommerce.Tests.Common;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Tests.Controller
{
    public class ItemControllerTests : ControllerTestBase<ItemController>
    {
        public ItemControllerTests() : base(new ItemController()) { }
        [Fact]
        public async Task GetAllItemAsync_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var result = Result.Success<List<ItemDto>>(new List<ItemDto>());
            MediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await Controller.GetAllItemAsync(new GetAllItemQuery());

            // Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

    }
}
