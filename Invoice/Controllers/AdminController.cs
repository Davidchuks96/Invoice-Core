using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Invoice.Core.Entity;
using Invoice.Core.Interfaces;
using Invoice.Core.ViewModel;
using Invoice.Data.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;

namespace Invoice.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreSettingRepository storeSettingRepository;
        private readonly ISaleRepository saleRepository;

        public AdminController(ICustomerRepository customerRepository,
            IProductRepository productRepository, IStoreSettingRepository storeSettingRepository,
            ISaleRepository saleRepository)
        {

            this.customerRepository = customerRepository;
            _productRepository = productRepository;
            this.storeSettingRepository = storeSettingRepository;
            this.saleRepository = saleRepository;
        }
        public IActionResult Index()
        {
            var dashboard= new DashboardViewModel
            {
                Customers = customerRepository.All().Count(),
                Products = _productRepository.All().Count(),
                TotalSales = saleRepository.All().Count(),
                TotalSaleValue = saleRepository.All().Sum(x => x.GrandTotal),
                LastFiveSales = saleRepository.All().OrderByDescending(x => x.SalesDate).Take(5).ToList()
            };
            return View(dashboard);
        }


        #region customer
        public IActionResult CustomerList()
        {
            return View(customerRepository.All());
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Insert(model);
                return RedirectToAction("CustomerList");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditCustomer(int customerId)
        {
            var customer = customerRepository.Find(customerId);
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            customerRepository.Update(model, model.Id);
            return RedirectToAction("CustomerList");
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            var customer = customerRepository.Find(customerId);
            if (customer != null)
            {
                customerRepository.Delete(customer);
                return RedirectToAction("CustomerList");
            }
            return RedirectToAction("CustomerList");
        }
        #endregion

        #region Product
        public IActionResult ProductList()
        {
            var products = _productRepository.All();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Insert(model);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            var product = _productRepository.Find(productId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Update(model, model.Id);
            return RedirectToAction("ProductList");
        }
        public ActionResult DeleteProduct(int productId)
        {
            var product = _productRepository.Find(productId);
            if (product != null)
            {
                _productRepository.Delete(product);
                return RedirectToAction("ProductList");
            }
            return RedirectToAction("ProductList");
        }


        [HttpGet]
        public ActionResult BarcodeGenerate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BarcodeGenerate(int productId, int quantity)
        {
            //{
            //    Document doc = new Document(new iTextSharp.text.Rectangle(24, 12), 5, 5, 1, 1);
            //    try
            //    {

            //        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(
            //          Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/codes.pdf", FileMode.Create));
            //        doc.Open();

            //        DataTable dt = new DataTable();
            //        dt.Columns.Add("ID");
            //        dt.Columns.Add("Price");
            //        for (int i = 0; i < 20; i++)
            //        {
            //            DataRow row = dt.NewRow();
            //            row["ID"] = "ZS00000000000000" + i.ToString();
            //            row["Price"] = "100," + i.ToString();
            //            dt.Rows.Add(row);
            //        }
            //        System.Drawing.Image img1 = null;
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (i != 0)
            //                doc.NewPage();
            //            PdfContentByte cb1 = writer.DirectContent;
            //            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            //            cb1.SetFontAndSize(bf, 2.0f);
            //            cb1.BeginText();
            //            cb1.SetTextMatrix(1.2f, 9.5f);
            //            cb1.ShowText("Safi Garments");
            //            cb1.EndText();

            //            PdfContentByte cb2 = writer.DirectContent;
            //            BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            cb2.SetFontAndSize(bf1, 1.3f);
            //            cb2.BeginText();
            //            cb2.SetTextMatrix(17.5f, 1.0f);
            //            cb2.ShowText(dt.Rows[i]["Price"].ToString());
            //            cb2.EndText();

            //            iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
            //            iTextSharp.text.pdf.Barcode128 bc = new Barcode128();
            //            bc.TextAlignment = Element.ALIGN_LEFT;
            //            bc.Code = dt.Rows[i]["ID"].ToString();
            //            bc.StartStopText = false;
            //            bc.CodeType = iTextSharp.text.pdf.Barcode128.EAN13;
            //            bc.Extended = true;

            //            //System.Drawing.Image bimg = 
            //            //  bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
            //            //img1 = bimg;

            //            iTextSharp.text.Image img = bc.CreateImageWithBarcode(cb,
            //              iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK);

            //            cb.SetTextMatrix(1.5f, 3.0f);
            //            img.ScaleToFit(60, 5);
            //            img.SetAbsolutePosition(1.5f, 1);
            //            cb.AddImage(img);
            //        }
            //    }
            //    catch
            //    {
            //    }
            //    finally
            //    {
            //        doc.Close();
            //    }

            if (ModelState.IsValid)
            {
                ProductReport paymentReport = new ProductReport();
                byte[] bytes = paymentReport.CreateReport();
                return File(bytes, "application/pdf");
            }
            return RedirectToAction("BarcodeGenerate");
        }
        #endregion

        #region Sales
        public IActionResult SalesList()
        {
            return View();
        }
        #endregion

        #region store
        [HttpGet]
        public IActionResult Settings()
        {
            var settings = storeSettingRepository.All().FirstOrDefault();
            if (settings != null)
                return View(settings);
            return View(new StoreSettingModel());
        }
        [HttpPost]


        public IActionResult Settings(StoreSettingModel model, IFormFile logoPostedFileBase)
        {
            if (!ModelState.IsValid) return View(model);
            if (logoPostedFileBase != null && logoPostedFileBase.Length > 0)
            {
                var path = Path.Combine(
                 Directory.GetCurrentDirectory(), "wwwroot/Images",
                 logoPostedFileBase.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    logoPostedFileBase.CopyTo(stream);
                }
                model.Logo = $"/Images/{logoPostedFileBase.FileName}";
            }
            else
            {
                var settings = storeSettingRepository.Find(model.Id);
                if (settings != null)
                    model.Logo = settings.Logo;
            }
            storeSettingRepository.Update(model, model.Id);
            return RedirectToAction("Settings");
        }
        #endregion

        #region json
        public JsonResult Products(string query)
        {
            return Json(_productRepository.All().Where(x => x.Name.ToLower().Contains(query.ToLower())));
        }
        #endregion

    }
}