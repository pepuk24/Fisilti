using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IAuditLogService _auditLogService;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
        }

        public async Task<Result<PaymentDTO>> AddAsync(PaymentDTO entity)
        {
            try
            {
                Payment Payment = _mapper.Map<Payment>(entity);

                await _unitOfWork.Payments.AddAsync(Payment);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { RecordId = Payment.Id.ToString(), TableName = "Payments", Type = LogType.Insert });

                return Result<PaymentDTO>.Ok(entity, "Payment created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<PaymentDTO>.Fail("Payment creation failed.");
            }
        }

        public async Task<Result<IEnumerable<PaymentDTO>>> AddRangeAsync(IEnumerable<PaymentDTO> entities)
        {
            try
            {
                IEnumerable<Payment> Payments = _mapper.Map<IEnumerable<Payment>>(entities);

                await _unitOfWork.Payments.AddRangeAsync(Payments);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Insert });

                return Result<IEnumerable<PaymentDTO>>.Ok(entities, "Payment created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PaymentDTO>>.Fail("Payment creation failed.");
            }
        }

        public async Task<Result<IEnumerable<PaymentDTO>>> FindAsync(Expression<Func<Payment, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {

                IEnumerable<PaymentDTO> Payments = _mapper.Map<IEnumerable<PaymentDTO>>(await _unitOfWork.Payments.FindAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Warning });

                return Result<IEnumerable<PaymentDTO>>.Ok(Payments, "Payment got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PaymentDTO>>.Fail("Payment got failed.");
            }
        }

        public async Task<Result<IEnumerable<PaymentDTO>>> GetAllAsync(Expression<Func<Payment, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {
                IEnumerable<PaymentDTO> Payments = _mapper.Map<IEnumerable<PaymentDTO>>(await _unitOfWork.Payments.GetAllAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Warning });

                return Result<IEnumerable<PaymentDTO>>.Ok(Payments, "Payment got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PaymentDTO>>.Fail("Payment got failed.");
            }
        }

        public async Task<Result<PaymentDTO?>> GetByIdAsync(int id)
        {
            try
            {

                PaymentDTO Payment = _mapper.Map<PaymentDTO>(await _unitOfWork.Payments.GetByIdAsync(id));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Warning });

                return Result<PaymentDTO?>.Ok(Payment, "Payment got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<PaymentDTO?>.Fail("Payment got failed.");
            }
        }

        public async Task<Result<PaymentDTO>> Remove(PaymentDTO entity)
        {
            try
            {

                Payment Payment = _mapper.Map<Payment>(entity);

                _unitOfWork.Payments.Remove(Payment);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Delete });

                return Result<PaymentDTO>.Ok(entity, "Payment deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<PaymentDTO>.Fail("Payment deleted failed.");
            }
        }
        public async Task<Result<IEnumerable<PaymentDTO>>> RemoveRange(IEnumerable<PaymentDTO> entities)
        {
            try
            {

                IEnumerable<Payment> Payments = _mapper.Map<IEnumerable<Payment>>(entities);

                _unitOfWork.Payments.RemoveRange(Payments);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Delete });

                return Result<IEnumerable<PaymentDTO>>.Ok(entities, "Payments deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PaymentDTO>>.Fail("Payments deleted failed.");
            }
        }

        public async Task<Result<PaymentDTO>> Update(PaymentDTO entity)
        {
            try
            {

                Payment Payment = _mapper.Map<Payment>(entity);

                _unitOfWork.Payments.Update(Payment);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Update });

                return Result<PaymentDTO>.Ok(entity, "Payments Updated successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Payments", Type = LogType.Error, Action = e.Message });

                return Result<PaymentDTO>.Fail("Payments updated failed.");
            }
        }
    }
}
