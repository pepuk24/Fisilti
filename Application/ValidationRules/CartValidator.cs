using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("KullanıcıId 0'dan büyük olmalıdır.");

            RuleFor(x => x.PromptId)
                .GreaterThan(0).WithMessage("PromptId 0'dan büyük olmalıdır.");

        }
    }
}
