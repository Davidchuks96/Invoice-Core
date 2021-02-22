using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Core.Entity;
using Invoice.Core.Interfaces;
using Invoice.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PharmaApp.Web.Reports;

namespace Invoice.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISaleRepository saleRepository;
        private readonly ISaleItemRepository saleItemRepository;
        private readonly IStoreSettingRepository storeSettingRepository;
        private readonly IConfiguration configuration;

        public SalesController(ICustomerRepository customerRepository, ISaleRepository saleRepository,
            ISaleItemRepository saleItemRepository, IStoreSettingRepository storeSettingRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customerRepository = customerRepository;
            this.saleRepository = saleRepository;
            this.saleItemRepository = saleItemRepository;
            this.storeSettingRepository = storeSettingRepository;
           
        }

        public IActionResult Index()
        {
            return View(saleRepository.All());
        }
        [HttpGet]
        public IActionResult AddSale()
        {
            ViewBag.customers = customerRepository.GetAllForDropDown();
            return View();
        }
        [HttpPost]
        public IActionResult AddSale([FromBody] SalesModel model)
        {
            if (ModelState.IsValid)
            {
                saleRepository.Insert(model);
                return Json(new { error = false, message = "Sales saved successfully" });
            }
            return Json(new { error = true, message = "failed to save Sales" });
        }
        [HttpGet]
        public IActionResult EditSale(int saleId)
        {
            return View(model: saleId);
        }
        [HttpPost]
        public IActionResult EditSale(SalesModel model)
        {
            if (model != null)
            {
                var items = saleItemRepository.All().Where(x => x.SalesId == model.Id).ToList();
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        saleItemRepository.Delete(item);
                    }
                }

                var sale = saleRepository.Find(model.Id);
                sale.Notes = model.Notes;
                sale.PaymentMethod = model.PaymentMethod;
                sale.SaleCode = model.SaleCode;
                sale.CustomerId = model.CustomerId;
                sale.Total = model.Total;
                sale.SalesDate = model.SalesDate;
                sale.Status = model.Status;
                sale.Discount = model.Discount;
                sale.GrandTotal = model.GrandTotal;

                //add again
                foreach (var item in model.Items)
                {
                    sale.Items.Add(new SalesItemsModel
                    {
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        Name = item.Name
                    });
                }
                saleRepository.Update(sale, model.Id);
                return Json(new { error = false, message = "Sales updated successfully" });
            }
            return Json(new { error = true, message = "failed to update Sales" });
        }

        public IActionResult DeleteSale(int saleId)
        {
            var item = this.saleRepository.Find(saleId);
            saleRepository.Delete(item);
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult SalesReport(DateTime? From, DateTime? To)
        {
            if (From.HasValue && To.HasValue)
                return View(saleRepository.All().Where(x => x.SalesDate >= From && x.SalesDate <= To));
            return View(new List<SalesModel>());
        }

        public ActionResult SaleInvoice(int saleId, int style)
        {
            if (storeSettingRepository.All().Count() == 0)
            {
                TempData["Msg"] = "Setup store setting first then print sale invoice";
                return RedirectToAction("Index");
            }
            var store = storeSettingRepository.All().FirstOrDefault();

            var sale = saleRepository.All().Include(x => x.CustomerModel).SingleOrDefault(x => x.Id == saleId);
            sale.Items = saleItemRepository.All().Where(x => x.SalesId == saleId).ToList();
            if (sale != null)
            {
                var sales = new SaleReportViewModel
                {
                    company = store,
                    Sales = sale
                };
                if (style == 1)
                {
                    SalesReport paymentReport = new SalesReport(configuration);
                    byte[] bytes = paymentReport.CreateReport(sales);
                    return File(bytes, "application/pdf");
                }
                if (style == 2)
                {
                    SalesReportSmall paymentReport = new SalesReportSmall();
                    byte[] bytes = paymentReport.CreateReport(sales);
                    return File(bytes, "application/pdf");
                }
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public JsonResult GetSales(int saleId)
        {
            try
            {
                SalesModel sales = saleRepository.All().FirstOrDefault(x => x.Id == saleId);
                sales.Items = saleItemRepository.All().Where(x => x.SalesId == sales.Id).ToList();
                //SalesItemsModel a = new SalesItemsModel { Name = "abc", Amount = 2.5, Id = 1, IsDelete = false, Price = 2.5, Quantity = 1, SalesId = 5};
                //sales.Items.Add(a);
                return Json(sales);
            }
            catch (Exception e){
                return Json(e.ToString());
            }
           
        }

        public JsonResult GetCustomers()
        {
            return Json(customerRepository.All());
        }
    }
}