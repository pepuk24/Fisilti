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
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("miktar 0 dan buyuk olmalı");
            RuleFor(x => x.AppUserId).GreaterThan(0).WithMessage("kullanıcıid 0 dan buyuk olmalı");
            RuleFor(x => x.PromptId).GreaterThan(0).WithMessage("promptid 0 dan buyuk olmalı"); 
        }
    }
}
