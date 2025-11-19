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
    public class FavouriteService : IFavouriteService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IAuditLogService _auditLogService;

        public FavouriteService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
        }

        public async Task<Result<FavouriteDTO>> AddAsync(FavouriteDTO entity)
        {
            try
            {
                Favourite Favourite = _mapper.Map<Favourite>(entity);

                await _unitOfWork.Favourites.AddAsync(Favourite);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { AppUserId = entity.AppUserId, RecordId = Favourite.Id.ToString(), TableName = "Favourites", Type = LogType.Insert });

                return Result<FavouriteDTO>.Ok(entity, "Favourite created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { AppUserId = entity.AppUserId, TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<FavouriteDTO>.Fail("Favourite creation failed.");
            }
        }

        public async Task<Result<IEnumerable<FavouriteDTO>>> AddRangeAsync(IEnumerable<FavouriteDTO> entities)
        {
            try
            {
                IEnumerable<Favourite> Favourites = _mapper.Map<IEnumerable<Favourite>>(entities);

                await _unitOfWork.Favourites.AddRangeAsync(Favourites);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { AppUserId = entities.FirstOrDefault()?.AppUserId, TableName = "Favourites", Type = LogType.Insert });

                return Result<IEnumerable<FavouriteDTO>>.Ok(entities, "Favourite created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { AppUserId = entities.FirstOrDefault()?.AppUserId, TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<FavouriteDTO>>.Fail("Favourite creation failed.");
            }
        }

        public async Task<Result<IEnumerable<FavouriteDTO>>> FindAsync(Expression<Func<Favourite, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {

                IEnumerable<FavouriteDTO> Favourites = _mapper.Map<IEnumerable<FavouriteDTO>>(await _unitOfWork.Favourites.FindAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Warning });

                return Result<IEnumerable<FavouriteDTO>>.Ok(Favourites, "Favourite got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<FavouriteDTO>>.Fail("Favourite got failed.");
            }
        }

        public async Task<Result<IEnumerable<FavouriteDTO>>> GetAllAsync(Expression<Func<Favourite, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {
                IEnumerable<FavouriteDTO> Favourites = _mapper.Map<IEnumerable<FavouriteDTO>>(await _unitOfWork.Favourites.GetAllAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Warning });

                return Result<IEnumerable<FavouriteDTO>>.Ok(Favourites, "Favourite got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<FavouriteDTO>>.Fail("Favourite got failed.");
            }
        }

        public async Task<Result<FavouriteDTO?>> GetByIdAsync(int id)
        {
            try
            {

                FavouriteDTO Favourite = _mapper.Map<FavouriteDTO>(await _unitOfWork.Favourites.GetByIdAsync(id));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Warning });

                return Result<FavouriteDTO?>.Ok(Favourite, "Favourite got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<FavouriteDTO?>.Fail("Favourite got failed.");
            }
        }

        public async Task<Result<FavouriteDTO>> Remove(FavouriteDTO entity)
        {
            try
            {

                Favourite Favourite = _mapper.Map<Favourite>(entity);

                _unitOfWork.Favourites.Remove(Favourite);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Delete });

                return Result<FavouriteDTO>.Ok(entity, "Favourite deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<FavouriteDTO>.Fail("Favourite deleted failed.");
            }
        }
        public async Task<Result<IEnumerable<FavouriteDTO>>> RemoveRange(IEnumerable<FavouriteDTO> entities)
        {
            try
            {

                IEnumerable<Favourite> Favourites = _mapper.Map<IEnumerable<Favourite>>(entities);

                _unitOfWork.Favourites.RemoveRange(Favourites);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Delete });

                return Result<IEnumerable<FavouriteDTO>>.Ok(entities, "Favourites deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<FavouriteDTO>>.Fail("Favourites deleted failed.");
            }
        }

        public async Task<Result<FavouriteDTO>> Update(FavouriteDTO entity)
        {
            try
            {

                Favourite Favourite = _mapper.Map<Favourite>(entity);

                _unitOfWork.Favourites.Update(Favourite);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Update });

                return Result<FavouriteDTO>.Ok(entity, "Favourites Updated successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Favourites", Type = LogType.Error, Action = e.Message });

                return Result<FavouriteDTO>.Fail("Favourites updated failed.");
            }
        }
    }
}
