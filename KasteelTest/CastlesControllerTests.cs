using Kasteel.Controllers;
using Kasteel.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasteelTest
{
    public class CastlesControllerTests
    {
        [Fact]
        public async void GetAllCastles()
        {
            // Arrange
            var mockRepository = new Mock<ICastleRepository>();
            mockRepository
                .Setup(m => m.GetAll())
                .Returns(Task.FromResult(new List<Kasteel.Models.Castle> {
                    new("Test castle 1") { Id = 1 },
                    new("Test castle 2") { Id = 2 },
                    new("Test castle 3") { Id = 3 },
                }));

            var controller = new CastlesController(mockRepository.Object);

            // Act
            var castles = (await controller.GetCastles()).Value;

            // Assert
            mockRepository.Verify(r => r.GetAll());
            Assert.Equal(3, castles?.Count());
        }

        [Fact]
        public async void GetCastle()
        {
            // Arrange
            var mockRepository = new Mock<ICastleRepository>();
            mockRepository
                .Setup(m => m.GetByID(1))
                .Returns(Task.FromResult<Kasteel.Models.Castle?>(new Kasteel.Models.Castle("Test castle") { Id = 1 }));

            var controller = new CastlesController(mockRepository.Object);

            // Act
            var castle = await controller.GetCastle(1);

            // Assert
            mockRepository.Verify(r => r.GetByID(1));
            Assert.Equal("Test castle", castle.Value?.Name);
        }

        [Fact]
        public async void AddCastle()
        {
            // Arrange
            var mockRepository = new Mock<ICastleRepository>();
            var controller = new CastlesController(mockRepository.Object);

            // Act
            await controller.PostCastle(new Kasteel.Models.Castle("Test castle") { Id = 1 });

            // Assert
            mockRepository.Verify(r => r.Insert(It.IsAny<Kasteel.Models.Castle>()));
            mockRepository.Verify(r => r.Save());
        }

        [Fact]
        public async void UpdateCastle()
        {
            // Arrange
            var mockRepository = new Mock<ICastleRepository>();
            var controller = new CastlesController(mockRepository.Object);

            // Act
            await controller.PutCastle(1, new Kasteel.Models.Castle("Test castle") { Id = 1 });

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<Kasteel.Models.Castle>()));
            mockRepository.Verify(r => r.Save());
        }

        [Fact]
        public async void DeleteCastle()
        {
            // Arrange
            var mockRepository = new Mock<ICastleRepository>();
            mockRepository
                .Setup(m => m.Delete(1))
                .Returns(Task.FromResult(true));

            var controller = new CastlesController(mockRepository.Object);

            // Act
            var result = await controller.DeleteCastle(1) as StatusCodeResult;

            // Assert
            mockRepository.Verify(r => r.Delete(1));
            mockRepository.Verify(r => r.Save());
            Assert.Equal(204, result?.StatusCode);
        }
    }
}
