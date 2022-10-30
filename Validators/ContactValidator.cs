using FluentValidation;
using PhoneBookBackend.Entities;


namespace PhoneBookBackend.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(f => f.FirstName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("First name must be defined and be characters only!");
            RuleFor(l => l.LastName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("Last name  must be defined and be characters only!");
        }
    }
}
