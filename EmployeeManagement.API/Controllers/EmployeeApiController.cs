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
                    throw new ArgumentNullException("No Employee");
                }
                
               
                
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
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

                _employeeService.InsertEmployee(empDto);
                return Ok();

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
                if (employeeDetailedViewModel.Id <0)
                {
                    throw new ArgumentException("Invalid Employee Entry");
                }
                var empDto = new EmployeeDto
                {
                    Id = employeeDetailedViewModel.Id,
                    Name = employeeDetailedViewModel.Name,
                    Address = employeeDetailedViewModel.Address,
                    Age = employeeDetailedViewModel.Age,
                    Department = employeeDetailedViewModel.Department
                };

                
                _employeeService.UpdateEmployee(empDto);
                return Ok();
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
                _employeeService.DeleteEmployee(employeeId);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
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

        public void ValidateEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                throw new ArgumentException("Invalid Employee Entry");
            }

        }

        
        //Create Employee Insert, Update and Delete Endpoint here
    }
}
