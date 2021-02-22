using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Data.Reports
{
    public class ProductReport
    {
        #region Declaration

        private int _totalColumn = 5;
        private Document _document;
        private Font _fontStyle;
        private Font _fontStyleNormal;
        private PdfPTable _pdfPTable;
        private PdfPCell _pdfPCell;
        private MemoryStream _memoryStream;
        private PdfWriter writer;

        private PdfPTable paymentTable;
       
        public ProductReport()
        {

            paymentTable = new PdfPTable(2);
            _pdfPTable = new PdfPTable(_totalColumn);
            _memoryStream = new MemoryStream();
            

        }



        #endregion
        public byte[] CreateReport()
        {

            #region page
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            writer = PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfPTable.SetWidths(new float[] { 40f, 150f, 80f, 80f, 80f });

            paymentTable.WidthPercentage = 100;
            paymentTable.HorizontalAlignment = Element.ALIGN_LEFT;
            paymentTable.SetWidths(new float[] { 100f, 100f });

            this.reportHeader();
          

            _pdfPTable.HeaderRows = 2;
            _document.Add(paymentTable);
            _document.Add(_pdfPTable);
            _document.Close();
            return _memoryStream.ToArray();

            #endregion

        }
        private void reportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfPCell = new PdfPCell(new Phrase("PRODUCT NAME PRODUCT NAME  : ", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);


            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Price : 10$", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfPCell = new PdfPCell(new Phrase("0000000000000000000", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();
        }


        //private void reportBody()
        //{
        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);

        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    paymentTable.AddCell(_pdfPCell);
        //    paymentTable.CompleteRow();

        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    paymentTable.AddCell(_pdfPCell);
        //    paymentTable.CompleteRow();

        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    paymentTable.AddCell(_pdfPCell);
        //    paymentTable.CompleteRow();



        //    _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
        //    _pdfPCell = new PdfPCell(new Phrase("PRODUCT LIST PURCHASED BY CUSTOMER", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();

        //    _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();




        //    #region TableHeader



        //    _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
        //    _pdfPCell = new PdfPCell(new Phrase("SL No", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("Item", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("Price", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("Quantity", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("Amount", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    _pdfPTable.AddCell(_pdfPCell);


        //    _pdfPTable.CompleteRow();
        //    #endregion

        //    #region Table body
        //    _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);

        //    int sl = 0;
        //    //foreach (var item in _model.Sales.Items)
        //    //{
        //    //    ++sl;
        //    //    _pdfPCell = new PdfPCell(new Phrase(sl.ToString(), _fontStyle));
        //    //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    //    _pdfPTable.AddCell(_pdfPCell);

        //    //    _pdfPCell = new PdfPCell(new Phrase(item.Name, _fontStyle));
        //    //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    //    _pdfPTable.AddCell(_pdfPCell);

        //    //    _pdfPCell = new PdfPCell(new Phrase(item.Price.ToString(), _fontStyle));
        //    //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    //    _pdfPTable.AddCell(_pdfPCell);

        //    //    _pdfPCell = new PdfPCell(new Phrase(item.Quantity.ToString(), _fontStyle));
        //    //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    //    _pdfPTable.AddCell(_pdfPCell);

        //    //    _pdfPCell = new PdfPCell(new Phrase(item.Amount.ToString(), _fontStyle));
        //    //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    //    _pdfPTable.AddCell(_pdfPCell);
        //    //    _pdfPTable.CompleteRow();

        //    //}

        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();


        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
        //    _pdfPCell = new PdfPCell(new Phrase("Total :", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    _pdfPCell.Colspan = 4;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("5000", _fontStyle));
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPTable.CompleteRow();


        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
        //    _pdfPCell = new PdfPCell(new Phrase("Discount :", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    _pdfPCell.Colspan = 4;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("50", _fontStyle));
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPTable.CompleteRow();



        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
        //    _pdfPCell = new PdfPCell(new Phrase("Grand Total :", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    _pdfPCell.Colspan = 4;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("0000", _fontStyle));
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPTable.CompleteRow();



        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
        //    _pdfPCell = new PdfPCell(new Phrase("Status  :", _fontStyle));
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    _pdfPCell.Colspan = 4;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPCell = new PdfPCell(new Phrase("", _fontStyle));
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPTable.AddCell(_pdfPCell);

        //    _pdfPTable.CompleteRow();

        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();
        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();
        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();
        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();
        //    _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
        //    _pdfPCell.Colspan = 5;
        //    _pdfPCell.Border = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();





        //    #endregion


        //    _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);
        //    _pdfPCell = new PdfPCell(new Phrase(" Thanks for your shoping.", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();


        //    _pdfPCell = new PdfPCell(new Phrase("Sales Invoice generated by : Kodauthor, contact : facebook.com/kodauthor ", _fontStyle));
        //    _pdfPCell.Colspan = _totalColumn;
        //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //    _pdfPCell.Border = 0;
        //    _pdfPCell.BackgroundColor = BaseColor.WHITE;
        //    _pdfPCell.ExtraParagraphSpace = 0;
        //    _pdfPTable.AddCell(_pdfPCell);
        //    _pdfPTable.CompleteRow();
        //}

    }
}
