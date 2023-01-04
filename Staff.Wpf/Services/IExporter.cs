using System.Threading.Tasks;

namespace Staff.Wpf.Services;

public interface IExporter<T>
{
    Task ExportAsync(T objs, string path);
}
