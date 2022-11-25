using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Models.Graph
{
    public class AffectedRowsResponse
    {
        [JsonProperty("affected_rows")]
        public int AffectedRows { get; set; }
    }
}
