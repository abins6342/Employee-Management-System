using EmployeeManagement.UI.Controllers.InternalAPI;
using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Test
{
    [TestClass]
    public class EmployeeInternalApiControllerTest
    {
        #region PRIVATE_FIELD
        private Mock<IEmployeeApiClient> _mockEmployeeApiClient;
        private EmployeeInternalApiController _employeeInternalApiController;
        #endregion


        [TestInitialize]
        public void InitialiseTest()
        {
            _mockEmployeeApiClient = new Mock<IEmployeeApiClient>();
            _employeeInternalApiController = new EmployeeInternalApiController(_mockEmployeeApiClient.Object);
        }

        [TestMethod]
        public void GetEmployeeById_ReturnsSucessfull()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m=>m.GetEmployeeById(It.IsAny<int>())).Returns(GetDummyEmployeeViewModelSuccessfull());
            //Act

            var result = _employeeInternalApiController.GetEmployeeById(1) as OkObjectResult;

            //Asert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);

        }

        [TestMethod]
        public void GetEmployeeById_ReturnsNull()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(GetDummyEmployeeViewModelNull());
         
            //Act
            var result = _employeeInternalApiController.GetEmployeeById(1) as ObjectResult;

            //Asert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);

        }
        [TestMethod]
        public void InsertEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(true);

            //Act
            var result = _employeeInternalApiController.InsertEmployee(new EmployeeDetailedViewModel()) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]
        public void DeleteEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.DeleteEmployee(It.IsAny<int>())).Returns(true);

            //Act
            var result = _employeeInternalApiController.DeleteEmployee(1) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]
        public void DeleteEmployee_ReturnInvalidId()
        {
            //Arrange
            //_mockEmployeeApiClient.Setup(m => m.DeleteEmployee(It.IsAny<int>()));

            //Act
            var result = _employeeInternalApiController.DeleteEmployee(-1) as ObjectResult;

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void UpdateEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(true);

            //Act
            var result = _employeeInternalApiController.UpdateEmployee(new EmployeeDetailedViewModel()) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }
        [TestMethod]
        public void UpdateEmployee_ReturnsInvalidId()
        {
            //Arrange
           // _mockEmployeeApiClient.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDetailedViewModel>()));

            //Act
            var result = _employeeInternalApiController.UpdateEmployee(new EmployeeDetailedViewModel() {Id=-1 }) as ObjectResult;

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }



        private EmployeeDetailedViewModel GetDummyEmployeeViewModelSuccessfull()
        {
            return new EmployeeDetailedViewModel ();
        }

        private EmployeeDetailedViewModel GetDummyEmployeeViewModelNull()
        {
            return null;
        }
    }
}
