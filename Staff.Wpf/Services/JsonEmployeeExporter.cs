using Staff.Domain;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Staff.Wpf.Services;

public class JsonEmployeeExporter : IExporter<List<Employee>>
{
    public async Task ExportAsync(List<Employee> employees, string path)
    {
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        await using FileStream stream = File.Create(@"" + path + ".json");
        await JsonSerializer.SerializeAsync(stream, employees, options);
    }
}
