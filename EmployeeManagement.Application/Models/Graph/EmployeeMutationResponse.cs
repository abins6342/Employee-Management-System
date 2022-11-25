using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Models.Graph
{
    public class EmployeeInsertMutationResponse
    {
        [JsonProperty("insert_employees_one")]
        public EmployeeDto Employee { get; set; }
    }
    public class EmployeeUpdateMutationResponse
    {
        [JsonProperty("update_employees_by_pk")]
        public EmployeeDto Employee { get; set; }
    }
    public class EmployeeDeleteMutationResponse
    {
        [JsonProperty("delete_employees")]
        public AffectedRowsResponse Employee { get; set; }
    }

}
