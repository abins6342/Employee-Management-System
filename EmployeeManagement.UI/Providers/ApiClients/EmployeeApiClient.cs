using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("https://localhost:44305/api/getall").Result)
            {
                var employee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }
        }

        public EmployeeDetailedViewModel GetEmployeeById(int employeeId)
        {
            //Consume /{employeeId} endpoint in the EmployeeManagementApi using _httpClient

            using (var response = _httpClient.GetAsync("https://localhost:44305/api/" + employeeId).Result)
            {
                 var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(response.Content.ReadAsStringAsync().Result);

                 return employee;
            }  
        }

        public async void InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var json = JsonConvert.SerializeObject(employeeDetailedViewModel);
            var contentData = new StringContent(json, Encoding.UTF8, "application/json");
            using var response =await _httpClient.PostAsync("https://localhost:44305/api/insertEmployees", contentData);
            await response.Content.ReadAsStringAsync();
        }

        public async void UpdateEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var json = JsonConvert.SerializeObject(employeeDetailedViewModel);
            var contentData = new StringContent(json, Encoding.UTF8, "application/json");
            using var response =await _httpClient.PutAsync("https://localhost:44305/api/updateemployees", contentData);
            await response.Content.ReadAsStringAsync();
        }

        public async void DeleteEmployee(int employeeId)
        {
            using var response =await _httpClient.DeleteAsync("https://localhost:44305/api/deleteemployees/" + employeeId);
            await response.Content.ReadAsStringAsync();
        }
    }
}
 