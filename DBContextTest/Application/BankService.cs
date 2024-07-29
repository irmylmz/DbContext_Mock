using Domain;

namespace Application;

public interface BankService
{
    public IEnumerable<Customer> GetAllCustomers();
    public IEnumerable<Employee> GetAllEmployees();
    Customer GetCustomerById(int id);
    void InsertCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);

    Employee GetEmployeeById(int id);
    void InsertEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
}