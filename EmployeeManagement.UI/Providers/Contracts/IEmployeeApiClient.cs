using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Providers.Contracts
{
    public interface IEmployeeApiClient
    {
        IEnumerable<EmployeeViewModel> GetAllEmployee();
        EmployeeDetailedViewModel GetEmployeeById(int id);

        void InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel);

        void UpdateEmployee(EmployeeDetailedViewModel employeeDetailedViewModel);

        void DeleteEmployee(int id);

    }
}
