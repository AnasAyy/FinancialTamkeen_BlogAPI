using FinancialTamkeen_BlogAPI.Models;

namespace FinancialTamkeen_BlogAPI.Data.Repo
{
    public interface IEmployee
    {
        public void Create(Employee employee);
        public void Update(Employee employee);
        public List<Employee> GetAll();
        public Employee? GetEmployeeById(int id);
        public bool SaveChanges();
    }




    public class EmployeeRepo : IEmployee
    {
        private readonly DataContext _context;

        public EmployeeRepo(DataContext context)
        {
            _context = context;
        }
        public void Create(Employee employee)
        {
             _context.Employees.Add(employee);
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Employee employee)
        {
             _context.Employees.Update(employee);
        }


        public  bool SaveChanges()
        {
            try
            {
                return  _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _context.ChangeTracker.Clear();
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }

}
