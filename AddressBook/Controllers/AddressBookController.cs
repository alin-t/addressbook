using System.Collections.Generic;
using System.Web.Http;
using AddressBook.Dal;

namespace AddressBook.Service.Controllers
{
    public class AddressBookController : ApiController
    {
        private readonly IDataAccessor _dataAccessor;
        public AddressBookController() : this(new MemoryAccessor())
        {
            
        }

        public AddressBookController(IDataAccessor accessor)
        {
            _dataAccessor = accessor;
        }

        // GET /
        [HttpGet]
        public IEnumerable<Entities.AddressBook> Get()
        {
            return _dataAccessor.GetAll();
        }

        // GET /getbyname/name
        [HttpGet]
        public Entities.AddressBook GetByName(string name)
        {
            return _dataAccessor.GetByName(name);
        }

        // GET /add/name/email/phone
        [HttpGet]
        public void Add(string name, string email, string phone)
        {
            _dataAccessor.AddItem(new Entities.AddressBook() {Name = name, Email = email, Phone = phone});
        }

        // GET /delete/name
        [HttpGet]
        [ActionName("delete")]
        public void Delete(string name)
        {
            _dataAccessor.DeleteByName(name);
        }
    }
}
