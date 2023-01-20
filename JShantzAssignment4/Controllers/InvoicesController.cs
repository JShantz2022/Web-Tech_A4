using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JShantzAssignment4.Models;
using JShantzAssignment4.Models.DbGenerated;
using Microsoft.EntityFrameworkCore;

namespace JShantzAssignment4.Controllers
{
    public class InvoicesController : Controller
    {
        private apContext _apContext;

        public InvoicesController(apContext apContext)
        {
            _apContext = apContext;
        }

        public IActionResult Index(int vendorId, int invoiceId, string activeCategory)
        {
            var invoices = _apContext.Invoices.Where(i => i.VendorId == vendorId).OrderBy(i => i.InvoiceId).ToList();

            var vendor = _apContext.Vendors.Find(vendorId);

            ViewBag.Vendor = vendor;
            ViewBag.Term = _apContext.Terms.Find(vendor.DefaultTermsId);
            ViewBag.Invoices = invoices;


            if (invoiceId == 0 && invoices.Count() != 0)
            {
                invoiceId = invoices[0].InvoiceId;
            }

            var invoiceLines = _apContext.InvoiceLineItems.Where(il => il.InvoiceId == invoiceId).OrderBy(il => il.InvoiceSequence).ToList();

            ViewData["ActiveCategory"] = activeCategory;
            ViewData["ActiveInvoice"] = invoiceId;
            ViewData["newInvoiceSequence"] = invoiceLines.Count() + 1;
            ViewBag.InvoiceLines = invoiceLines;

            return View(new InvoiceLineItem());
        }

        public IActionResult Add(InvoiceLineItem lineItem, string activeCategory)
        {
            Invoice invoice = _apContext.Invoices.Find(lineItem.InvoiceId);

            invoice.InvoiceTotal += lineItem.LineItemAmount;
            invoice.PaymentTotal += lineItem.LineItemAmount;

            _apContext.InvoiceLineItems.Add(lineItem);
            _apContext.Invoices.Update(invoice);
            _apContext.SaveChanges();

            return RedirectToAction("Index", "Invoices", new { vendorId = invoice.VendorId ,invoiceId = lineItem.InvoiceId, activeCategory = activeCategory});
        }
    }
}
