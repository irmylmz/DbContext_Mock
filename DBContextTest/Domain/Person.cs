namespace Domain;

public class Person
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Gender { get; private set; }

    public Person(int id, string name, string address, string gender)
    {
        Id = id;
        Name = name;
        Address = address;
        Gender = gender;
    }
}