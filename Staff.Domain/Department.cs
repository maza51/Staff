using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Staff.Domain;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    [XmlIgnore]
    public ICollection<Employee>? Employees { get; set; }
}