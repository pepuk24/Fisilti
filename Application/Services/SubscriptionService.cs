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
    public class SubscriptionService : ISubscriptionService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IAuditLogService _auditLogService;

        public SubscriptionService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
        }

        public async Task<Result<SubscriptionDTO>> AddAsync(SubscriptionDTO entity)
        {
            try
            {
                Subscription Subscription = _mapper.Map<Subscription>(entity);

                await _unitOfWork.Subscriptions.AddAsync(Subscription);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { AppUserId = entity.AppUserId, RecordId = Subscription.Id.ToString(), TableName = "Subscriptions", Type = LogType.Insert });

                return Result<SubscriptionDTO>.Ok(entity, "Subscription created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { AppUserId = entity.AppUserId, TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<SubscriptionDTO>.Fail("Subscription creation failed.");
            }
        }

        public async Task<Result<IEnumerable<SubscriptionDTO>>> AddRangeAsync(IEnumerable<SubscriptionDTO> entities)
        {
            try
            {
                IEnumerable<Subscription> Subscriptions = _mapper.Map<IEnumerable<Subscription>>(entities);

                await _unitOfWork.Subscriptions.AddRangeAsync(Subscriptions);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { AppUserId = entities.FirstOrDefault()?.AppUserId, TableName = "Subscriptions", Type = LogType.Insert });

                return Result<IEnumerable<SubscriptionDTO>>.Ok(entities, "Subscription created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { AppUserId = entities.FirstOrDefault()?.AppUserId, TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<SubscriptionDTO>>.Fail("Subscription creation failed.");
            }
        }

        public async Task<Result<IEnumerable<SubscriptionDTO>>> FindAsync(Expression<Func<Subscription, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {

                IEnumerable<SubscriptionDTO> Subscriptions = _mapper.Map<IEnumerable<SubscriptionDTO>>(await _unitOfWork.Subscriptions.FindAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Warning });

                return Result<IEnumerable<SubscriptionDTO>>.Ok(Subscriptions, "Subscription got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<SubscriptionDTO>>.Fail("Subscription got failed.");
            }
        }

        public async Task<Result<IEnumerable<SubscriptionDTO>>> GetAllAsync(Expression<Func<Subscription, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {
                IEnumerable<SubscriptionDTO> Subscriptions = _mapper.Map<IEnumerable<SubscriptionDTO>>(await _unitOfWork.Subscriptions.GetAllAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Warning });

                return Result<IEnumerable<SubscriptionDTO>>.Ok(Subscriptions, "Subscription got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<SubscriptionDTO>>.Fail("Subscription got failed.");
            }
        }

        public async Task<Result<SubscriptionDTO?>> GetByIdAsync(int id)
        {
            try
            {

                SubscriptionDTO Subscription = _mapper.Map<SubscriptionDTO>(await _unitOfWork.Subscriptions.GetByIdAsync(id));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Warning });

                return Result<SubscriptionDTO?>.Ok(Subscription, "Subscription got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<SubscriptionDTO?>.Fail("Subscription got failed.");
            }
        }

        public async Task<Result<SubscriptionDTO>> Remove(SubscriptionDTO entity)
        {
            try
            {

                Subscription Subscription = _mapper.Map<Subscription>(entity);

                _unitOfWork.Subscriptions.Remove(Subscription);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Delete });

                return Result<SubscriptionDTO>.Ok(entity, "Subscription deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<SubscriptionDTO>.Fail("Subscription deleted failed.");
            }
        }
        public async Task<Result<IEnumerable<SubscriptionDTO>>> RemoveRange(IEnumerable<SubscriptionDTO> entities)
        {
            try
            {

                IEnumerable<Subscription> Subscriptions = _mapper.Map<IEnumerable<Subscription>>(entities);

                _unitOfWork.Subscriptions.RemoveRange(Subscriptions);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Delete });

                return Result<IEnumerable<SubscriptionDTO>>.Ok(entities, "Subscriptions deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<SubscriptionDTO>>.Fail("Subscriptions deleted failed.");
            }
        }

        public async Task<Result<SubscriptionDTO>> Update(SubscriptionDTO entity)
        {
            try
            {

                Subscription Subscription = _mapper.Map<Subscription>(entity);

                _unitOfWork.Subscriptions.Update(Subscription);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Update });

                return Result<SubscriptionDTO>.Ok(entity, "Subscriptions Updated successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Subscriptions", Type = LogType.Error, Action = e.Message });

                return Result<SubscriptionDTO>.Fail("Subscriptions updated failed.");
            }
        }
    }
}
