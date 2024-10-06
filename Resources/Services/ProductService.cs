using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services
{
    public class ProductService : IProductService<Product, Product>
    {
        private readonly IFileService _fileService;
        private List<Product> _products;

        public ProductService(IFileService fileService)
        {
            _fileService = fileService;
            _products = [];
            GetAll();
        }

        public ProductService(string filePath)
        {
            _fileService = new FileService(filePath);
            _products = [];
            GetAll();
        }

        public ResultResponse<Product> Create(Product product)
        {
            try
            {
                _products.Add(product);
                var json = JsonConvert.SerializeObject(_products);
                var result = _fileService.SaveToFile(json);

                if (result.Succeeded)
                    return new ResultResponse<Product> { Succeeded = true };
                else
                    return new ResultResponse<Product> { Succeeded = false, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Product> { Succeeded = false, Message = ex.Message };
            }
        }

        public ResultResponse<IEnumerable<Product>> GetAll()
        {
            try
            {
                var result = _fileService.GetFromFile();

                if (result.Succeeded)
                {
                    _products = JsonConvert.DeserializeObject<List<Product>>(result.Result!)!;
                    return new ResultResponse<IEnumerable<Product>> { Succeeded = true, Result = _products };
                }
                else
                    return new ResultResponse<IEnumerable<Product>> { Succeeded = false, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<Product>> { Succeeded = false, Message = ex.Message };
            }
        }

        public ResultResponse<Product> Update(string id, Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                var json = JsonConvert.SerializeObject(_products);
                var result = _fileService.SaveToFile(json);

                if (result.Succeeded)
                    return new ResultResponse<Product> { Succeeded = true };
                else
                    return new ResultResponse<Product> { Succeeded = false, Message = result.Message };
            }
            return new ResultResponse<Product> { Succeeded = false, Message = "Product not found." };
        }

        public ResultResponse<Product> Delete(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                var json = JsonConvert.SerializeObject(_products);
                var result = _fileService.SaveToFile(json);

                if (result.Succeeded)
                    return new ResultResponse<Product> { Succeeded = true };
                else
                    return new ResultResponse<Product> { Succeeded = false, Message = result.Message };
            }
            return new ResultResponse<Product> { Succeeded = false, Message = "Product not found." };
        }
    }
}
