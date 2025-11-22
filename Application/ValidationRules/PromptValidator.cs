using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class PromptValidator : AbstractValidator<Prompt>
    {
        public PromptValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık Boş Olamaz")
                .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır.")
                .MaximumLength(120).WithMessage("Başlık en fazla 120 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik Boş Olamaz.")
                .MinimumLength(20).WithMessage("İçerik en az 20 karakter olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama Boş Olamaz.");


        }
    }
}
