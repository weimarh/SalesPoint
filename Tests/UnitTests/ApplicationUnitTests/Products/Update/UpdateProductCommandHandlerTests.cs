using Application.UseCases.Products.Update;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Tests.UnitTests.ApplicationUnitTests.Products.Update
{
    [TestClass]
    public class UpdateProductCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleUpdateProduct_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new UpdateProductCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object ,mockProductRepository.Object);

            //Arrange

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            var productId = Guid.NewGuid();
            var price = Price.Create("25.50");
            if (price == null)
            {
                throw new ArgumentException("Invalid price format");
            }

            var product = new Product
            (
                new ProductId(productId),
                "ProductName",
                price,
                category.Id,
                category,
                "ProductDescription",
                "ProductThumbnailPath"
            );

            var command = new UpdateProductCommand
            (
                productId,
                "ProductName",
                "25.50",
                category.Id.Value,
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.ExistsAsync(category.Id))
                .ReturnsAsync(true);
            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);
            mockProductRepository.Setup(repo => repo.ExistsAsync(new ProductId(productId)))
                .ReturnsAsync(true);
            mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id))
                .ReturnsAsync(product);
            mockUnitOfWork.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            mockProductRepository.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
