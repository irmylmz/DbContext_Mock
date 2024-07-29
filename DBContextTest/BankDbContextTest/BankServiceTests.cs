using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Moq;
using Xunit;
using Application;

namespace BankDbContextTest;

public class BankServiceTests
    {
        private readonly IQueryable<Customer> customers;
        private readonly IQueryable<Employee> employees;
        private readonly Mock<BankDbContext> mockContext;

        public BankServiceTests()
        {
            customers = new List<Customer>
            {
                new Customer(1, "Customer 1", "Address1", "woman", "1000"),
                new Customer(2, "Customer 2", "Address2", "man", "1001"),
                new Customer(3, "Customer 3", "Address3", "woman", "1002")
            }.AsQueryable();

            employees = new List<Employee>
            {
                new Employee(1, "Employee 1", "Address1", "woman", "Manager"),
                new Employee(2, "Employee 2", "Address2", "man", "Clerk")
            }.AsQueryable();

            mockContext = new Mock<BankDbContext>(new DbContextOptions<BankDbContext>());
        }

        [Fact]
        public void GetAllCustomers_ReturnsAllCustomers()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var result = service.GetAllCustomers();
        
            Assert.Equal(3, result.Count());
            Assert.Contains(result, c => c.Name == "Customer 1");
            Assert.Contains(result, c => c.Name == "Customer 2");
            Assert.Contains(result, c => c.Name == "Customer 3");
        }

        [Fact]
        public void GetCustomerById_ReturnsCustomer()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => customers.FirstOrDefault(c => c.Id == (int)ids[0]));

            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var result = service.GetCustomerById(2); 

            Assert.NotNull(result);
            Assert.Equal("Customer 2", result.Name);
        }




        [Fact]
        public void InsertCustomer_AddsCustomer()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var customer = new Customer(4, "Customer 4", "Address 4", "man", "1003");
            service.InsertCustomer(customer);

            mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void UpdateCustomer_UpdatesCustomer()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var customer = new Customer(2, "Updated Customer 2", "Address 2", "man", "1001");
            service.UpdateCustomer(customer);

            mockSet.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteCustomer_RemovesCustomer()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => customers.FirstOrDefault(c => c.Id == (int)ids[0]));

            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            service.DeleteCustomer(2);

            mockSet.Verify(m => m.Remove(It.Is<Customer>(c => c.Id == 2)), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Fact]
        public void GetAllEmployees_ReturnsAllEmployees()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var result = service.GetAllEmployees();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Name == "Employee 1");
            Assert.Contains(result, e => e.Name == "Employee 2");
        }

        [Fact]
        public void GetEmployeeById_ReturnsEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => employees.FirstOrDefault(e => e.Id == (int)ids[0]));

            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var result = service.GetEmployeeById(2);

            Assert.NotNull(result);
            Assert.Equal("Employee 2", result.Name);
        }

        [Fact]
        public void InsertEmployee_AddsEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var employee = new Employee(3, "Employee 3", "Address 3", "woman", "Developer");
            service.InsertEmployee(employee);

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void UpdateEmployee_UpdatesEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            var employee = new Employee(2, "Updated Employee 2", "Address 3", "woman", "Clerk");
            service.UpdateEmployee(employee);

            mockSet.Verify(m => m.Update(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteEmployee_RemovesEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => employees.FirstOrDefault(e => e.Id == (int)ids[0]));

            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            var service = new BankManager(mockContext.Object);

            service.DeleteEmployee(2);

            mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }