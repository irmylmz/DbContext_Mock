using Domain;
using Persistence;

namespace Application;

public class BankManager : BankService
{
    private readonly BankDbContext _context;

    public BankManager(BankDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _context.Customers.ToList();
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _context.Employees.ToList();
    }

    public Customer GetCustomerById(int id)
    {
        return _context.Customers.Find(id);
    }

    public void InsertCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();
    }

    public void DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }

    public Employee GetEmployeeById(int id)
    {
        return _context.Employees.Find(id);
    }

    public void InsertEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}