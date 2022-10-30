using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookBackend.Entities
{
    public class Contact
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public List<ContactDetails> Details { get; set; }
        public ICollection<ContactDetails> Details { get; set; }
    }
}
