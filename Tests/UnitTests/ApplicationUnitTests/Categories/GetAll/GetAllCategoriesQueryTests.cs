using Application.UseCases.Categories.GetAll;
using Domain.Entities.Categories;

namespace Tests.UnitTests.ApplicationUnitTests.Categories.GetAll
{
    [TestClass]
    public class GetAllCategoriesQueryTests
    {
        [TestMethod]
        public async Task HandleGetAllCategories_ShouldReturnAllCategories()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var handler = new GetAllCategoriesQueryHandler(mockCategoryRepository.Object);

            var categories = new List<Category>
            {
                new Category
                (
                    new CategoryId(Guid.NewGuid()),
                    "TestName1",
                    "TestDescription1"
                ),
                new Category
                (
                    new CategoryId(Guid.NewGuid()),
                    "TestName2",
                    "TestDescription2"
                )
            };

            mockCategoryRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(categories);

            //Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().HaveCount(2);
        }
    }
}
