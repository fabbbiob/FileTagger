﻿using System.Data.SQLite;
using System.Linq;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories;
using FileTaggerRepository.Repositories.Impl.Simple;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.TagTypeTests
{
    [TestFixture]
    public class TagTypeRepositoryTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            DbCreator.DeleteDatabase();
            DbCreator.CreateDatabase();
        }

        [Test]
        public void CanAddTagType()
        {
            TagTypeRepository repo = new TagTypeRepository();
            repo.Add(new TagType
            {
                Description = "tag type"
            });
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailWithNullDescription()
        {
            TagTypeRepository repo = new TagTypeRepository();
            repo.Add(new TagType());
        }

        [Test]
        public void CanGetTagType()
        {
            TagTypeRepository repo = new TagTypeRepository();
            TagType tagType = new TagType
            {
                Description = "tag type"
            };
            repo.Add(tagType);

            Assert.AreEqual(tagType, repo.GetById(tagType.Id));
        }

        [Test]
        public void CanUpdateTagType()
        {
            TagTypeRepository repo = new TagTypeRepository();
            TagType tagType = new TagType
            {
                Description = "tag type"
            };
            repo.Add(tagType);

            const string newDescription = "newDescription";
            tagType.Description = newDescription;
            repo.Update(tagType);

            Assert.AreEqual(newDescription, repo.GetById(tagType.Id).Description);
        }

        [Test]
        public void CanDeleteTagType()
        {
            TagTypeRepository repo = new TagTypeRepository();
            TagType tagType = new TagType
            {
                Description = "tag type"
            };
            repo.Add(tagType);

            repo.Delete(tagType);

            Assert.Null(repo.GetById(tagType.Id));
        }

        [Test]
        public void CanGetAllTagTypes()
        {
            TagTypeRepository repo = new TagTypeRepository();
            TagType tagType1 = new TagType {Description = "tt1"};
            TagType tagType2 = new TagType { Description = "tt2" };
            repo.Add(tagType1);
            repo.Add(tagType2);

            TagType[] tagTypes = repo.GetAll().ToArray();
            
            Assert.AreEqual(tagType1, tagTypes[0]);
            Assert.AreEqual(tagType2, tagTypes[1]);
        }
    }
}
