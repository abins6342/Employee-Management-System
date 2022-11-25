using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Models.Graph;
using EmployeeManagement.Application.Services;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Application.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {

        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private EmployeeService _employeeService;

        #region TEST_INITIALISE
        [TestInitialize]
        public void InitialiseTest()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object,null);
        }
        #endregion
       
        [TestMethod]

        public void GetAllEmployees_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployees()).Returns(new List<EmployeeData>());

            //Act
            var result = _employeeService.GetEmployees();

            //Assert
            Assert.IsNotNull(result);           
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAllEmployees_ReturnsNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployees()).Returns(()=>null);

            //Act
           var result = _employeeService.GetEmployees();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetEmployeeById_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(new EmployeeData());

            //Act
            var result = _employeeService.GetEmployeeById(1);

            //Assert
            Assert.IsNotNull(result);

            //Verify
            _mockEmployeeRepository.Verify(m => m.GetEmployeeById(It.IsAny<int>()),Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetEmployeeById_ReturnsNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployeeById(It.IsAny<int>())).Returns(() => null);

            //Act
            var result = _employeeService.GetEmployeeById(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.InsertEmployee(It.IsAny<EmployeeData>())).Returns(true).Callback<EmployeeData>((employeeData) => { employeeData.Name = "Abin"; employeeData.Department = "IT"; }) ;

            //Act
            var res = _employeeService.InsertEmployee(new EmployeeDto());

            //Assert
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InsertEmployee_ReturnsNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.InsertEmployee(It.IsAny<EmployeeData>())).Returns(false);

            //Act
            var result = _employeeService.InsertEmployee(new EmployeeDto());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeData>())).Returns(true);

            //Act
            var result = _employeeService.UpdateEmployee(new EmployeeDto());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateEmployee_ReturnsNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeData>())).Returns(false);

            //Act
            var result = _employeeService.UpdateEmployee(new EmployeeDto());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateEmployee_ReturnsInvalidId()
        {
            //Arrange
            //_mockEmployeeRepository.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeData>())).Returns(false);

            //Act
            var result = _employeeService.UpdateEmployee(new EmployeeDto() {Id=-1 });

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteEmployee_ReturnsSuccessful()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.DeleteEmployee(It.IsAny<int>())).Returns(true);

            //Act
            var result=_employeeService.DeleteEmployee(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteEmployee_ReturnsNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.DeleteEmployee(It.IsAny<int>())).Returns(false);

            //Act
            var result = _employeeService.DeleteEmployee(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEmployee_ReturnsInvalidID()
        {
            //Arrange
            //_mockEmployeeRepository.Setup(m => m.DeleteEmployee(It.IsAny<int>())).Returns(false);

            //Act
            var result = _employeeService.DeleteEmployee(-1);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
