using Sude.Domain.Interfaces;
using Sude.Domain.Models.Common;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {

        private GenericRepository<AttachmentInfo> _AttachmentRepository;

        public AttachmentRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._AttachmentRepository = new GenericRepository<AttachmentInfo>(ctx);
        }

   
       

       
       

         

        public async Task<IEnumerable<AttachmentInfo>> GetAttachmentsAsync(Guid? entityId=null, Guid? entityTypeId=null, Guid? attachmentTypeId=null)
        {
            //return _ctx.Attachments;
            return await _AttachmentRepository.GetAsync(a=>
            (a.EntityId== entityId || entityId == null) &&
            (a.EntityTypeId == entityTypeId || entityTypeId == null) &&
            (a.AttachmentTypeId == attachmentTypeId || attachmentTypeId == null));
        }
        public void AddAttachment(AttachmentInfo Attachment)
        {
            //_ctx.Attachments.Add(Attachment);
            _AttachmentRepository.Insert(Attachment);
        }

      


      

        

       
        public AttachmentInfo GetAttachmentById(Guid AttachmentId)
        {
            //return _ctx.Attachments.Find(AttachmentId);
            return _AttachmentRepository.GetById(AttachmentId);
        }
        public async  Task<AttachmentInfo> GetAttachmentByIdAsync(Guid AttachmentId)
        {
            //return _ctx.Attachments.Find(AttachmentId);
            return await _AttachmentRepository.GetByIdAsync(AttachmentId);
        }
        public bool EditAttachment(AttachmentInfo Attachment)
        {
            //_ctx.Entry(Attachment).State = EntityState.Modified;
            ////_ctx.Attachments.Update(Attachment);
            _AttachmentRepository.Update(Attachment);
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _AttachmentRepository.Save();
        }

      
        public async Task SaveAsync() =>
            await _AttachmentRepository.SaveAsync();



        public bool DeleteAttachment(Guid attahmentId)
        {
            var attachment = GetAttachmentById(attahmentId);
            if (attachment == null)
                return false;
            try
            {

             
                _AttachmentRepository.Delete(attachment);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
