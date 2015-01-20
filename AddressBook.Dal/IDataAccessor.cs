using System.Collections.Generic;
using AddressBook.Entities;
namespace AddressBook.Dal
{
    public interface IDataAccessor
    {
        void AddItem(Entities.AddressBook addressBook);

        List<Entities.AddressBook> GetAll();

        Entities.AddressBook GetByName(string name);
    }
}
