using Microsoft.VisualStudio.TestTools.UnitTesting;
using IiaTdd.routes; // Assure-toi d'importer ton namespace
using System;
using IiaTdd.cs.Author;
using IiaTdd.cs.format;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;

namespace IiaTddTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestController_Get_ReturnsTrue()
        {
            var controller = new TestController();
            bool result = controller.Get();
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("1234567890", 10)]
        [DataRow("1234567890123", 13)]
        [DataRow("1234-567890123", 13)]
        [DataRow("123456789X", 10)]
        [DataRow("12345678 0X", 10)]
        public void CheckTypeIsbn_Valid(string isbn, int expectedLength)
        {
            Assert.AreEqual(expectedLength, CheckIsbn.TenOrThirteen(isbn));
        }

        [DataTestMethod]
        [DataRow("123456789A", "ISBN ivalide, le dernier caractère doit être un chiffre ou un X pour un ISBN de 10 chiffres")]
        [DataRow("123456789012345", "ISBN ivalide")]
        [DataRow("1234-56789012A", "ISBN ivalide, doit être un chiffre pour un ISBN de 13 chiffres")]
        public void CheckTypeIsbn_Invalid(string isbn, string expectedMessage)
        {
            var exception = Assert.ThrowsException<Exception>(() => CheckIsbn.TenOrThirteen(isbn));
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [DataTestMethod]
        [DataRow("123456789X", true)]
        [DataRow("1234567890", false)]
        public void ValidIsbn_ValidIsbn10(string isbn, bool expected)
        {
            Assert.AreEqual(expected, CheckIsbnValide.CheckIsbnTen(isbn));
        }

        [DataTestMethod]
        [DataRow("9783161484100", true)]
        [DataRow("9783161484101", false)]
        public void ValidIsbn_ValidIsbn13(string isbn, bool expected)
        {
            Assert.AreEqual(expected, CheckIsbnValide.CheckIsbnThirteen(isbn));
        }

        [DataTestMethod]
        [DataRow("123456789A", "ISBN ivalide, le dernier caractère doit être un chiffre ou un X pour un ISBN de 10 chiffres")]
        [DataRow("123456789012345", "ISBN ivalide")]
        [DataRow("1234-56789012A", "ISBN ivalide, doit être un chiffre pour un ISBN de 13 chiffres")]
        public void ValidIsbn_InvalidIsbn(string isbn, string expectedMessage)
        {
            var exception = Assert.ThrowsException<Exception>(() => CheckIsbnValide.CheckIsbnTen(isbn));
            Assert.AreEqual(expectedMessage, exception.Message);
        }
        [DataTestMethod]
        [DataRow("Jean", "Dupont1")]
        [DataRow("Jea2n", "Dupont-Dupond")]
        [DataRow("Robert", "Dub3ois")]
        public void CheckAuthor_InvalidNameAndFirstName(string name, string firstName)
        {
            var author = new Author() { Name = name, FirstName = firstName };
            var exception = Assert.ThrowsException<Exception>(() => CheckAuthor.CheckAuthorName(author));
            Assert.AreEqual("Nom de l'auteur invalide, il ne doit pas contenir de chiffres", exception.Message);
        }
        
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void CheckFormat_Valid(int i)
        {
            Format.CheckFormatEnum(i);
        }
        [DataTestMethod]
        [DataRow(3)]
        public void CheckFormat_Invalid(int i)
        {
            var exception = Assert.ThrowsException<Exception>(() => Format.CheckFormatEnum(i));
            Assert.AreEqual("Format invalide", exception.Message);
        }
        
        
    }
}
