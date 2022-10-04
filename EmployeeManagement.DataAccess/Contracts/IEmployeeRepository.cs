using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeData> GetEmployees();
        EmployeeData GetEmployeeById(int id);
        void InsertEmployee(EmployeeData employeeData);
        void UpdateEmployee(EmployeeData employeeData);
        void DeleteEmployee(int id);
        
    }
}
