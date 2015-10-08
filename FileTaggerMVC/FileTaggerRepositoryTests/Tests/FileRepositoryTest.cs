using System;
using System.Data.SQLite;
using System.Linq;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories;
using FileTaggerRepository.Repositories.Impl.Simple;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class FileRepositoryTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            DbCreator.CreateDatabase();
        }

        [Test]
        public void CanAddFile()
        {
            FileRepository repo = new FileRepository();
            repo.Add(new File
            {
                FilePath = Guid.NewGuid().ToString()
            });
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailSamePath()
        {
            FileRepository repo = new FileRepository();
            const string filePath = "1";
            repo.Add(new File
            {
                FilePath = filePath
            });
            repo.Add(new File
            {
                FilePath = filePath
            });
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailWithNullDescription()
        {
            FileRepository repo = new FileRepository();
            repo.Add(new File());
        }

        [Test]
        public void CanGetFile()
        {
            FileRepository repo = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString()
            };
            repo.Add(file);

            Assert.AreEqual(file, repo.GetById(file.Id));
        }

        [Test]
        public void CanUpdateFile()
        {
            FileRepository repo = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString()
            };
            repo.Add(file);

            file.FilePath = Guid.NewGuid().ToString();

            Assert.AreEqual(file, repo.GetById(file.Id));
        }

        [Test]
        public void CanDeleteFile()
        {
            FileRepository repo = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString()
            };
            repo.Add(file);

            repo.Delete(file);

            Assert.Null(repo.GetById(file.Id));
        }

        [Test]
        public void CanGetAllFiles()
        {
            FileRepository repo = new FileRepository();
            File file1 = new File { FilePath = Guid.NewGuid().ToString() };
            File file2 = new File { FilePath = Guid.NewGuid().ToString() };
            repo.Add(file1);
            repo.Add(file2);

            File[] files = repo.GetAll().ToArray();

            Assert.True(files.Contains(file1));
            Assert.True(files.Contains(file2));
        }
    }
}
