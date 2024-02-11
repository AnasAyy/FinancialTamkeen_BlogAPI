namespace FinancialTamkeen_BlogAPI.Dtos
{
    public class UpdateEmployeeRequestDto
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; } 
      
        public string? LastName { get; set; } 

        public string? Department { get; set; } 
        
        public decimal Salary { get; set; }
    }
}
