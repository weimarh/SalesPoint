using Application.UseCases.Categories.Delete;
using Domain.Entities.Categories;
using Domain.Primitives;

namespace Tests.UnitTests.ApplicationUnitTests.Categories.Delete
{
    [TestClass]
    public class DeleteCategoryCommandHandlerTests
    {
        [TestMethod]
        public async Task HandleDeleteCategory_WhenInputIsCorrect_ShouldReturnSuccess()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new DeleteCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);

            //Arrange
            var CategoryID = Guid.NewGuid();
            var category = new Category
            (
                new CategoryId(CategoryID),
                "CategoryName",
                "CategoryDescription"
            );

            DeleteCategoryCommand command = new DeleteCategoryCommand(CategoryID);

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(CategoryID)))
                .ReturnsAsync(category);

            mockUnitOfWork.Setup(uof => uof.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Unit.Value);

            mockCategoryRepository.Verify(repo => repo.Remove(category), Times.Once());
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [TestMethod]
        public async Task HandleDeleteCategory_WhenCategoryDoesNotExist_ShouldReturnNotFoundException()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new DeleteCategoryCommandHandler(mockUnitOfWork.Object, mockCategoryRepository.Object);


            //Arrange
            var command = new DeleteCategoryCommand(Guid.NewGuid());

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(new CategoryId(Guid.NewGuid())))
                .ReturnsAsync((Category?) null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(CategoryErrors.CategoryNotFound);
            mockCategoryRepository.Verify(repo => repo.Remove(It.IsAny<Category>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
        }
    } 
}
