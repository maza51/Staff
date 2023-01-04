using Staff.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

/*

Format

<EXPORT>
    <EMPLOYEE>
        <DEPARTMENT>Бухгалтерия</DEPARTMENT>
        <DATA>
            <FIRSTNAME>Антонина</FIRSTNAME>
            <MIDDLENAME>Валерьевна</MIDDLENAME>
            <LASTNAME>Петрова</LASTNAME>
            <DATEBIRTH>18.08.1981</DATEBIRTH>
            <POSITION>Главный бухгалтер</POSITION>
            <PHONE>79819878567</PHONE>
            <EMAIL>petrova@goodcompany.com</EMAIL>
        </DATA>
    </EMPLOYEE>
    <EMPLOYEE>
        <DEPARTMENT>Отдел программной разработки</DEPARTMENT>
        <DATA>
            <FIRSTNAME>Евгений</FIRSTNAME>
            <MIDDLENAME>Васильевич</MIDDLENAME>
            <LASTNAME>Иващенко</LASTNAME>
            <DATEBIRTH>10.01.1995</DATEBIRTH>
            <POSITION>Инженер</POSITION>
            <PHONE>79819870517</PHONE>
            <EMAIL>ivashenko@goodcompany.com</EMAIL>
        </DATA>
    </EMPLOYEE>
</EXPORT>

*/

namespace Staff.Wpf.Services;

public class XmlEmployeeImporter : IImporter<List<Employee>>
{
    public async Task<List<Employee>> ImportAsync(string path)
    {
        var employees = new List<Employee>();

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Async = true;

        using XmlReader reader = XmlReader.Create(path, settings);

        while (reader.ReadToFollowing("EMPLOYEE"))
        {
            var employee = new Employee();

            while (reader.Read() && reader.Name != "EMPLOYEE")
            {
                switch (reader.Name)
                {
                    case "DEPARTMENT":
                        employee.Department = new Department { Name = reader.ReadString() };
                        break;
                    case "FIRSTNAME":
                        employee.FirstName = reader.ReadString();
                        break;
                    case "MIDDLENAME":
                        employee.MiddleName = reader.ReadString();
                        break;
                    case "LASTNAME":
                        employee.LastName = reader.ReadString();
                        break;
                    case "DATEBIRTH":
                        employee.DateBirth = DateTime.ParseExact(reader.ReadString(), "dd.MM.yyyy", null);
                        break;
                    case "PHONE":
                        employee.Phone = reader.ReadString();
                        break;
                    case "EMAIL":
                        employee.Email = reader.ReadString();
                        break;
                    case "POSITION":
                        employee.Position = reader.ReadString();
                        break;
                }
            }

            employees.Add(employee);
        }

        return employees;
    }
}
