using FluentValidation;
using PusulaCCProject.WebUI.Models;

namespace PusulaCCProject.WebUI.Validations
{
    public class LoginRegisterModelValidator : AbstractValidator<LoginRegisterModel>
    {
        public LoginRegisterModelValidator() 
        {
            RuleFor(model => model.username).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz!");
            RuleFor(model => model.password).NotEmpty().WithMessage("Şifre Boş Bırakılamaz!");
        }
    }
}
