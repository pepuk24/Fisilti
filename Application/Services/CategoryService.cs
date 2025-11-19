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
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IAuditLogService _auditLogService;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
        }

        public async Task<Result<CategoryDTO>> AddAsync(CategoryDTO entity)
        {
            try
            {
                Category Category = _mapper.Map<Category>(entity);

                await _unitOfWork.Categories.AddAsync(Category);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { RecordId = Category.Id.ToString(), TableName = "Categories", Type = LogType.Insert });

                return Result<CategoryDTO>.Ok(entity, "Category created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<CategoryDTO>.Fail("Category creation failed.");
            }
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> AddRangeAsync(IEnumerable<CategoryDTO> entities)
        {
            try
            {
                IEnumerable<Category> Categories = _mapper.Map<IEnumerable<Category>>(entities);

                await _unitOfWork.Categories.AddRangeAsync(Categories);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Insert });

                return Result<IEnumerable<CategoryDTO>>.Ok(entities, "Category created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<CategoryDTO>>.Fail("Category creation failed.");
            }
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> FindAsync(Expression<Func<Category, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {

                IEnumerable<CategoryDTO> Categories = _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Categories.FindAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Warning });

                return Result<IEnumerable<CategoryDTO>>.Ok(Categories, "Category got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<CategoryDTO>>.Fail("Category got failed.");
            }
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> GetAllAsync(Expression<Func<Category, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {
                IEnumerable<CategoryDTO> Categories = _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Categories.GetAllAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Warning });

                return Result<IEnumerable<CategoryDTO>>.Ok(Categories, "Category got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<CategoryDTO>>.Fail("Category got failed.");
            }
        }

        public async Task<Result<CategoryDTO?>> GetByIdAsync(int id)
        {
            try
            {

                CategoryDTO Category = _mapper.Map<CategoryDTO>(await _unitOfWork.Categories.GetByIdAsync(id));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Warning });

                return Result<CategoryDTO?>.Ok(Category, "Category got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<CategoryDTO?>.Fail("Category got failed.");
            }
        }

        public async Task<Result<CategoryDTO>> Remove(CategoryDTO entity)
        {
            try
            {

                Category Category = _mapper.Map<Category>(entity);

                _unitOfWork.Categories.Remove(Category);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Delete });

                return Result<CategoryDTO>.Ok(entity, "Category deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<CategoryDTO>.Fail("Category deleted failed.");
            }
        }
        public async Task<Result<IEnumerable<CategoryDTO>>> RemoveRange(IEnumerable<CategoryDTO> entities)
        {
            try
            {

                IEnumerable<Category> Categories = _mapper.Map<IEnumerable<Category>>(entities);

                _unitOfWork.Categories.RemoveRange(Categories);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Delete });

                return Result<IEnumerable<CategoryDTO>>.Ok(entities, "Categories deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<CategoryDTO>>.Fail("Categories deleted failed.");
            }
        }

        public async Task<Result<CategoryDTO>> Update(CategoryDTO entity)
        {
            try
            {

                Category Category = _mapper.Map<Category>(entity);

                _unitOfWork.Categories.Update(Category);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Update });

                return Result<CategoryDTO>.Ok(entity, "Categories Updated successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Categories", Type = LogType.Error, Action = e.Message });

                return Result<CategoryDTO>.Fail("Categories updated failed.");
            }
        }
    }
}
