using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal/employee")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                ValidateEmployee(employeeId);
                var employee = _employeeApiClient.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    throw new Exception("No Employee Found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("insert-employees")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {
                var employee=_employeeApiClient.InsertEmployee(employeeDetailedViewModel);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        [Route("update-employees")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {
                ValidateEmployee(employeeDetailedViewModel.Id);
                var employee = _employeeApiClient.UpdateEmployee(employeeDetailedViewModel);

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
        public IActionResult DeleteEmployee([FromRoute] int employeeId)
        {
            try
            {
                ValidateEmployee(employeeId);
                var employee=_employeeApiClient.DeleteEmployee(employeeId);
                
                return Ok(employee);
            }
            catch(ArgumentException ex){
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
                throw new ArgumentException("invalid Entry");
            }
        }
    }
}
