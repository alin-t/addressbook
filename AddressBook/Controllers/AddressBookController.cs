using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AddressBook.Dal;

namespace AddressBook.Controllers
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
        public IEnumerable<Entities.AddressBook> Get()
        {
            return _dataAccessor.GetAll();
        }

        // GET /name
        public Entities.AddressBook Get(string name)
        {
            return _dataAccessor.GetByName(name);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
