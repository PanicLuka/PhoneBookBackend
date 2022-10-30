using FluentValidation;
using PhoneBookBackend.Entities;


namespace PhoneBookBackend.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(f => f.FirstName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("First name must be defined and be characters only!");
            RuleFor(l => l.LastName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("Last name  must be defined and be characters only!");
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress()
                .WithMessage("Email must be defined!");
            RuleFor(p => p.Password).NotEmpty().Matches(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$").WithMessage("Password wrong");
        }
    }
}//@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
