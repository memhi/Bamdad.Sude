using Sude.Application.Interfaces;
using Sude.Domain.Models.Type;
using Sude.Common;
using Sude.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Result;

namespace Sude.Application.Services
{
    public class TypeService : ITypeService
    {
        private ITypeRepository _TypeRepository;
        private ITypeGroupRepository _TypeGroupRepository;
        public TypeService(ITypeRepository typeRepository,ITypeGroupRepository typeGroupRepository)
        {
            this._TypeRepository = typeRepository;
            this._TypeGroupRepository = typeGroupRepository;
        }


        #region Type Method
        public async Task<ResultSet<IEnumerable<TypeInfo>>> GetTypesByGroupKeyAsync(string GroupKey)
        {
            return new ResultSet<IEnumerable<TypeInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _TypeRepository.GetTypesByGroupKeyAsync(GroupKey)
            };
           
        }

         
        public ResultSet<TypeInfo> GetTypeById(Guid TypeId)
        {
            TypeInfo Type = _TypeRepository.GetTypeById(TypeId);
            
            if (Type == null)
                return new ResultSet<TypeInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeInfo()
                {
                    Id = Type.Id,
                    Description = Type.Description,
                    TypeGroupId = Type.TypeGroupId,
                    TypeKey = Type.TypeKey,
                    TypeTitle = Type.TypeTitle

                }
            };

        }

        public async Task<ResultSet<TypeInfo>> GetTypeByIdAsync(Guid TypeId)
        {
            TypeInfo Type = await _TypeRepository.GetTypeByIdAsync(TypeId);

            if (Type == null)
                return new ResultSet<TypeInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeInfo()
                {
                    Id = Type.Id,
                    Description = Type.Description,
                    TypeGroupId = Type.TypeGroupId,
                    TypeKey = Type.TypeKey,
                    TypeTitle = Type.TypeTitle

                }
            };

        }

        public ResultSet<TypeInfo> GetTypeByKey(string TypeKey)
        {
            TypeInfo Type = _TypeRepository.GetTypeByKey(TypeKey);

            if (Type == null)
                return new ResultSet<TypeInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeInfo()
                {
                    Id = Type.Id,
                    Description = Type.Description,
                    TypeGroupId = Type.TypeGroupId,
                    TypeKey = Type.TypeKey,
                    TypeTitle = Type.TypeTitle

                }
            };

        }

        public async Task<ResultSet<TypeInfo>> GetTypeByKeyAsync(string TypeKey)
        {
            TypeInfo Type =await _TypeRepository.GetTypeByKeyAsync(TypeKey);

            if (Type == null)
                return new ResultSet<TypeInfo>()
                {
                    IsSucceed = false,
                    Message = "نوع با این کلید پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeInfo()
                {
                    Id = Type.Id,
                    Description = Type.Description,
                    TypeGroupId = Type.TypeGroupId,
                    TypeKey = Type.TypeKey,
                    TypeTitle = Type.TypeTitle

                }
            };

        }


        #endregion

        #region Type Method
        public async Task<ResultSet<IEnumerable<TypeGroupInfo>>> GetTypesGroupAsync()
        {
            return new ResultSet<IEnumerable<TypeGroupInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _TypeGroupRepository.GetTypeGroupsAsync()
            };

        }


        public ResultSet<TypeGroupInfo> GetTypeGroupById(Guid TypeGroupId)
        {
            TypeGroupInfo TypeGroup = _TypeGroupRepository.GetTypeGroupById(TypeGroupId);

            if (TypeGroup == null)
                return new ResultSet<TypeGroupInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeGroupInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeGroupInfo()
                {
                    Id = TypeGroup.Id,
                    Description = TypeGroup.Description,
           
                     TypeGroupKey = TypeGroup.TypeGroupKey,
                     TypeGroupTitle = TypeGroup.TypeGroupTitle

                }
            };

        }

        public async Task<ResultSet<TypeGroupInfo>> GetTypeGroupByIdAsync(Guid TypeGroupId)
        {
            TypeGroupInfo TypeGroup =await _TypeGroupRepository.GetTypeGroupByIdAsync(TypeGroupId);

            if (TypeGroup == null)
                return new ResultSet<TypeGroupInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeGroupInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeGroupInfo()
                {
                    Id = TypeGroup.Id,
                    Description = TypeGroup.Description,

                    TypeGroupKey = TypeGroup.TypeGroupKey,
                    TypeGroupTitle = TypeGroup.TypeGroupTitle

                }
            };

        }
        public ResultSet<TypeGroupInfo> GetTypeGroupByKey(string TypeGroupKey)
        {
            TypeGroupInfo TypeGroup = _TypeGroupRepository.GetTypeGroupByKey(TypeGroupKey);

            if (TypeGroup == null)
                return new ResultSet<TypeGroupInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeGroupInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeGroupInfo()
                {
                    Id = TypeGroup.Id,
                    Description = TypeGroup.Description,

                    TypeGroupKey = TypeGroup.TypeGroupKey,
                    TypeGroupTitle = TypeGroup.TypeGroupTitle

                }
            };

        }

        public async Task< ResultSet<TypeGroupInfo>> GetTypeGroupByKeyAsync(string TypeGroupKey)
        {
            TypeGroupInfo TypeGroup = await _TypeGroupRepository.GetTypeGroupByKeyAsync(TypeGroupKey);

            if (TypeGroup == null)
                return new ResultSet<TypeGroupInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<TypeGroupInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new TypeGroupInfo()
                {
                    Id = TypeGroup.Id,
                    Description = TypeGroup.Description,

                    TypeGroupKey = TypeGroup.TypeGroupKey,
                    TypeGroupTitle = TypeGroup.TypeGroupTitle

                }
            };

        }
        #endregion
    }
}
