using Microsoft.VisualStudio.TestTools.UnitTesting;
using IiaTdd.routes; // Assure-toi d'importer ton namespace
using System;
using IiaTdd.cs;
using IiaTdd.cs.Author;
using IiaTdd.cs.Book;
using IiaTdd.cs.format;
using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;
using IiaTddTest.Fake;
using Microsoft.Extensions.Configuration;
using Moq;
using MySql.Data.MySqlClient;
using PostBook = IiaTdd.cs.Book.PostBook;

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
        [DataRow("123456789A",
            "ISBN ivalide, le dernier caractère doit être un chiffre ou un X pour un ISBN de 10 chiffres")]
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
        [DataRow("123456789A",
            "ISBN ivalide, le dernier caractère doit être un chiffre ou un X pour un ISBN de 10 chiffres")]
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
        [DataRow("Jean", "Du pont")]
       
        public void CheckAuthor_InvalidName(string name, string firstName)
        {
            var author = new Author() { Name = name, FirstName = firstName };
            var exception = Assert.ThrowsException<Exception>(() => CheckAuthor.CheckAuthorName(author));
            Assert.AreEqual("Nom de l'auteur invalide, il ne doit pas contenir d'espace", exception.Message);
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

        [DataTestMethod]
        [DataRow("123456789A")]
        [DataRow("123456789012345")]
        [DataRow("1234-56789012A")]
        public void BookWithNullData_Invalid(string isbn)
        {
            // Arrange : On suppose que ces ISBN sont invalides et que les méthodes de validation lanceront une exception.
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);

            // Act & Assert : La validation doit échouer et lancer une exception
            Assert.ThrowsException<Exception>(() => bookService.AutoComplete(isbn));
        }

        [DataTestMethod]
        [DataRow("1234567890", "Le seigneur des anneaux", "Tolkien", "J.R.R", "Christian Bourgois", 0)]
        [DataRow("9781987654321", "Le mystère du passé", "Renoir", "Jean", "Flammarion", 1)]
        [DataRow("9783135792468", "Voyage dans le temps", "Curie", "Marie", "Le Seuil", 2)]
        public void AutoComplete_ShouldReturnBook_WhenBookExists(string isbn, string title, string authorName, string authorFirstName, string editor, int format)
       
        {

            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            IiaTdd.objet.PostBookObj bookObj = new IiaTdd.objet.PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
     
            var result = bookService.AutoComplete(isbn);

        
            Assert.AreEqual(bookObj.Isbn, result.Isbn);
            Assert.AreEqual(bookObj.Title, result.Title);
            Assert.AreEqual(bookObj.Author.Name, result.Author.Name);
            Assert.AreEqual(bookObj.Author.FirstName, result.Author.FirstName);
            Assert.AreEqual(bookObj.Editor, result.Editor);
            Assert.AreEqual(bookObj.Format, result.Format);
            
            
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Aucun livre trouvé")]
        public void AutoComplete_ShouldThrowException_WhenBookNotFound()
        {
          
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);

          
            bookService.AutoComplete("0987654321");
        }
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        
        public void DeleteBook_ShouldReturnTrue(int Id)
        {
            
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var delete = new DeleteBook(fakeRepo);
            var result = delete.DeleteBookIdRep(Id);
            Assert.IsTrue(result);
        }
        [DataTestMethod]
        [DataRow(4)]
        [ExpectedException(typeof(Exception), "Aucun livre trouvé")]
        public void DeleteBook_ShouldThrowException_WhenBookNotFound(int id)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var delete = new DeleteBook(fakeRepo);
            delete.DeleteBookIdRep(id);
        }
        [DataTestMethod]
        [DataRow(1, "1234567890", "Le seigneur des anneaux", "Tolkien", "gJNKDqskjfq", "Christian Bourgois", 0)]
        [DataRow(2, "9781987654321", "Le mystère du passé", "EAZEAZ", "Jean", "Flammarion", 1)]
        [DataRow(3, "9783135792468", "Temps dans le voyage", "Curie", "Marie", "Le Seuil", 2)]
        public void UpdateBook_ShouldReturnTrue(int id, string isbn, string title, string authorName, string authorFirstName, string editor, int format)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var update = new UpdateBook(fakeRepo);
            var book = new IiaTdd.objet.PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
            var result = update.UpdateBookIdRep(id, book);
            Assert.IsTrue(result);
        }
        
        [DataTestMethod]
        [DataRow(4, "1234567890", "Le seigneur des anneaux", "Tolkien", "J.R.R", "Christian Bourgois", 0)]
        [ExpectedException(typeof(Exception), "Aucun livre trouvé")]
        public void UpdateBook_ShouldThrowException_WhenBookNotFound(int id, string isbn, string title, string authorName, string authorFirstName, string editor, int format)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var update = new UpdateBook(fakeRepo);
            var book = new IiaTdd.objet.PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
            update.UpdateBookIdRep(id, book);
        }
        //methode post qui fonctionne 
        [TestMethod]
        [DataRow("1234567890", "Le seigneur des anneaux", "Tolkien", "J.R.R", "Christian Bourgois", 0)]
        [DataRow("1234-567890123", "Le mystère du passé", "Renoir", "Jean", "Flammarion", 1)]
        [DataRow("9783161484101", "Voyage dans le temps", "Curie", "Marie", "Le Seuil", 2)]
        public void PostBook_ShouldReturnTrue(string isbn, string title, string authorName, string authorFirstName, string editor, int format)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var book = new PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
             bookService.AddBook(book);
            
        }
        [DataTestMethod]
        [DataRow("1234567890", "Le seigneur des anneaux", "Tolkien",  null, "Christian Bourgois", 0)]
        [DataRow("9783135792468", null, "Renoir", "Jean", "Flammarion", 1)]
        public void ShouldBeAutoCompleted(string isbn, string title, string authorName, string authorFirstName, string editor, int format)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var book = new PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
            bookService.AddBook(book);
        }
        [DataTestMethod]
        [DataRow(null, "Le seigneur des anneaux", "Tolkien",  null, "Christian Bourgois", 0)]
        [ExpectedException(typeof(Exception), "Isbn invalide")]
        public void PostBook_ShouldThrowException_WhenIsbnIsInvalid(string isbn, string title, string authorName, string authorFirstName, string editor, int format)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var book = new PostBookObj()
            {
                Isbn = isbn,
                Title = title,
                Author = new Author
                {
                    Name = authorName,
                    FirstName = authorFirstName
                },
                Editor = editor,
                Format = (FormatEnum)format
            };
            bookService.AddBook(book);
        }
        
        [DataTestMethod]
        [DataRow("Tolkien", "J.R.R")]
        [DataRow("Renoir", "Jean")]
        [DataRow("Curie", "Marie")]
        public void GetBookByAuthor_ShouldReturnBook(string name, string firstName)
        {
            IBookRepository fakeRepo = new FakeBookRepository();
            var bookService = new PostBook(fakeRepo);
            var author = new Author()
            {
                Name = name,
                FirstName = firstName
            };
            var result = bookService.GetBookByAuthor(author);
            Assert.IsNotNull(result);
        }
        
        
        
        
        
    }
    
}
