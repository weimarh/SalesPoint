using Application.UseCases.Products.Create;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Primitives;

namespace Tests.UnitTests.ApplicationUnitTests.Products.Create
{
    [TestClass]
    public class CreateProductCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleCreateProduct_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new CreateProductCommandHandler(mockUnitOfWork.Object, mockProductRepository.Object, mockCategoryRepository.Object);

            //Arrange

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            CreateProductCommand command = new CreateProductCommand
            (
                "ProductName",
                "25.50",
                category.Id.Value,
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeOfType<Unit>();
            mockProductRepository.Setup(repo => repo.AddAsync(
                It.Is<Product>(product =>
                    product.ProductName == command.ProductName &&
                    product.Description == command.Description &&
                    product.ThumbnailUrl == command.ThumbnailPath &&
                    product.CategoryId.Value == command.CategoryId
            ))).Returns(Task.CompletedTask);
        }

        [TestMethod]
        public async Task HandleCreateProduct_WhenProductNameIsEmptyString_ShouldReturnValidationError()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new CreateProductCommandHandler(mockUnitOfWork.Object, mockProductRepository.Object, mockCategoryRepository.Object);

            //Arrange

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            CreateProductCommand command = new CreateProductCommand
            (
                "",
                "25.50",
                category.Id.Value,
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ProductErrors.EmptyName.Code);
            result.FirstError.Description.Should().Be(ProductErrors.EmptyName.Description);
        }

        [TestMethod]
        public async Task HandleCreateProduct_WhenProductDescriptionIsTooLong_ShouldReturnValidationError()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new CreateProductCommandHandler(mockUnitOfWork.Object, mockProductRepository.Object, mockCategoryRepository.Object);

            //Arrange

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            CreateProductCommand command = new CreateProductCommand
            (
                "ProductName",
                "25.50",
                category.Id.Value,
                "productDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescription" +
                "productDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescriptionproductDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ProductErrors.DescriptionTooLong.Code);
            result.FirstError.Description.Should().Be(ProductErrors.DescriptionTooLong.Description);
        }

        [TestMethod]
        public async Task HandleCreateProduct_WhenProductPriceInNotInTheCorrectFormat_ShouldReturnValidationError()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new CreateProductCommandHandler(mockUnitOfWork.Object, mockProductRepository.Object, mockCategoryRepository.Object);

            //Arrange

            var category = new Category
            (
                new CategoryId(Guid.NewGuid()),
                "CategoryName",
                "CategoryDescription"
            );

            CreateProductCommand command = new CreateProductCommand
            (
                "ProductName",
                "25.50A",
                category.Id.Value,
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ProductErrors.PriceWithBadFormat.Code);
            result.FirstError.Description.Should().Be(ProductErrors.PriceWithBadFormat.Description);
        }


        [TestMethod]
        public async Task HandleCreateProduct_WhenCategoryIdDoesNotExists_ShouldReturnNotFoundException()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new CreateProductCommandHandler(mockUnitOfWork.Object, mockProductRepository.Object, mockCategoryRepository.Object);

            //Arrange

            CreateProductCommand command = new CreateProductCommand
            (
                "ProductName",
                "25.50A",
                Guid.NewGuid(),
                "productDescription",
                "ProductThumbnailPath"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(Guid.NewGuid())))
                .ReturnsAsync((Category?)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.NotFound);
            result.FirstError.Code.Should().Be(ProductErrors.CategoryNotFound.Code);
            result.FirstError.Description.Should().Be(ProductErrors.CategoryNotFound.Description);
        }
    }
}
