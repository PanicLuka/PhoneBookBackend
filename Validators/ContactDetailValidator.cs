using FluentValidation;
using PhoneBookBackend.Entities;

namespace PhoneBookBackend.Validators
{
    public class ContactDetailValidator : AbstractValidator<ContactDetails>
    { 
        public ContactDetailValidator()
        {
            RuleFor(v => v.Value).NotEmpty().WithMessage("Value must not be empty!");
        }
    }
}
