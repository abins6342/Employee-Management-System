using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Models.Graph;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees();
        EmployeeDto GetEmployeeById(int id);

       bool InsertEmployee(EmployeeDto employeeDto);

        bool DeleteEmployee(int id);

        bool UpdateEmployee(EmployeeDto employeeDto);

        Task<IEnumerable<EmployeeDto>> GetEmployeesHasura();
        Task<EmployeeDto> GetEmployeeByIdHasura(int id);

        Task<int> InsertEmployeeSaveToHasura(EmployeeDtoHasura employeeDtoHasura);

        Task<int> UpdateEmployeeSaveToHasura(EmployeeDto employeeDto);

    }
}
