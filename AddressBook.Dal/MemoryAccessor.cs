using System;
using System.Collections.Generic;
using System.Linq;
using AddressBook.Entities;

namespace AddressBook.Dal
{
    public class MemoryAccessor : IDataAccessor
    {
        private readonly List<Entities.AddressBook> _addressBooks = new List<Entities.AddressBook>(); 
        public void AddItem(Entities.AddressBook addressBook)
        {
            if (_addressBooks.Any(item => item.Name.Equals(addressBook.Name)))
                throw new ArgumentException("Item already exists");
            _addressBooks.Add(addressBook);
        }

        public List<Entities.AddressBook> GetAll()
        {
            return _addressBooks;
        }

        public Entities.AddressBook GetByName(string name)
        {
            return _addressBooks.FirstOrDefault(item => item.Name.Equals(name));
        }
    }
}
