
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using WebApi.Controllers;
using WebApi.Store;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Tests.DataUtilities;

namespace WebApi.Tests.Controllers
{
    public class UsersControllerTests
    {
        #region GET
        [Fact]
        public async Task GetUsers_NullList_ReturnsNotFound()
        {
            //Arrange
            var serviceStub = new Mock<IDataStore>();
            IEnumerable<User> mockedUsers = null;
            serviceStub.Setup(
                users => users.Get<User>()
            ).ReturnsAsync(mockedUsers);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUsers_EmptyList_ReturnsNotFound()
        {
            //Arrange
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>()
            ).ReturnsAsync(new List<User>());

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUsers_UserList_ReturnsAllUsers()
        {
            //Arrange
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>()
            ).ReturnsAsync(UserData.GetUsers());

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get() as OkObjectResult;

            //Assert
            Assert.Equal(2, ((List<User>)result.Value).Count);
        }
        #endregion GET

        #region GETbyId
        [Fact]
        public async Task GetUser_EmptyId_ReturnsBadRequest()
        {
            //Arrange
            string emptyId = " ";
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(emptyId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get(emptyId);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetUser_UnknownId_ReturnsBadRequest()
        {
            //Arrange
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.UnknownUserId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get(UserData.UnknownUserId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUser_KnownId_ReturnsUser()
        {
            //Arrange
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.KnownUserId)
            ).ReturnsAsync(UserData.GetKnownUser());

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Get(UserData.KnownUserId) as OkObjectResult;

            //Assert
            Assert.Equal(UserData.KnownUserId, ((User)result.Value).Id);
        }
        #endregion GETbyId

        #region POST
        [Fact]
        public async Task Create_NullUser_ReturnsBadRequest()
        {
            //Arrange
            string emptyId = null;
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Insert(user)
            ).ReturnsAsync(emptyId);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Post(user);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_User_ReturnsCreatedAtRouteResponse()
        {
            //Arrange
            User user = UserData.GetKnownUser();
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Insert(user)
            ).ReturnsAsync(UserData.KnownUserId);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Post(user) as CreatedAtRouteResult;

            //Assert
            Assert.Equal(UserData.KnownUserId, ((User)result.Value).Id);
        }
        #endregion POST

        #region PUT
        [Fact]
        public async Task Update_NullUserId_ReturnsBadRequest()
        {
            //Arrange
            string emptyId = null;
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Put(emptyId, user);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_NullUser_ReturnsBadRequest()
        {
            //Arrange
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Put(UserData.KnownUserId, user);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_IdAndUserIdDontMatch_ReturnsBadRequest()
        {
            //Arrange
            User user = UserData.GetKnownUser();
            var serviceStub = new Mock<IDataStore>();
            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Put(UserData.UnknownUserId, user);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_UnknownUser_ReturnsNotFound()
        {
            //Arrange
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.UnknownUserId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Put(UserData.UnknownUserId, UserData.GetUnknownUser());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_User_ReturnsNoContent()
        {
            //Arrange
            User user = UserData.GetKnownUser();
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.KnownUserId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Put(UserData.KnownUserId, user);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        #endregion PUT

        #region DELETE
        [Fact]
        public async Task Delete_NullUserId_ReturnsBadRequest()
        {
            //Arrange
            string emptyId = null;
            var serviceStub = new Mock<IDataStore>();
            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Delete(emptyId);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_User_ReturnsNoContent()
        {
            //Arrange
            User user = UserData.GetKnownUser();
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.KnownUserId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Delete(UserData.KnownUserId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_UnknownUser_ReturnsNotFound()
        {
            //Arrange
            User user = null;
            var serviceStub = new Mock<IDataStore>();
            serviceStub.Setup(
                users => users.Get<User>(UserData.UnknownUserId)
            ).ReturnsAsync(user);

            var contoller = new UsersController(serviceStub.Object);

            //Act
            var result = await contoller.Delete(UserData.UnknownUserId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion PUT
    }
}
