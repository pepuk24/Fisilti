using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class PromptListValidator :AbstractValidator<IEnumerable<PromptDTO>>
    {
        public PromptListValidator()
        {
            //2.yontem
            RuleForEach(x => x).SetValidator(new PromptDTOValidator());
        }
    }
}
