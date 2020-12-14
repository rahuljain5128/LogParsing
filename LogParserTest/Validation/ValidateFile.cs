using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;

namespace LogParserTest
{
    [TestClass]
    public class ValidateFile
    {
        [TestMethod]
        public void ValidateFile_NullPath_ThrowsException()
        {
            string path = null;
            var validationObj = new Validation();
            //var result = validationObj.ValidateFile(path);
        }

        [TestMethod]
        public void ValidateFile_InvalidPathExtension_ThrowsException()
        {
            string path = "abc.txt";
            var validationObj = new Validation();
            //var result = validationObj.ValidateFile(path);
        }

        [TestMethod]
        public void ValidateFile_EmptyFile_ThrowsException()
        {
            
        }

        [TestMethod]
        public void ValidateFile_ValidFile_ReturnsTrue()
        {
            string path = @"..\..\..\abc.csv";
            var validationObj = new Validation();
            var result = validationObj.ValidateFile(path);
            Assert.IsTrue(result);
        }
    }
}
