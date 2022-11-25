using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Models.Graph
{
    public class EmployeesQueryResponse
    {
        [JsonProperty("employees")]
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }

    public class EmployeeByIdQueryResponse
    {
        [JsonProperty("employees_by_pk")]
        public EmployeeDto Employee { get; set; }
    }
}
