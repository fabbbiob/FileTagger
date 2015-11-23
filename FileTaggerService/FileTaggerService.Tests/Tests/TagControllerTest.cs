using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerService.Controllers;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FileTaggerService.Tests.Tests
{
    [TestFixture]
    public class TagControllerTest
    {
        [Test]
        public void CanGetTagType()
        {
            Tag expected = new Tag { Id = 1, Description = "abc" };
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            mock.Setup(f => f.GetById(1))
                .Returns(expected);

            TagController controller = new TagController(mock.Object);
            Tag tag = controller.Get(1);

            Assert.AreEqual(expected, tag);
        }
        
        [Test]
        public void CanGetAllTags()
        {
            Tag[] expected = 
            {
                new Tag { Id = 1, Description = "abc" },
                new Tag { Id = 2, Description = "def" }
            };
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            mock.Setup(f => f.GetAll())
                .Returns(expected);

            TagController controller = new TagController(mock.Object);
            Tag[] tags = controller.Get().ToArray();

            Assert.AreEqual(expected, tags);
        }

        [Test]
        public void CanFailGetTag()
        {
            Tag expected = new Tag { Id = 1, Description = "abc" };
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            mock.Setup(f => f.GetById(1))
                .Returns(expected);

            TagController controller = new TagController(mock.Object);

            Tag tag = controller.Get(2);

            Assert.AreNotEqual(expected, tag);
        }

        [Test]
        public void CanAddTagType()
        {
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            TagController controller = new TagController(mock.Object);

            controller.Post(new Tag { Id = 1, Description = "abc" });
        }

        [Test]
        public void CanUpdateTag()
        {
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            TagController controller = new TagController(mock.Object);

            controller.Put(new Tag { Id = 1, Description = "abc" });
        }

        [Test]
        public void CanDeleteUpdateTag()
        {
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            TagController controller = new TagController(mock.Object);

            controller.Delete(1);
        }

    }
}
