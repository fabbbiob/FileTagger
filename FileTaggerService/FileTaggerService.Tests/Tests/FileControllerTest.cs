using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerService.Controllers;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FileTaggerService.Tests.Tests
{
    [TestFixture]
    public class FileControllerTest
    {
        [Test]
        public void CanGetFileByTagId()
        {
            File[] expected = 
            {
                new File { Id = 1 },
                new File { Id = 2 }
            };        

            Mock<IFileRepository> mock = new Mock<IFileRepository>();
            mock.Setup(f => f.GetByTag(It.IsAny<int>()))
                .Returns(expected);

            FileController controller = new FileController(mock.Object);
            Assert.AreEqual(expected, controller.Get(1));
        }

        [Test]
        public void CanGetFileByTagIds()
        {
            File[] expected =
            {
                new File { Id = 1 },
                new File { Id = 2 }
            };

            Mock<IFileRepository> mock = new Mock<IFileRepository>();
            mock.Setup(f => f.GetByTags(It.IsAny<int[]>()))
                .Returns(expected);

            FileController controller = new FileController(mock.Object);
            Assert.AreEqual(expected, controller.Get(new [] {1, 2}));
        }

        [Test]
        public void CanGetFileByFileName()
        {
            File expected = new File { Id = 1 };

            Mock<IFileRepository> mock = new Mock<IFileRepository>();
            mock.Setup(f => f.GetByFilename(It.IsAny<string>()))
                .Returns(expected);

            FileNameController controller = new FileNameController(mock.Object);
            Assert.AreEqual(expected, controller.GetByFilename(""));
        }

        [Test]
        public void CanAddFile()
        {
            Mock<IFileRepository> mock = new Mock<IFileRepository>();
            File fileToAdd = new File { FilePath = "" };
            FileController controller = new FileController(mock.Object);

            controller.Post(fileToAdd);
        }

        [Test]
        public void CanUpdateFile()
        {
            Mock<IFileRepository> mock = new Mock<IFileRepository>();
            File fileToUpdate = new File { Id = 1, FilePath = "" };
            FileController controller = new FileController(mock.Object);

            controller.Put(fileToUpdate);
        }

    }
}
