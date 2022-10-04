using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.UI.Models
{
    public class EmployeeDetailedViewModel
    {
        [Required(ErrorMessage="Please enter a ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a Department name")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Please enter a age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter a address")]
        public string Address { get; set; }
    }
}
