using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Content;

namespace Sude.Mvc.UI.Admin.Controllers.Content
{
    public class AttachmentController : BaseAdminController
    {
        public readonly SudeSessionContext _sudeSessionContext;
  
        private readonly IWebHostEnvironment _environment;
        public AttachmentController(SudeSessionContext sudeSessionContext,IWebHostEnvironment environment)

        {
            _sudeSessionContext = sudeSessionContext;
            _environment = environment;

        }
        // GET: AttachmentController
        [HttpGet]
        public ActionResult AddAttachment()
        {
            List<AttachmentNewDtoModel> attachments = new List<AttachmentNewDtoModel>();
            if (attachments == null)
                attachments = new List<AttachmentNewDtoModel>();
            string userDirectoryPath = Path.Combine(_environment.WebRootPath, "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id);
            if (Directory.Exists(userDirectoryPath))
            {
                foreach(string fileaddress in Directory.GetFiles(userDirectoryPath))
                {
                    FileInfo file = new FileInfo(fileaddress);
                    AttachmentNewDtoModel attachment = new AttachmentNewDtoModel()
                    {
                        AttachmentFileAddress = file.FullName,
                        AttachmentFileType = file.Extension,
                        Title = file.Name


                    };
                    attachments.Add(attachment);

                }
                _sudeSessionContext.CurrentAttachmentPictures = attachments;

            }


            return PartialView("AddAttachment", attachments);
        }



        [HttpPost]
        public IActionResult Capture(IFormFileCollection fs)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                                                                       StoreInFolder(file );
                                          
                                           }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        private void StoreInFolder(IFormFile file )
        {
            AttachmentNewDtoModel attachmentNew = new AttachmentNewDtoModel();

            var fileName = file.FileName;
      
            // Unique filename "Guid"  
            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
            attachmentNew.AttachmentId = myUniqueFileName;
          
            // Getting Extension  
            var fileExtension = Path.GetExtension(fileName);
            attachmentNew.AttachmentFileType = fileExtension;

          // Concating filename + fileExtension (unique filename)  
            var newFileName = string.Concat(myUniqueFileName, fileExtension);
            //  Generating Path to store photo   
            var directorypath = Path.Combine(_environment.WebRootPath, "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id);
            attachmentNew.AttachmentFileAddress= Path.Combine( "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id, newFileName);


            if (!Directory.Exists(directorypath))
                Directory.CreateDirectory(directorypath);
            var filepath = directorypath + $@"\{newFileName}";

            using (FileStream fs = System.IO.File.Create(filepath))
            {
                file.CopyTo(fs);
                fs.Flush();
                List<AttachmentNewDtoModel> attachments = _sudeSessionContext.CurrentAttachmentPictures;
                if (attachments == null)
                    attachments = new List<AttachmentNewDtoModel>();
                attachments.Add(attachmentNew);
                _sudeSessionContext.CurrentAttachmentPictures = attachments;
            }
        }


    }
}
