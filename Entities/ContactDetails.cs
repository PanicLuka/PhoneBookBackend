using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookBackend.Entities
{
    public class ContactDetails
    {
        [Key]
        public int ContactDetailsId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        [ForeignKey("FK_ContactId")]
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

    }
}
