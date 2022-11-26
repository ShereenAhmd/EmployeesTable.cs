using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LastEmployeeTest.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string image { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
       
        [Column(TypeName = "decimal(12,2)")]
        public Decimal Salary { get; set; }
       
        [MaxLength(14)]
        [MinLength(14)]
        [Column(TypeName ="varchar(14)")]
        public string NationalId { get; set; }
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
        
        public Department? Department { get; set; }
        
    }
}
