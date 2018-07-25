using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServicesTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;
        private VideoService _videoService;

        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }


        [Test]
        public void GetUnprocessedVideosAsCSV_AllVideosAreProcessed_ReturnEmptyString()
        {
           _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

           var results =  _videoService.GetUnprocessedVideosAsCsv();

           Assert.That(results, Is.EqualTo(""));

        }


        [Test]
        public void GetUnprocessedVideosAsCSV_SomeVideosAreProcessed_ReturnUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video() { Id = 1, Title = "House on Haunted Hill"  },
                new Video() { Id = 2, Title = "Shrek"},
                new Video() { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

    }
}
