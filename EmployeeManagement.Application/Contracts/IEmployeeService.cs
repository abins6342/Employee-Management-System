using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees();
        EmployeeDto GetEmployeeById(int id);

       void InsertEmployee(EmployeeDto employeeDto);

        void DeleteEmployee(int id);

        void UpdateEmployee(EmployeeDto employeeDto);


    }
}
