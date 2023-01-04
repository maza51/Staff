namespace Staff.Domain;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateBirth { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }
    public Department? Department { get; set; }
}