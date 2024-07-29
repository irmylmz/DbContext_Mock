namespace Domain;


public class Customer : Person
{
    public string AccountNumber { get; set; }

    public Customer(int id, string name, string address, string gender, string accountNumber):base(id, name, address, gender)
    {
        AccountNumber = accountNumber;
    }
}