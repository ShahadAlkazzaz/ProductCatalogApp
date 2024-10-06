using Resources.Services;
using Resources.Models;
using Moq;
using Resources.Interfaces;


namespace Resources.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IFileService> _mockFileService;
        private readonly ProductService _productService;
        private List<Product> _mockProductList;

        public ProductServiceTests()
        {
            _mockFileService = new Mock<IFileService>();
            _mockProductList = [];

            _mockFileService
                .Setup(fs => fs.GetFromFile())
                .Returns(() =>
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(_mockProductList);
                    return new ResultResponse<string> { Succeeded = true, Result = json };
                });

            _ = _mockFileService
                .Setup(fs => fs.SaveToFile(It.IsAny<string>()))
                .Callback<string>(json =>
                {
                    _mockProductList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json) ?? [];

                })
                .Returns(new ResultResponse<string> { Succeeded = true });


            _productService = new ProductService(_mockFileService.Object);
        }

        [Fact]
        public void AddProduct_ShouldIncreaseProductCount()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Price = 99.99m
            };

            // Act
            _productService.Create(product);
            var products = _productService.GetAll().Result;

            // Assert
            Assert.NotNull(products);
            Assert.Single(products);
        }
    }
}


