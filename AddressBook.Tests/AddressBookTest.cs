using System;
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
        private static IDataAccessor accessor;
        private static AddressBookController controller;
        private static Entities.AddressBook testAddressBook;

        [ClassInitialize]
        public static void MyTestInitialize(TestContext context)
        {
            accessor = new MemoryAccessor();
            controller = new AddressBookController(accessor);
            testAddressBook = new Entities.AddressBook() { Name = "testName", Email = "testEmail", Phone = "testPhone" };
        }

        [TestInitialize]
        public void MyInit()
        {
            accessor.Clear();
        }

        [TestMethod]
        public void TestInitialLoadReturnsEmptyList()
        {
            List<Entities.AddressBook> addressBooks = controller.Get().ToList();
            Assert.AreEqual(0, addressBooks.Count, "Initial load does not return empty list");
        }

        [TestMethod]
        public void TestNewItemIsSavedCorrectlyByAddAction()
        {
            controller.Add(testAddressBook.Name, testAddressBook.Email, testAddressBook.Phone);
            Entities.AddressBook addressBook = accessor.GetByName(testAddressBook.Name);
            Assert.IsNotNull(addressBook);
            CompareAddressItems(testAddressBook, addressBook);
        }

        [TestMethod]
        public void TestItemIsRetrievedCorrectlyByGetByNameActionWhenExists()
        {
            controller.Add(testAddressBook.Name, testAddressBook.Email, testAddressBook.Phone);
            Entities.AddressBook addressBook = controller.GetByName(testAddressBook.Name);

            Assert.IsNotNull(addressBook);
            CompareAddressItems(testAddressBook, addressBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemAlreadyExistsException))]
        public void TestAddingTheSameItemTwiceThrowsException()
        {
            controller.Add(testAddressBook.Name, testAddressBook.Email, testAddressBook.Phone);
            controller.Add(testAddressBook.Name, testAddressBook.Email, testAddressBook.Phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void TestLookingForInexistingItemThrowsException()
        {
            controller.GetByName(testAddressBook.Name);
        }

        private void CompareAddressItems(Entities.AddressBook expected, Entities.AddressBook actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Stored item does not have the same name");
            Assert.AreEqual(expected.Email, actual.Email, "Stored item does not have the same email");
            Assert.AreEqual(expected.Phone, actual.Phone, "Stored item does not have the same phone");
        }
    }
}
