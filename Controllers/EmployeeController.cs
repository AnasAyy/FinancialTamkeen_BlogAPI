using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialTamkeen_BlogAPI.Data.Repo;
using FinancialTamkeen_BlogAPI.Dtos;
using FinancialTamkeen_BlogAPI.Models;
using FinancialTamkeen_BlogAPI.Utilties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinancialTamkeen_BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee, IConfiguration configuration)
        {
            _employee = employee;
            _configuration = configuration;
        }

        [Authorize, HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(CreateEmployeeRequestDto request)
        {
            if(request.FirstName == null || request.LastName == null || request.Salary == 0)
            {
                return BadRequest(new MessageDto
                {
                    MessageAr = "عذراً، حدث خطأ ما. يرجى المحاولة مرة أخرى.",
                    MessageEn = "Oops, something went wrong. Please try again.",
                });
            }

            var emp = new Employee(); 
            emp.FirstName = request.FirstName;
            emp.LastName = request.LastName;
            emp.Salary = request.Salary;
            if (request.Department != null) { emp.Department = request.Department; }
            _employee.Create(emp);
            if (!_employee.SaveChanges())
            {
                return BadRequest(new MessageDto
                {
                    
                    MessageAr = "عذراً، حدث خطأ ما. يرجى المحاولة مرة أخرى.",
                    MessageEn = "Oops, something went wrong. Please try again.",
                });
            }

            return Ok();
        }


        [Authorize, HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(UpdateEmployeeRequestDto request)
        {


            var emp = new Employee();
            if (request.FirstName != null) { emp.FirstName = request.FirstName; }
            if (request.LastName != null) { emp.LastName = request.LastName; }
            if (request.Salary != 0 ) { emp.Salary = request.Salary; }
            if (request.Department != null) { emp.Department = request.Department; }

            _employee.Update(emp);
            if (!_employee.SaveChanges())
            {
                return BadRequest(new MessageDto
                {

                    MessageAr = "عذراً، حدث خطأ ما. يرجى المحاولة مرة أخرى.",
                    MessageEn = "Oops, something went wrong. Please try again.",
                });
            }

            return Ok();
        }

        [HttpGet("GetAllEmployes")]
        public IActionResult GetAllUsers()
        {
            var result = _employee.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(new MessageDto
            {
                MessageAr = "لا يوجد بيانات",
                MessageEn = "No Data",
            }); ;
        }
        
        
        [HttpGet("GetEmployeById")]
        public IActionResult GetEmployeById(int Id)
        {
            var result = _employee.GetEmployeeById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(new MessageDto
            {
                MessageAr = "لا يوجد بيانات",
                MessageEn = "No Data",
            }); ;
        }



        [AllowAnonymous, HttpPost("Login")]
        public IActionResult Login(string UserName, string Password)
        {


            if (UserName != "Anas" || Password != "123")
            {
                return Ok(new MessageDto
                {
                    MessageAr = "اسم المستخدم او كلمة المرور خاطئة",
                    MessageEn = "Username or password may wrong ",
                }); ;
            }


            #region Create Token
            var subject = _configuration["Jwt:Subject"];
            var keyhash = _configuration["Jwt:Key"];
            if (subject == null || keyhash == null)
            {
                return BadRequest();
            }


            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyhash));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(48),
                signingCredentials: signIn);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);




            return Ok(stringToken);
                #endregion

            
           
        }
    }
}
