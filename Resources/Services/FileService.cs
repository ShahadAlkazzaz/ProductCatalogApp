using Resources.Interfaces;
using Resources.Models;


namespace Resources.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath;

        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        public ResultResponse<string> SaveToFile(string content)
        {
            try
            {
                File.WriteAllText(_filePath, content);
                return new ResultResponse<string> { Succeeded = true, Result = "File saved successfully." };
            }
            catch (Exception ex)
            {
                return new ResultResponse<string> { Succeeded = false, Message = ex.Message };
            }
        }

        public ResultResponse<string> GetFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var content = File.ReadAllText(_filePath);
                    return new ResultResponse<string> { Succeeded = true, Result = content };
                }
                return new ResultResponse<string> { Succeeded = false, Message = "File not found." };
            }
            catch (Exception ex)
            {
                return new ResultResponse<string> { Succeeded = false, Message = ex.Message };
            }
        }
    }
}
