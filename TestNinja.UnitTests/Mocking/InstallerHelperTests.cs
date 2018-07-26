using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking;
using System.Net;

namespace TestNinja.UnitTests.Mocking
{

    [TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void setup()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

       [Test]
       public void DownloadInstaller_DownloadPass_returnTrue()
       {
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Equals(true);

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);

       }


        [Test]
        public void DownloadInstaller_DownloadFails_returnFalse()
        {

            _fileDownloader.Setup(fd =>
                  fd.DownloadFile(It.IsAny<string>(),It.IsAny<string>()))
                 .Throws<WebException>();


            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);

           


        }



    }
}
