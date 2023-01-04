using System.Collections.Generic;
using System.Threading.Tasks;
using Staff.Domain;
using System.Xml;

namespace Staff.Wpf.Services;

public class XmlEmployeeExporter : IExporter<List<Employee>>
{
    public async Task ExportAsync(List<Employee> employees, string path)
    {
        //XmlSerializer xmlSerializer = new XmlSerializer(employees.GetType(), new XmlRootAttribute("Export"));
        //await using FileStream stream = File.Create(@"" + path + ".xml");
        //xmlSerializer.Serialize(stream, employees);

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Async = true;

        using XmlWriter writer = XmlWriter.Create(path + ".xml", settings);

        writer.WriteStartElement("EXPORT");

        foreach (var employee in employees)
        {
            writer.WriteStartElement("EMPLOYEE");
            writer.WriteElementString("DEPARTMENT", employee.Department?.Name ?? string.Empty);
            writer.WriteStartElement("DATA");
            writer.WriteElementString("FIRSTNAME", employee.FirstName);
            writer.WriteElementString("MIDDLENAME", employee.MiddleName);
            writer.WriteElementString("LASTNAME", employee.LastName);
            writer.WriteElementString("DATEBIRTH", employee.DateBirth.ToString("dd.MM.yyyy"));
            writer.WriteElementString("POSITION", employee.Position);
            writer.WriteElementString("PHONE", employee.Phone);
            writer.WriteElementString("EMAIL", employee.Email);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        await writer.FlushAsync();
    }
}