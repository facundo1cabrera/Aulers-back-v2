using AulersAPI.Controllers;
using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AulersApiTest.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public async void GetAllUsers_Should_Return_Users_Provided_By_Service()
        {
            //Arrange
            var mockRepo = new Mock<IUsersRepository>();
            mockRepo.Setup(repo => repo.GetUsers())
                .ReturnsAsync(new List<User>() 
                    { 
                        new User() { Id = 1, Email = "test@mail.com" },
                        new User() { Id = 2, Email = "test2@mail.com" },
                    });
            var controller = new UsersController(mockRepo.Object, null);

            //Act
            var response = await controller.GetAllUsers() as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<User>>(response.Value);
            Assert.Equal(2, items.Count);
        }

    }
}
