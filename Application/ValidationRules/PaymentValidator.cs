using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ValidationRules
{
    internal class PaymentValidator : AbstractValidator<Payment>
    {
        IUnitOfWork unitOfWork;
        public PaymentValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Amount)
               .GreaterThan(0).WithMessage("Miktar 0'dan Büyük olmalıdır.");

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("İşlem Numarası Boş Olamaz.")
                .MustAsync(TransactionIdIsUnique).WithMessage("İşlem Numarası Daha Önce Kayıtlı");




            this.unitOfWork = unitOfWork;
        }

        async Task<bool> TransactionIdIsUnique(string transactionId, CancellationToken cancellationToken)
        {
            IEnumerable<Payment> result = await unitOfWork.Payments.FindAsync(x => x.TransactionId == transactionId);

            if (result.Count() > 0)
                return false;
            else
                return true;
        }
    }
}
