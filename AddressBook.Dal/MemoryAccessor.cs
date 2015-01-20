using System.Collections.Generic;
using System.Linq;
using AddressBook.Core;

namespace AddressBook.Dal
{
    public class MemoryAccessor : IDataAccessor
    {
        private static readonly List<Entities.AddressBook> AddressBooks = new List<Entities.AddressBook>();
        static readonly object Locker = new object();
        public void Clear()
        {
            lock (Locker)
            {
                AddressBooks.Clear();
            }
        }

        public void AddItem(Entities.AddressBook addressBook)
        {
            lock (Locker)
            {
                if (AddressBooks.Any(item => item.Name.Equals(addressBook.Name)))
                    throw new ItemAlreadyExistsException("Item already exists");
                AddressBooks.Add(addressBook);
            }
        }

        public List<Entities.AddressBook> GetAll()
        {
            return AddressBooks;
        }

        public Entities.AddressBook GetByName(string name)
        {
            lock (Locker)
            {
                if (!AddressBooks.Any(item => item.Name.Equals(name))) throw new ItemNotFoundException(string.Format("{0} does not exists", name));
                return AddressBooks.First(item => item.Name.Equals(name));
            }
        }

        public void DeleteByName(string name)
        {
            lock (Locker)
            {
                if (!AddressBooks.Any(item => item.Name.Equals(name))) throw new ItemNotFoundException(string.Format("{0} does not exists", name));
                AddressBooks.RemoveAll(item => item.Name.Equals(name));
            }
        }
    }
}
