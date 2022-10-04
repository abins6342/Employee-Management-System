using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return null;
                }

                return new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Department = employee.Department
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            try
            {
                var employeeDataList = _employeeRepository.GetEmployees();
                if (employeeDataList == null)
                {
                    return null;
                }

                return employeeDataList.Select(employee => new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Department = employee.Department
                });
            }
            catch (Exception)
            {

                throw;
            } 
        }

        public void InsertEmployee(EmployeeDto employeedto)
        {
            try
            {
                var empdata = new EmployeeData()
                {
                    
                    Name = employeedto.Name,
                    Age = employeedto.Age,
                    Address = employeedto.Address,
                    Department = employeedto.Department
                };
                _employeeRepository.InsertEmployee(empdata);
               
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public void UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var empdata = new EmployeeData()
                {
                    Id=employeeDto.Id,
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Department = employeeDto.Department
                };
                _employeeRepository.UpdateEmployee(empdata);

            }
            catch (Exception)
            {
               throw;
            }
        }

        public void DeleteEmployee(int id)
        {
           _employeeRepository.DeleteEmployee(id);
        }
    }
}
