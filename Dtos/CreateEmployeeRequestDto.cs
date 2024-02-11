using System.ComponentModel.DataAnnotations;

namespace FinancialTamkeen_BlogAPI.Dtos
{
    public class CreateEmployeeRequestDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        
        public string? Department { get; set; } = null!;
        [Required]
        public decimal Salary  { get; set; } 

    }
}
