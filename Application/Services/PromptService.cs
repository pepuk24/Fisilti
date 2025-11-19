//using Application.Common;
//using Application.DTOs;
//using Application.Interfaces;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Enums;
//using Domain.Interfaces;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using FluentValidation.Results;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.ValidationRules;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class PromptService : IPromptService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IAuditLogService _auditLogService;
        IValidator<PromptDTO> _validator;

        IValidator<IEnumerable<PromptDTO>> _listValidator;

        public PromptService(IUnitOfWork unitOfWork, IMapper mapper, IAuditLogService auditLogService, IValidator<PromptDTO> validator, IValidator<IEnumerable<PromptDTO>> listValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
            _validator = validator;
            _listValidator = listValidator;
        }

        


        public async Task<Result<PromptDTO>> AddAsync(PromptDTO entity)
        {
            try
            {
                ValidationResult result= _validator.Validate(entity);
                if (!result.IsValid)
                {
                   string ErrorsMessages= string.Join(',',result.Errors.Select(x => x.ErrorMessage));

                    throw new ApplicationException($" Validation Hatası {ErrorsMessages}");
                }

                Prompt Prompt = _mapper.Map<Prompt>(entity);

                await _unitOfWork.Prompts.AddAsync(Prompt);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { RecordId = Prompt.Id.ToString(), TableName = "Prompts", Type = LogType.Insert });

                return Result<PromptDTO>.Ok(entity, "Prompt created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<PromptDTO>.Fail("Prompt creation failed.");
            }
        }

        public async Task<Result<IEnumerable<PromptDTO>>> AddRangeAsync(IEnumerable<PromptDTO> entities)
        {
            try
            {

                 //1.yontem  
                foreach (var entity in entities)
                {
                    ValidationResult result = _validator.Validate(entity);
                    if (!result.IsValid)
                    {
                        string ErrorsMessages = string.Join(',', result.Errors.Select(x => x.ErrorMessage));

                        throw new ApplicationException($" Validation Hatası {ErrorsMessages}");
                    }

                }
                //2.yontem 

                ValidationResult result1 = await _listValidator.ValidateAsync(entities);
                if (!result1.IsValid)
                {
                    string ErrorsMessages = string.Join(',', result1.Errors.Select(x => x.ErrorMessage));

                    throw new ApplicationException($" Validation Hatası {ErrorsMessages}");
                }

                IEnumerable<Prompt> Prompts = _mapper.Map<IEnumerable<Prompt>>(entities);

                await _unitOfWork.Prompts.AddRangeAsync(Prompts);
                await _unitOfWork.CompleteAsync();

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Insert });

                return Result<IEnumerable<PromptDTO>>.Ok(entities, "Prompt created successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PromptDTO>>.Fail("Prompt creation failed.");
            }
        }

       


        public async Task<Result<IEnumerable<PromptDTO>>> FindAsync(Expression<Func<Prompt, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {

                IEnumerable<PromptDTO> Prompts = _mapper.Map<IEnumerable<PromptDTO>>(await _unitOfWork.Prompts.FindAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Warning });

                return Result<IEnumerable<PromptDTO>>.Ok(Prompts, "Prompt got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PromptDTO>>.Fail("Prompt got failed.");
            }
        }

        public async Task<Result<IEnumerable<PromptDTO>>> GetAllAsync(Expression<Func<Prompt, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            try
            {
                IEnumerable<PromptDTO> Prompts = _mapper.Map<IEnumerable<PromptDTO>>(await _unitOfWork.Prompts.GetAllAsync(filter, orderType, includes));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Warning });

                return Result<IEnumerable<PromptDTO>>.Ok(Prompts, "Prompt got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PromptDTO>>.Fail("Prompt got failed.");
            }
        }

        public async Task<Result<PromptDTO?>> GetByIdAsync(int id)
        {
            try
            {

                PromptDTO Prompt = _mapper.Map<PromptDTO>(await _unitOfWork.Prompts.GetByIdAsync(id));

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Warning });

                return Result<PromptDTO?>.Ok(Prompt, "Prompt got successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<PromptDTO?>.Fail("Prompt got failed.");
            }
        }

        public async Task<Result<PromptDTO>> Remove(PromptDTO entity)
        {
            try
            {

                Prompt Prompt = _mapper.Map<Prompt>(entity);

                _unitOfWork.Prompts.Remove(Prompt);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Delete });

                return Result<PromptDTO>.Ok(entity, "Prompt deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<PromptDTO>.Fail("Prompt deleted failed.");
            }
        }
        public async Task<Result<IEnumerable<PromptDTO>>> RemoveRange(IEnumerable<PromptDTO> entities)
        {
            try
            {

                IEnumerable<Prompt> Prompts = _mapper.Map<IEnumerable<Prompt>>(entities);

                _unitOfWork.Prompts.RemoveRange(Prompts);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Delete });

                return Result<IEnumerable<PromptDTO>>.Ok(entities, "Prompts deleted successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<IEnumerable<PromptDTO>>.Fail("Prompts deleted failed.");
            }
        }

        public async Task<Result<PromptDTO>> Update(PromptDTO entity)
        {
            try
            {

                Prompt Prompt = _mapper.Map<Prompt>(entity);

                _unitOfWork.Prompts.Update(Prompt);

                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Update });

                return Result<PromptDTO>.Ok(entity, "Prompts Updated successfully.");
            }
            catch (Exception e)
            {
                await _auditLogService.AddAsync(new AuditLog { TableName = "Prompts", Type = LogType.Error, Action = e.Message });

                return Result<PromptDTO>.Fail("Prompts updated failed.");
            }
        }
    }
}
