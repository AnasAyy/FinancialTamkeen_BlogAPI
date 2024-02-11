using System.ComponentModel.DataAnnotations;

namespace FinancialTamkeen_BlogAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ?Department { get; set; } 
        public decimal Salary { get; set; }
    }
}
