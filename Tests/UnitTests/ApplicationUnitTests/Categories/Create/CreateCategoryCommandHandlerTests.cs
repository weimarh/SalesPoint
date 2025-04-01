using Application.UseCases.Categories.Create;
using Domain.Entities.Categories;
using Domain.Primitives;

namespace Tests.UnitTests.ApplicationUnitTests.Categories.Create
{
    [TestClass]
    public class CreateCategoryCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleCreateCategory_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            CreateCategoryCommand command = new CreateCategoryCommand
            (
                "CategoryName",
                "CategoryDescription"
            );

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeOfType<Unit>();
            mockCategoryRepository.Setup(repo => repo.AddAsync(
                It.Is<Category>(category =>
                    category.CategoryName == command.CategoryName &&
                    category.Description == command.Description
            ))).Returns(Task.CompletedTask);
        }

        [TestMethod]
        public async Task HandleCreateCategory_WhenCategoryNameIsEmptyString_ShouldReturnValidationError()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            CreateCategoryCommand command = new CreateCategoryCommand
            (
                "",
                "CategoryDescription"
            );

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(CategoryErrors.EmptyName.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.EmptyName.Description);
        }

        [TestMethod]
        public async Task HandleCreateCategory_WhenCategoryDescriptionIsTooLong_ShouldReturnValidationError()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            CreateCategoryCommand command = new CreateCategoryCommand
            (
                "CategoryName",
                "CategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescription" +
                "CategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescriptionCategoryDescription"
            );

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(CategoryErrors.DescriptionTooLong.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.DescriptionTooLong.Description);
        }
    }
}
