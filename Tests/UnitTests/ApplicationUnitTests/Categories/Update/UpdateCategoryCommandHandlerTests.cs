using Application.UseCases.Categories.Update;
using Domain.Entities.Categories;
using Domain.Primitives;

namespace Tests.UnitTests.ApplicationUnitTests.Categories.Update
{
    [TestClass]
    public class UpdateCategoryCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleUpdateCategory_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new UpdateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            (
                categoryId,
                "UpdatedName",
                "UpdatedDescription"
            );

            var category = new Category
            (
                new CategoryId( categoryId ),
                "CategoryName",
                "CategoryDescription"
            );

            mockCategoryRepository.Setup(repo => repo.ExistsAsync(new CategoryId(categoryId)))
                .ReturnsAsync( true );
            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync(category);
            mockUnitOfWork.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            mockCategoryRepository.Verify(repo => repo.Update(It.IsAny<Category>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task HandleUpdateCategory_WhenCategoryNameIsEmptyString_ShouldReturnValidationError()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new UpdateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            (
                categoryId,
                "",
                "UpdatedDescription"
            );

            var category = new Category
            (
                new CategoryId(categoryId),
                "CategoryName",
                "CategoryDescription"
            );

            mockCategoryRepository.Setup(repo => repo.ExistsAsync(new CategoryId(categoryId)))
                .ReturnsAsync(true);
            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync(category);
            mockUnitOfWork.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(CategoryErrors.EmptyName.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.EmptyName.Description);
        }

        [TestMethod]
        public async Task HandleUpdateCategory_WhenCategoryDescriptionIsTooLong_ShouldReturnValidationError()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new UpdateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            (
                categoryId,
                "UpdatedName",
                "UpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescription" +
                "UpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescriptionUpdatedDescription"
            );

            var category = new Category
            (
                new CategoryId(categoryId),
                "CategoryName",
                "CategoryDescription"
            );

            mockCategoryRepository.Setup(repo => repo.ExistsAsync(new CategoryId(categoryId)))
                .ReturnsAsync(true);
            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync(category);
            mockUnitOfWork.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(CategoryErrors.DescriptionTooLong.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.DescriptionTooLong.Description);
        }

        [TestMethod]
        public async Task HandleUpdateCategory_WhenCategoryDoesNotExist_ShouldReturnNotFoundException()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new UpdateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            var categoryId = Guid.NewGuid();
            var category = new UpdateCategoryCommand
            (
                categoryId,
                "CategoryName",
                "CategoryDescription"
            );

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync((Category?)null); ;

            //Act
            var result = await handler.Handle(category, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.NotFound);
            result.FirstError.Code.Should().Be(CategoryErrors.CategoryNotFound.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.CategoryNotFound.Description);
        }
    }
}
