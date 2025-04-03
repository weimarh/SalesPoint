using Application.UseCases.Products.Create;
using Application.UseCases.Products.Delete;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Tests.UnitTests.ApplicationUnitTests.Products.Delete
{
    [TestClass]
    public class DeleteProductCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleDeleteProduct_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new DeleteProductCommandHandler(mockProductRepository.Object, mockUnitOfWork.Object);

            //arrange
            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            var price = Price.Create("25.50");
            if (price == null)
            {
                throw new ArgumentException("Invalid price format");
            }

            var productId = Guid.NewGuid();

            var product = new Product
            (
                new ProductId(productId),
                "ProductName",
                price,
                category.Id,
                category,
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            DeleteProductCommand command = new DeleteProductCommand(productId);

            mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            mockUnitOfWork.Setup(uof => uof.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Unit.Value);

            mockProductRepository.Verify(repo => repo.Remove(product), Times.Once());
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task HandleDeleteProduct_WhenProductDoesNotExist_ShouldReturnNotFoundException()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new DeleteProductCommandHandler(mockProductRepository.Object, mockUnitOfWork.Object);

            //Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());

            mockProductRepository.Setup(repo => repo.GetByIdAsync(new ProductId(Guid.NewGuid())))
                .ReturnsAsync((Product?) null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(ProductErrors.ProductNotFound);
            mockProductRepository.Verify(repo => repo.Remove(It.IsAny<Product>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
        }
    }
}
