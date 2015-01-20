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
        private static Entities.AddressBook _testAddressItem;

        [ClassInitialize]
        public static void ClassLevelInitialization(TestContext context)
        {
            _accessor = new MemoryAccessor();
            _controller = new AddressBookController(_accessor);
            _testAddressItem = new Entities.AddressBook() { Name = "testName", Email = "testEmail", Phone = "testPhone" };
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
            _controller.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
            Entities.AddressBook addressBook = _accessor.GetByName(_testAddressItem.Name);
            Assert.IsNotNull(addressBook);
            CompareAddressItems(_testAddressItem, addressBook);
        }

        [TestMethod]
        public void TestItemIsRetrievedCorrectlyByGetByNameActionWhenExists()
        {
            _controller.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
            Entities.AddressBook addressBook = _controller.GetByName(_testAddressItem.Name);

            Assert.IsNotNull(addressBook);
            CompareAddressItems(_testAddressItem, addressBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemAlreadyExistsException))]
        public void TestAddingTheSameItemTwiceThrowsException()
        {
            _controller.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
            _controller.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void TestLookingForInexistingItemThrowsException()
        {
            _controller.GetByName(_testAddressItem.Name);
        }

        [TestMethod]
        public void TestDeleteActionRemovesItemFromStorage()
        {
            _controller.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
            _controller.Delete(_testAddressItem.Name);
            Assert.AreEqual(0, _accessor.GetAll().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void TestDeletingNonExistingItemThrowsException()
        {
            _controller.Delete(_testAddressItem.Name);
        }

        [TestMethod]
        public void TestAddingItemUsingDefaultControllersConstructorWorks()
        {
            var defaultController = new AddressBookController();
            defaultController.Add(_testAddressItem.Name, _testAddressItem.Email, _testAddressItem.Phone);
            var actualAddressItem = defaultController.GetByName(_testAddressItem.Name);
            CompareAddressItems(_testAddressItem, actualAddressItem);
        }

        private void CompareAddressItems(Entities.AddressBook expected, Entities.AddressBook actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Stored item does not have the same name");
            Assert.AreEqual(expected.Email, actual.Email, "Stored item does not have the same email");
            Assert.AreEqual(expected.Phone, actual.Phone, "Stored item does not have the same phone");
        }
    }
}
