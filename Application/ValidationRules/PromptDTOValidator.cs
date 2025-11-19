using Application.DTOs;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class PromptDTOValidator :AbstractValidator<PromptDTO >
    {
        public PromptDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Başlık Boş olamaz").MinimumLength(5).WithMessage("başlık en azından 5 karakter olmalıdır")
                .MaximumLength(120).WithMessage("baslik en fazla 120 karekter olmalıdır");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("içerik boş olamaz")
                .MinimumLength(120).WithMessage("en az 20 karekter olmalıdı");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("fiyat negatif olamaz");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("açıklama boş olamaz");

             
        }
    }
}
