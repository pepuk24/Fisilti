using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class FavouriteValidator :AbstractValidator<Favourite>
    {
        public FavouriteValidator()
        {
            RuleFor(x => x.AppUserId).GreaterThan(0)
                .WithMessage("kullanıcıid 0 dan buyuk olmalı");
            RuleFor(x => x.PromptId)
                .GreaterThan(0).WithMessage("promptid 0 dan buyuk olmalı");
        }
    }
}
