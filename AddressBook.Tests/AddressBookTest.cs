using System.Collections.Generic;
using System.Linq;
using AddressBook.Core;
using AddressBook.Dal;
using AddressBook.Service.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressBook.Tests
{
    [TestClass]
    public class AddressBookTest
    {
        private static IDataAccessor _accessor;
        private static AddressBookController _controller;
        private static Entities.AddressBook _testAddressBook;

        [ClassInitialize]
        public static void ClassLevelInitialization(TestContext context)
        {
            _accessor = new MemoryAccessor();
            _controller = new AddressBookController(_accessor);
            _testAddressBook = new Entities.AddressBook() { Name = "testName", Email = "testEmail", Phone = "testPhone" };
        }

        [TestInitialize]
        public void TestLevelInitialization()
        {
            _accessor.Clear();
        }

        [TestMethod]
        public void TestInitialLoadReturnsEmptyList()
        {
            List<Entities.AddressBook> addressBooks = _controller.Get().ToList();
            Assert.AreEqual(0, addressBooks.Count, "Initial load does not return empty list");
        }

        [TestMethod]
        public void TestNewItemIsSavedCorrectlyByAddAction()
        {
            _controller.Add(_testAddressBook.Name, _testAddressBook.Email, _testAddressBook.Phone);
            Entities.AddressBook addressBook = _accessor.GetByName(_testAddressBook.Name);
            Assert.IsNotNull(addressBook);
            CompareAddressItems(_testAddressBook, addressBook);
        }

        [TestMethod]
        public void TestItemIsRetrievedCorrectlyByGetByNameActionWhenExists()
        {
            _controller.Add(_testAddressBook.Name, _testAddressBook.Email, _testAddressBook.Phone);
            Entities.AddressBook addressBook = _controller.GetByName(_testAddressBook.Name);

            Assert.IsNotNull(addressBook);
            CompareAddressItems(_testAddressBook, addressBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemAlreadyExistsException))]
        public void TestAddingTheSameItemTwiceThrowsException()
        {
            _controller.Add(_testAddressBook.Name, _testAddressBook.Email, _testAddressBook.Phone);
            _controller.Add(_testAddressBook.Name, _testAddressBook.Email, _testAddressBook.Phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void TestLookingForInexistingItemThrowsException()
        {
            _controller.GetByName(_testAddressBook.Name);
        }

        [TestMethod]
        public void TestDeleteActionRemovesItemFromStorage()
        {
            _controller.Add(_testAddressBook.Name, _testAddressBook.Email, _testAddressBook.Phone);
            _controller.Delete(_testAddressBook.Name);
            Assert.AreEqual(0, _accessor.GetAll().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void TestDeletingNonExistingItemThrowsException()
        {
            _controller.Delete(_testAddressBook.Name);
        }

        private void CompareAddressItems(Entities.AddressBook expected, Entities.AddressBook actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Stored item does not have the same name");
            Assert.AreEqual(expected.Email, actual.Email, "Stored item does not have the same email");
            Assert.AreEqual(expected.Phone, actual.Phone, "Stored item does not have the same phone");
        }
    }
}
