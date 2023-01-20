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
    public class VendorController : Controller
    {
        private apContext _apContext;

        public VendorController(apContext apContext)
        {
            _apContext = apContext;
        }

        [HttpGet]
        public IActionResult Add(string activeCategory)
        {
            ViewData["ActiveCategory"] = activeCategory;

            ViewData["Action"] = "Add";

            ViewData["Terms"] = _apContext.Terms.OrderBy(t => t.TermsDueDays).ToList();

            ViewData["Accounts"] = _apContext.GeneralLedgerAccounts.OrderBy(g => g.AccountNumber).ToList();

            return View("Edit", new Vendor());
        }

        [HttpPost]
        public IActionResult Add(Vendor newVendor, string activeCategory)
        {
            if (ModelState.IsValid)
            {
                _apContext.Vendors.Add(newVendor);
                _apContext.SaveChanges();

                return RedirectToAction("Index", "Vendors", new { activeCategory = activeCategory });
            }
            else
            {
                ViewData["ActiveCategory"] = activeCategory;
                ViewData["Action"] = "Add";
                ViewData["Terms"] = _apContext.Terms.OrderBy(t => t.TermsDueDays).ToList();
                ViewData["Accounts"] = _apContext.GeneralLedgerAccounts.OrderBy(g => g.AccountNumber).ToList();
                ModelState.AddModelError("", "There were errors in the fom - please fix them and try again.");
                return View("Edit", newVendor);
            }
        }

        [HttpGet]
        public IActionResult Edit(int vendorId ,string activeCategory)
        {
            ViewData["ActiveCategory"] = activeCategory;
            ViewData["Action"] = "Edit";
            ViewData["Terms"] = _apContext.Terms.OrderBy(t => t.TermsDueDays).ToList();
            ViewData["Accounts"] = _apContext.GeneralLedgerAccounts.OrderBy(g => g.AccountNumber).ToList();

            Vendor vendor = _apContext.Vendors.Find(vendorId);

            return View("Edit", vendor);
        }

        [HttpPost]
        public IActionResult Edit(Vendor vendor, string activeCategory)
        {
            if (ModelState.IsValid)
            {
                _apContext.Vendors.Update(vendor);
                _apContext.SaveChanges();

                return RedirectToAction("Index", "Vendors", new { activeCategory = activeCategory });
            }
            else
            {
                ViewData["ActiveCategory"] = activeCategory;
                ViewData["Action"] = "Edit";
                ViewData["Terms"] = _apContext.Terms.OrderBy(t => t.TermsDueDays).ToList();
                ViewData["Accounts"] = _apContext.GeneralLedgerAccounts.OrderBy(g => g.AccountNumber).ToList();
                ModelState.AddModelError("", "There were errors in the fom - please fix them and try again.");

                return View("Edit", vendor);
            }
        }

        public IActionResult Delete(int vendorId, string activeCategory)
        {
            Vendor vendor = _apContext.Vendors.Find(vendorId);
            vendor.IsDeleted = true;
            _apContext.Vendors.Update(vendor);
            _apContext.SaveChanges();
            TempData["Message"] = vendor.VendorName;
            TempData["vendorId"] = vendor.VendorId;
            return RedirectToAction("Index", "Vendors", new { activeCategory = activeCategory });
        }

        public IActionResult Undo(int vendorId, string activeCategory)
        {
            Vendor vendor = _apContext.Vendors.Find(vendorId);
            vendor.IsDeleted = false;
            _apContext.Vendors.Update(vendor);
            _apContext.SaveChanges();
            return RedirectToAction("Index", "Vendors", new { activeCategory = activeCategory });
        }
    }
}
