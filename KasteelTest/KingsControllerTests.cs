using Castle.Core.Resource;
using Kasteel.Controllers;
using Kasteel.DAL.Interfaces;
using Kasteel.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace KasteelTest
{
    public class KingsControllerTests
    {
        [Fact]
        public async void GetAllKings()
        {
            // Arrange
            var mockRepository = new Mock<IKingRepository>();
            mockRepository
                .Setup(m => m.GetAll())
                .Returns(Task.FromResult(new List<King> {
                    new("Test king 1") { Id = 1 },
                    new("Test king 2") { Id = 2 },
                    new("Test king 3") { Id = 3 },
                }));

            var controller = new KingsController(mockRepository.Object);

            // Act
            var kings = (await controller.GetKings()).Value;

            // Assert
            mockRepository.Verify(r => r.GetAll());
            Assert.Equal(3, kings?.Count());
        }

        [Fact]
        public async void GetKing()
        {
            // Arrange
            var mockRepository = new Mock<IKingRepository>();
            mockRepository
                .Setup(m => m.GetByID(1))
                .Returns(Task.FromResult<King?>(new King("Test king") { Id = 1 }));

            var controller = new KingsController(mockRepository.Object);

            // Act
            var king = await controller.GetKing(1);

            // Assert
            mockRepository.Verify(r => r.GetByID(1));
            Assert.Equal("Test king", king.Value?.Name);
        }

        [Fact]
        public async void AddKing()
        {
            // Arrange
            var mockRepository = new Mock<IKingRepository>();
            var controller = new KingsController(mockRepository.Object);

            // Act
            await controller.PostKing(new King("Test king") { Id = 1 });

            // Assert
            mockRepository.Verify(r => r.Insert(It.IsAny<King>()));
            mockRepository.Verify(r => r.Save());
        }

        [Fact]
        public async void UpdateKing()
        {
            // Arrange
            var mockRepository = new Mock<IKingRepository>();
            var controller = new KingsController(mockRepository.Object);

            // Act
            await controller.PutKing(1, new King("Test king") { Id = 1 });

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<King>()));
            mockRepository.Verify(r => r.Save());
        }

        [Fact]
        public async void DeleteKing()
        {
            // Arrange
            var mockRepository = new Mock<IKingRepository>();
            mockRepository
                .Setup(m => m.Delete(1))
                .Returns(Task.FromResult(true));

            var controller = new KingsController(mockRepository.Object);

            // Act
            var result = await controller.DeleteKing(1) as StatusCodeResult;

            // Assert
            mockRepository.Verify(r => r.Delete(1));
            mockRepository.Verify(r => r.Save());
            Assert.Equal(204, result?.StatusCode);
        }
    }
}