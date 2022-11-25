using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
       
        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {

                ValidateEmployee(employeeId);
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
                var getAEmployee = _employeeService.GetEmployeeById(employeeId);
               
                if (getAEmployee != null)
                {
                    var employeeDetailedView = new EmployeeDetailedViewModel
                    {
                        Id = getAEmployee.Id,
                        Name = getAEmployee.Name,
                        Address = getAEmployee.Address,
                        Age = getAEmployee.Age,
                        Department = getAEmployee.Department
                    };
                    return Ok(employeeDetailedView);

                }
                else {
                    throw new Exception("No Employee");
                }   
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            } 
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetEmployees()
        {
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
            /// 
            try
            {
                var getAllEmployee = _employeeService.GetEmployees();

                if (getAllEmployee == null)
                {
                    throw new Exception("No Employees");
                }
                /*var listOfEmployeView = new List<EmployeeDetailedViewModel>();
                foreach (var employeView in listOfEmployeView)
                {
                    var employee = new EmployeeDetailedViewModel()
                    {
                        Id = employeView.Id,
                        Name = employeView.Name,
                        Department = employeView.Department
                    };
                    listOfEmployeView.Add(employee);
                }

                return Ok(listOfEmployeView);*/

                return Ok(getAllEmployee.Select(employee => new EmployeeDetailedViewModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Department = employee.Department
                }));
            }
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("insertEmployees")]
        public IActionResult InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {      
                
                var empDto = new EmployeeDto
                {
                    Name = employeeDetailedViewModel.Name,
                    Address = employeeDetailedViewModel.Address,
                    Age = employeeDetailedViewModel.Age,
                    Department = employeeDetailedViewModel.Department
                };

               var employee= _employeeService.InsertEmployee(empDto);
                if (!employee)
                    throw new Exception("No such Employee");
                return Ok(employee);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateemployees")]
        public IActionResult UpdateEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {
                ValidateEmployee(employeeDetailedViewModel.Id);               
                var empDto = new EmployeeDto
                {
                    Id = employeeDetailedViewModel.Id,
                    Name = employeeDetailedViewModel.Name,
                    Address = employeeDetailedViewModel.Address,
                    Age = employeeDetailedViewModel.Age,
                    Department = employeeDetailedViewModel.Department
                };

                var employee = _employeeService.UpdateEmployee(empDto);
                if (!employee)
                    throw new Exception("No such Employee");
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete]
        [Route("deleteemployees/{employeeId}")]
        public  IActionResult DeleteEmployee(int employeeId)
        {
            try
            {

                ValidateEmployee(employeeId);
                var employee=_employeeService.DeleteEmployee(employeeId);
                if (!employee)
                    throw new Exception("No such Employee");
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private void ValidateEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                throw new ArgumentException("Invalid Employee Entry");
            }

        }
        //Create Employee Insert, Update and Delete Endpoint here
    }
}
