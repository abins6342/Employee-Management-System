using EmployeeManagement.API.Controllers;
using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.API.Test
{
    [TestClass]
    public class EmployeeApiControllerTest
    {
        #region PRIVATE_FIELD
        private Mock<IEmployeeService>  _mockEmployeeService;
        private EmployeeApiController _employeeApiController;
        #endregion

        #region TEST_INITIALISE
        [TestInitialize]
        public void InitialiseTest()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeApiController = new EmployeeApiController(_mockEmployeeService.Object);
        }
        #endregion

        #region PUBLIC_METHODS
        [TestMethod]
        public void GetAllEmployees_ReturnsSucessfull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.GetEmployees()).Returns(new List<EmployeeDto>());

            ///Act
            var result = _employeeApiController.GetEmployees() as OkObjectResult;

            ///Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]

        public void GetAllEmployees_ReturnsNull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.GetEmployees()).Returns(()=>null);

            ///Act
            var result = _employeeApiController.GetEmployees() as ObjectResult;

            ///Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void GetEmployeeByIDTest_ReturnsSuccessfull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(GetDummyEmployeeDtoReturnsSuccessfull()).Callback<int>(data=> {
                Assert.AreEqual(data,20);
            });

            ///Act
            var result = _employeeApiController.GetEmployeeById(1) as OkObjectResult;

            ///Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);

            //verify
            _mockEmployeeService.Verify(m => m.GetEmployeeById(It.IsAny<int>()),Times.Once);
        }

        [TestMethod]
        public void GetEmployeeByIDTest_ReturnsNull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(GetDummyEmployeeDtoReturnsNull());

            ///Act
            var result =  _employeeApiController.GetEmployeeById(1) as ObjectResult;

            ///Assert
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
       
        public void GetEmployeeByIDTest_ReturnsInvalidId()
        {
            ///Arrange
            //_mockEmployeeService.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(GetDummyEmployeeDtoReturnsNull());

            ///Act
            var result = _employeeApiController.GetEmployeeById(-1) as ObjectResult;

            ///Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }
        [TestMethod]
        
        public void InsertEmployee_ReturnsSuccesfull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDto>())).Returns(true).Callback<EmployeeDto>(data=>
            {
                Assert.AreEqual(data.Id, 0);
                Assert.AreEqual(data.Name,"Abin");
                Assert.AreEqual(data.Address, "tvm");
                Assert.AreEqual(data.Department, "Develop");
            });

            ///Act
            var result = _employeeApiController.InsertEmployee(new EmployeeDetailedViewModel() { Name = "Abin", Address = "tvm", Age = 22, Department = "Develop" }) as OkObjectResult;

            ///Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]
       
        public void UpdateEmployee_ReturnsSuccessful()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDto>())).Returns(true);

            ///Act
            var result = _employeeApiController.UpdateEmployee(new EmployeeDetailedViewModel()) as OkObjectResult;

            ///Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]
        public void UpdateEmployee_ReturnsInvalidEntry()
        {
            ///Arrange
           // _mockEmployeeService.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDto>()));
           //null

            ///Act
            var result = _employeeApiController.UpdateEmployee(new EmployeeDetailedViewModel() { Id=-1}) as ObjectResult;

            ///Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [TestMethod]       
        public void UpdateEmployee_ReturnsNull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDto>())).Returns(false);
            
            ///Act
            var result = _employeeApiController.UpdateEmployee(new EmployeeDetailedViewModel()) as ObjectResult;

            ///Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void DeleteEmployee_ReturnsSuccesfull()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(It.IsAny<int>())).Returns(true);

            ///Act
            var result = _employeeApiController.DeleteEmployee(1) as OkObjectResult;

            ///Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }

        [TestMethod]
        public void DeleteEmployee_ReturnsInvalidid()
        {
            ///Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(It.IsAny<int>()));

            ///Act
            var result = _employeeApiController.DeleteEmployee(-1) as ObjectResult;

            ///Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }
        #endregion

        #region PRIVATE_METHODS
        private EmployeeDto GetDummyEmployeeDtoReturnsNull()
        {
            return null;  
        }

        private EmployeeDto GetDummyEmployeeDtoReturnsSuccessfull()
        {
            return new EmployeeDto();
        }
        #endregion

    }
}
