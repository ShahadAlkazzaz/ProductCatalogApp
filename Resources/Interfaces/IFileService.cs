using Resources.Models;

namespace Resources.Interfaces
{
    public interface IFileService
    {
        ResultResponse<string> SaveToFile(string content);
        ResultResponse<string> GetFromFile();
    }
}
