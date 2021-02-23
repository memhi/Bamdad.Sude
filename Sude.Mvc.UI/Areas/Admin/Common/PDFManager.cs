using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Order;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting;

namespace Sude.Mvc.UI.Admin
{
    public class PDFManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SudeSessionContext  _sudeSessionContext;

        public PDFManager(IHostingEnvironment hostingEnvironment, SudeSessionContext sudeSessionContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _sudeSessionContext = sudeSessionContext;
        }

        protected virtual Font GetFont(Rectangle pageSize)
        {
            float fontSize = 9;
            if (pageSize == PageSize.A7)
                fontSize = 6;
            else if (pageSize == PageSize.A5)
                fontSize = 7;

            return GetFont("Sude.ttf", fontSize);
        }


        protected virtual Font GetFont(string fontFileName,float fontSize)
        {
            if (fontFileName == null)
                throw new ArgumentNullException(nameof(fontFileName));

            var fontPath = _hostingEnvironment.ContentRootPath + "\\App_Data\\pdf\\" + fontFileName;
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, fontSize, Font.NORMAL);
            return font;
        }
        protected virtual int GetDirection()
        {
            return PdfWriter.RUN_DIRECTION_RTL;
        }

        public virtual Rectangle GetPageSize(string pageSize)
        {
            pageSize = pageSize.ToLower();
            switch (pageSize)
            {
                case "a7":
                    return PageSize.A7;
                case "a5":
                    return PageSize.A5;
                case "a4":
                    return PageSize.A4;
                case "letter":
                    return PageSize.Letter;


            }
            return PageSize.A4;


        }
        protected virtual int GetAlignment()
        {
            //if we need the element to be opposite, like logo etc`.


            return Element.ALIGN_RIGHT;
        }
        protected virtual PdfPCell GetPdfCell(string text, Font font)
        {
            return new PdfPCell(new Phrase(text, font));
        }
        protected virtual PdfPCell GetPdfCell(object text, Font font)
        {
            return new PdfPCell(new Phrase(text.ToString(), font));
        }

        protected virtual Paragraph GetParagraph(string text, Font font, params object[] args)
        {
            return GetParagraph(text, string.Empty, font, args);
        }
        protected virtual Paragraph GetParagraph(string text, string indent, Font font, params object[] args)
        {
            var formatText = text;
            return new Paragraph(indent + (args.Any() ? string.Format(formatText, args) : formatText), font);
        }

        public void GetPDFOrder(OrderDetailDetailDtoModel detailDetailDtoModel)
        {
            Document doc = new Document(PageSize.Letter, 7f, 5f, 5f, 0f);
            Font mainFont = FontFactory.GetFont("Segoe UI", 22, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#999")));
            Phrase mainPharse = new Phrase();




        }

        protected virtual void PrintHeader(OrderDetailDtoModel order, Font font, Font titleFont, Document doc)
        {




            var headerTable = new PdfPTable(1)
            {
                RunDirection = GetDirection()


            };
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            //store info
            //  var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            //

            var cellHeaderWork = GetPdfCell(order.WorkName, titleFont);
            cellHeaderWork.Phrase.Add(new Phrase(Environment.NewLine));
            cellHeaderWork.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderWork.Border = Rectangle.BOTTOM_BORDER;
            headerTable.AddCell(cellHeaderWork);
            var cellHeader = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.OrderNumber")+": " + order.OrderNumber, titleFont);
            cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
            //cellHeader.Phrase.Add(new Phrase(anchor));
            cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
            cellHeader.Phrase.Add(GetParagraph(_sudeSessionContext.GetLocalResourceValue("Page.Content.OrderDate") + ": " + order.OrderDate.ToLocalizationDateTime("g"), font));
            cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
            cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
            if (!order.IsBuy)
            {
                cellHeader.Phrase.Add(GetParagraph(_sudeSessionContext.GetLocalResourceValue("Page.Content.Customer") + ": " + order.Customer.Title, font));
                cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
                cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
            }
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeader.Border = Rectangle.NO_BORDER;

            headerTable.AddCell(cellHeader);


            headerTable.WidthPercentage = 100f;



            doc.Add(headerTable);
        }


        protected virtual void PrintFooter(List<string> columnLines, PdfWriter pdfWriter, Rectangle pageSize, Font font)
        {



            if (!columnLines.Any())
                return;

            var totalLines = columnLines.Count();
            const float margin = 43;

            //if you have really a lot of lines in the footer, then replace 9 with 10 or 11
            var footerHeight = totalLines * 9;
            var directContent = pdfWriter.DirectContent;
            directContent.MoveTo(pageSize.GetLeft(margin), pageSize.GetBottom(margin) + footerHeight);
            directContent.LineTo(pageSize.GetRight(margin), pageSize.GetBottom(margin) + footerHeight);
            directContent.Stroke();

            var footerTable = new PdfPTable(1)
            {
                WidthPercentage = 100f,
                RunDirection = GetDirection()
            };
           footerTable.SetTotalWidth(new float[] { 100f });

            //column 1
            if (columnLines.Any())
            {
                var column1 = new PdfPCell(new Phrase())
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };

                Font f = font;
                f.Size = 5;
                foreach (var footerLine in columnLines)
                {
                    column1.Phrase.Add(new Phrase(footerLine, f));
                    column1.Phrase.Add(new Phrase(Environment.NewLine));
                }

                footerTable.AddCell(column1);
            }
            else
            {
                var column = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER };
                footerTable.AddCell(column);
            }



            footerTable.WriteSelectedRows(0, totalLines, pageSize.GetLeft(margin), pageSize.GetBottom(margin) + footerHeight, directContent);
        }


     
        protected virtual void PrintDetails(Font titleFont, Document doc, OrderDetailDtoModel order, Font font, Font attributesFont)
        {
            //var oDetails = new PdfPTable(1)
            //{
            //    RunDirection = GetDirection(),
            //    WidthPercentage = 100f
            //};
            //var cellProducts = GetPdfCell("@_SudeSessionContext.GetLocalResourceValue("Page.Button.Detail") سفارش", titleFont);
            //cellProducts.Border = Rectangle.NO_BORDER;
            //oDetails.AddCell(cellProducts);
            //doc.Add(oDetails);
            //doc.Add(new Paragraph(" "));

            //a vendor should have access only to products
            var orderItems = order.OrderDetails;

            int fontSize = Convert.ToInt32(font.Size);

            var detailsTable = new PdfPTable(5)
            {
                RunDirection = GetDirection(),
                WidthPercentage = 100f
            };

            var widths = new Dictionary<int, int[]>
            {
                { 6, new[] {10, 40, 15, 15 ,20} },
                { 7, new[] {8, 45, 13, 13, 21} },
                { 9, new[] { 5, 50, 15, 15, 15 } }
            };

            detailsTable.SetWidths(widths[fontSize].Reverse().ToArray());


            var cellProductItem = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.Row") , font);
            cellProductItem.BackgroundColor = BaseColor.LightGray;
            cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
            detailsTable.AddCell(cellProductItem);
            //product name
            cellProductItem = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.ServiceTitle") , font);
            cellProductItem.BackgroundColor = BaseColor.LightGray;
            cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
            detailsTable.AddCell(cellProductItem);

            //SKU


            //Vendor name


            //price
            cellProductItem = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.Price")  , font);
            cellProductItem.BackgroundColor = BaseColor.LightGray;
            cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
            detailsTable.AddCell(cellProductItem);

            //qty
            cellProductItem = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.Count"), font);
            cellProductItem.BackgroundColor = BaseColor.LightGray;
            cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
            detailsTable.AddCell(cellProductItem);

            //total
            cellProductItem = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.Sum"), font);
            cellProductItem.BackgroundColor = BaseColor.LightGray;
            cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
            detailsTable.AddCell(cellProductItem);

            int rownumber = 0;
            double sum = 0;
            foreach (var detail in orderItems)
            {
                //  var product = _productService.GetProductById(orderItem.ProductId);
                rownumber++;
                var pAttribTable = new PdfPTable(1) { RunDirection = GetDirection() };
                pAttribTable.DefaultCell.Border = Rectangle.NO_BORDER;
                pAttribTable.AddCell(new Paragraph(rownumber.ToString(), font));
                cellProductItem.AddElement(new Paragraph(rownumber.ToString(), font));
                cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
                detailsTable.AddCell(pAttribTable);


                //cellProductItem = GetPdfCell(rownumber++.ToString(), font);         
                //cellProductItem.HorizontalAlignment = Element.ALIGN_RIGHT;
                //detailsTable.AddCell(cellProductItem);

                cellProductItem = GetPdfCell(detail.ServingName, font);
                cellProductItem.HorizontalAlignment = Element.ALIGN_LEFT;
                detailsTable.AddCell(cellProductItem);

                cellProductItem = GetPdfCell(String.Format("{0:n0}", detail.Price), font);
                cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
                detailsTable.AddCell(cellProductItem);

                cellProductItem = GetPdfCell(String.Format("{0:n0}", detail.Count), font);
                cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
                detailsTable.AddCell(cellProductItem);

                cellProductItem = GetPdfCell(String.Format("{0:n0}", detail.Count * detail.Price), font);
                cellProductItem.HorizontalAlignment = Element.ALIGN_CENTER;
                detailsTable.AddCell(cellProductItem);
                sum += detail.Count * detail.Price;



            }

            doc.Add(detailsTable);

            var statusTable = new PdfPTable(3)
            {
                RunDirection = GetDirection(),
                WidthPercentage = 100f
            };

            var widthsStatus = new Dictionary<int, int[]>
            {
                { 6, new[] {35, 15, 50 } },
                  { 7, new[] {34, 13, 53 } },
                    { 9, new[] {30, 15, 55 } },

            };

            statusTable.SetWidths(widthsStatus[fontSize].ToArray());
            var cellstatus = GetPdfCell( order.PaymentStatusTitle, titleFont);
            cellstatus.Border = Rectangle.NO_BORDER;
            statusTable.AddCell(cellstatus);
            cellstatus = GetPdfCell(_sudeSessionContext.GetLocalResourceValue("Page.Content.Sum"), titleFont);
            cellstatus.Border = Rectangle.BOX;
            statusTable.AddCell(cellstatus);

            cellstatus = GetPdfCell(String.Format("{0:n0}", sum), titleFont);
            cellstatus.Border = Rectangle.BOX;
            statusTable.AddCell(cellstatus);
            doc.Add(statusTable);


        }

      

        public virtual void PrintOrdersToPdf(Stream stream, OrderDetailDtoModel order, string pSize)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var pageSize = GetPageSize(pSize) ;

            //if (_pdfSettings.LetterPageSizeEnabled)
            //{
            //    pageSize = PageSize.Letter;
            //}

            var doc = new Document(pageSize);

            var pdfWriter = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            //fonts
            var titleFont = GetFont(pageSize);
            titleFont.SetStyle(Font.BOLD);
            titleFont.Color = BaseColor.Black;
            var font = GetFont(pageSize);
            var attributesFont = GetFont(pageSize);
            attributesFont.SetStyle(Font.ITALIC);





            //by default _pdfSettings contains settings for the current active store
            //and we need PdfSettings for the store which was used to place an order
            //so let's load it based on a store of the current order



            //header
            PrintHeader(order, font, titleFont, doc);


            PrintDetails(titleFont, doc, order, font, attributesFont);



            PrintFooter(new List<string> { _sudeSessionContext.GetLocalResourceValue("Page.Content.PrintTime") +": "+DateTime.Now.ToLocalizationDateTime("G") }, pdfWriter, pageSize, font);




            doc.Close();
        }




    }
}
