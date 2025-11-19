using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class PaymentValidator :AbstractValidator<Payment>
    {
        IUnitOfWork _unitOfWork;
        public PaymentValidator(IUnitOfWork unitOfWork)
        {
            

            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("miktar 0 dan buyuk olmali");
                
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("transactionid boş olamaz")
                .MustAsync(TransactionIdIsUnique).
                

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("fiyat negatif olamaz");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("açıklama boş olamaz");
            this._unitOfWork= unitOfWork;
        }

        async Task<bool> TransactionIdIsUnique(string transactionId)
        {
            IEnumerable<Payment> result =await UnitOfWork.Payments.FindAsync(x=>x.TransactionId == transactionId);
            if (result.Count() > 0)
                return false;
            else
                return true;
        }
    }
}
