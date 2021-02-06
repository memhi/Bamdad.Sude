using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using Sude.Dto.DtoModels.Content;
using Sude.Dto.DtoModels.Result;

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

        private static Image applyPaddingToImage(Image image)
        {
            //get the maximum size of the image dimensions
            int maxSize = Math.Max(image.Height, image.Width);
            Size squareSize = new Size(maxSize, maxSize);

            //create a new square image
            Bitmap squareImage = new Bitmap(squareSize.Width, squareSize.Height);

            using (Graphics graphics = Graphics.FromImage(squareImage))
            {
                //fill the new square with a color
                graphics.FillRectangle(Brushes.Red, 0, 0, squareSize.Width, squareSize.Height);

                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //put the original image on top of the new square
                graphics.DrawImage(image, (squareSize.Width / 2) - (image.Width / 2), (squareSize.Height / 2) - (image.Height / 2), image.Width, image.Height);
            }

            return squareImage;
        }


        private static RotateFlipType getRotateFlipType(int rotateValue)
        {
            RotateFlipType flipType = RotateFlipType.RotateNoneFlipNone;

            switch (rotateValue)
            {
                case 1:
                    flipType = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    flipType = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    flipType = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    flipType = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    flipType = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    flipType = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    flipType = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    flipType = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    flipType = RotateFlipType.RotateNoneFlipNone;
                    break;
            }

            return flipType;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {

            foreach (var prop in image.PropertyItems)
            {
                if (prop.Id == 0x0112)
                {
                    int rotateValue = image.GetPropertyItem(prop.Id).Value[0];
                    RotateFlipType flipType = getRotateFlipType(rotateValue);
                    image.RotateFlip(flipType);
                    break;
                }
            }
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;

            double ratio = ratio = Math.Min(ratioX, ratioY); ;
            if (image.Height>image.Width)
             ratio = (Math.Max(ratioX, ratioY)+ Math.Min(ratioX, ratioY))/2;
            if (ratio > 1)
                ratio = 1;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            
 


            var newImage = new Bitmap(newWidth, newHeight);
            newImage.SetResolution(72, 72);
 
            using (var graphics = Graphics.FromImage(newImage))
            {
               
                graphics.DrawImage(image,0, 0, newWidth, newHeight);

            }
            return newImage;
        }

        public static byte[] scaleImage(Image image, int maxWidth, int maxHeight, bool padImage)
        {
            try
            {
                int newWidth;
                int newHeight;
                byte[] returnArray;

                //check if the image needs rotating (eg phone held vertical when taking a picture for example)
                foreach (var prop in image.PropertyItems)
                {
                    if (prop.Id == 0x0112)
                    {
                        int rotateValue = image.GetPropertyItem(prop.Id).Value[0];
                        RotateFlipType flipType = getRotateFlipType(rotateValue);
                        image.RotateFlip(flipType);
                        break;
                    }
                }

                //apply padding if needed
                if (padImage == true)
                {
                    image = applyPaddingToImage(image);
                }

                //check if the with or height of the image exceeds the maximum specified, if so calculate the new dimensions
                if (image.Width > maxWidth || image.Height > maxHeight)
                {
                    var ratioX = (double)maxWidth / image.Width;
                    var ratioY = (double)maxHeight / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    newWidth = (int)(image.Width * ratio);
                    newHeight = (int)(image.Height * ratio);
                }
                else
                {
                    newWidth = image.Width;
                    newHeight = image.Height;
                }

                //start with a new image
                var newImage = new Bitmap(newWidth, newHeight);

                //set the new resolution, 72 is usually good enough for displaying images on monitors
                newImage.SetResolution(72, 72);
                //or use the original resolution
                //newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                //resize the image
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                }
                image = newImage;

                //save the image to a memorystream to apply the compression level, higher compression = better quality = bigger images
                using (MemoryStream ms = new MemoryStream())
                {
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
                    image.Save(ms, getEncoderInfo("image/jpeg"), encoderParameters);

                    //save the stream as byte array
                    returnArray = ms.ToArray();
                }

                //cleanup
                image.Dispose();
                newImage.Dispose();

                return returnArray;
            }
            catch (Exception ex)
            {
                //there was an error: ex.Message
                return null;
            }
        }


        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType.ToLower() == mimeType.ToLower())
                    return encoders[j];
            }
            return null;
        }



        // GET: AttachmentController
        [HttpGet]
        public ActionResult AddAttachment()
        {
            List<AttachmentNewDtoModel> attachments = _sudeSessionContext.CurrentAttachmentPictures;
            if (attachments == null)
                attachments = new List<AttachmentNewDtoModel>();
            //string userDirectoryPath = Path.Combine(_environment.WebRootPath, "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id);
            //if (Directory.Exists(userDirectoryPath))
            //{
            //    foreach(string fileaddress in Directory.GetFiles(userDirectoryPath))
            //    {
                    
            //        FileInfo file = new FileInfo(fileaddress);
            //        string filewebaddress = "../TempUserAttachmentFiles/" + _sudeSessionContext.CurrentUser.id + "/" + file.Name;
            //        AttachmentNewDtoModel attachment = new AttachmentNewDtoModel()
            //        {
            //            AttachmentFileAddress = filewebaddress,
            //            AttachmentFileType = file.Extension,
            //            Title = file.Name


            //        };
            //        attachments.Add(attachment);

            //    }
            //    _sudeSessionContext.CurrentAttachmentPictures = attachments;

            //}


            return PartialView("AddAttachment", attachments);
        }

        [HttpPost]
        public IActionResult DeleteTempFile(string filename)
        {
            
            try
            {
                string userTempFilePath = Path.Combine(_environment.WebRootPath, "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id, filename);
                FileInfo file = new FileInfo(userTempFilePath);
                if (file != null && file.Exists)
                {
                    file.Delete();


                }
                List<AttachmentNewDtoModel> attachments = _sudeSessionContext.CurrentAttachmentPictures;
                if (attachments != null)
                {
                    AttachmentNewDtoModel attachment = attachments.FirstOrDefault(a => a.Title.ToLower() == filename.ToLower());
                    if (attachment != null)
                       
                        attachments.Remove(attachment);


                        }
                _sudeSessionContext.CurrentAttachmentPictures = attachments;
                return Json(new ResultSetDto()
                {
                    IsSucceed = true,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = ex.Message
                });
            }

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
        private void StoreInFolder(IFormFile file)
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
            attachmentNew.AttachmentFileAddress = Path.Combine("TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id, newFileName);
            attachmentNew.Title = newFileName;

            if (!Directory.Exists(directorypath))
                Directory.CreateDirectory(directorypath);
            var filepath = directorypath + $@"\{newFileName}";
            Image img;
            Image newImage = new Bitmap(1280, 720); ;
            using (Image image = Image.FromStream(file.OpenReadStream(), true, true))
            {

                //  file.CopyTo(fs);
                //  img = Image.FromStream(fs);

                if (image != null)
                {
                    newImage = ScaleImage(image, 1280, 720);

                }



                List<AttachmentNewDtoModel> attachments = _sudeSessionContext.CurrentAttachmentPictures;
                if (attachments == null)
                    attachments = new List<AttachmentNewDtoModel>();
                attachments.Add(attachmentNew);
                _sudeSessionContext.CurrentAttachmentPictures = attachments;



            }
       
            if(newImage!=null)
                newImage.Save(filepath, ImageFormat.Jpeg); 

        }


    }
}
