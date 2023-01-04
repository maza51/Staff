using System.Threading.Tasks;

namespace Staff.Wpf.Services;

public interface IImporter<T>
{
    Task<T> ImportAsync(string path);
}
