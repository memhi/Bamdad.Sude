using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Common;

namespace Sude.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private IAttachmentRepository _AttachmentRepository;


        public AttachmentService(IAttachmentRepository AttachmentRepository)
        {
            this._AttachmentRepository = AttachmentRepository;
        }

    

        public async Task<ResultSet<IEnumerable<AttachmentInfo>>> GetAttachmentsAsync(Guid? entityId = null, Guid? entityTypeId = null, Guid? attachmentTypeId = null)
        {
            return new ResultSet<IEnumerable<AttachmentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _AttachmentRepository.GetAttachmentsAsync(entityId, entityTypeId, attachmentTypeId)
            };
        }

        public ResultSet<AttachmentInfo> GetAttachmentById(Guid AttachmentId)
        {
            AttachmentInfo Attachment = _AttachmentRepository.GetAttachmentById(AttachmentId);

            if (Attachment == null)
                return new ResultSet<AttachmentInfo>()
                {
                    IsSucceed = false,
                    Message = "Attachment Not Found",
                    Data = null
                };

            return new ResultSet<AttachmentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Attachment
            };
        }

        public ResultSet<AttachmentInfo> AddAttachment(AttachmentInfo  Attachment)
        {
         
          
            _AttachmentRepository.AddAttachment(Attachment);
            _AttachmentRepository.Save();

            return new ResultSet<AttachmentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Attachment
            };
        }

        public ResultSet EditAttachment(AttachmentInfo Attachment)
        {
          

            if (!_AttachmentRepository.EditAttachment(Attachment))
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Edited" };

            try
            {
                _AttachmentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteAttachment(Guid AttachmentId)
        {

            if (!_AttachmentRepository.DeleteAttachment(AttachmentId))
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Deleted" };

            try
            {
                _AttachmentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

       
        public async Task<ResultSet<AttachmentInfo>> AddAttachmentAsync(AttachmentInfo Attachment)
        {
            
           _AttachmentRepository.AddAttachment(Attachment);

            try{await _AttachmentRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<AttachmentInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<AttachmentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Attachment
            };
        }

        public async Task<ResultSet> EditAttachmentAsync(AttachmentInfo Attachment)
        {
          

            if (!_AttachmentRepository.EditAttachment(Attachment))
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Edited" };

            try
            {
                await _AttachmentRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteAttachmentAsync(Guid AttachmentId)
        {







            if (!_AttachmentRepository.DeleteAttachment(AttachmentId))
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Deleted" };

            try
            {
                await _AttachmentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Attachment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<AttachmentInfo>> GetAttachmentByIdAsync(Guid AttachmentId)
        {
            AttachmentInfo Attachment = await _AttachmentRepository.GetAttachmentByIdAsync(AttachmentId);

            if (Attachment == null)
                return new ResultSet<AttachmentInfo>()
                {
                    IsSucceed = false,
                    Message = "Attachment Not Found",
                    Data = null
                };

            return new ResultSet<AttachmentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Attachment
            };
        }
    }
}
