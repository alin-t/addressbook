using System;
using AddressBook.Controllers;
using AddressBook.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressBook.Tests
{
    [TestClass]
    public class AddressBookTest
    {
        private IDataAccessor accessor;
        private AddressBookController controller;

        [TestInitialize]
        public void MyTestInitialize()
        {
            accessor = new MemoryAccessor();
            controller = new AddressBookController(accessor);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var x = controller.Get();
            Console.WriteLine(x);
        }
    }
}
