namespace Domain;

public class Employee : Person
{
    public string Position { get; set; }
    
    public Employee(int id, string name, string address, string gender, string position)
        : base(id, name, address, gender)
    {
        Position = position;
    }
}