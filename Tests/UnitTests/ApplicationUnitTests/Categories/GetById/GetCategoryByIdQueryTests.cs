using Application.UseCases.Categories.Common;
using Application.UseCases.Categories.GetAll;
using Application.UseCases.Categories.GetById;
using Domain.Entities.Categories;

namespace Tests.UnitTests.ApplicationUnitTests.Categories.GetById
{
    [TestClass]
    public class GetCategoryByIdQueryTests
    {
        [TestMethod]
        public async Task HandleGetCategoryById_WhenInputIsCorrect_ShouldReturnCategory()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new GetCategoryByIdQueryHandler(mockCategoryRepository.Object);

            var categoryId = Guid.NewGuid();
            var category = new Category
            (
                new CategoryId(categoryId),
                "TestName",
                "TestDescription"
            );

            var command = new GetCategoryByIdQuery(category.Id.Value);

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync(category);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().BeOfType<CategoryResponse>();
            Assert.AreEqual(result.Value.Id, category.Id.Value);
        }

        [TestMethod]
        public async Task HandleGetCategoryById_WhenCategoryDoesNotExist_ShouldReturnNotFoundException()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new GetCategoryByIdQueryHandler(mockCategoryRepository.Object);

            var categoryId = Guid.NewGuid();
            
            var command = new GetCategoryByIdQuery(categoryId);

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(categoryId)))
                .ReturnsAsync((Category?)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.NotFound);
            result.FirstError.Code.Should().Be(CategoryErrors.CategoryNotFound.Code);
            result.FirstError.Description.Should().Be(CategoryErrors.CategoryNotFound.Description);
        }
    }
}
