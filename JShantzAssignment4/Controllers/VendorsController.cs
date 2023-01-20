using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JShantzAssignment4.Models.DbGenerated;
using JShantzAssignment4.Models;
using Microsoft.EntityFrameworkCore;

namespace JShantzAssignment4.Controllers
{
    public class VendorsController : Controller
    {
        private apContext _apContext;

        public VendorsController(apContext apContext)
        {
            _apContext = apContext;
        }

        public IActionResult Index(string activeCategory)
        {
            List<Vendor> vendors = new List<Vendor>();
            string[] letters;
            if(activeCategory == "A - E" || activeCategory == null)
            {
                ViewData["ActiveCategory"] = "A - E";
                letters = new string[] { "A", "B", "C", "D", "E" };
            }
            else if(activeCategory == "F - K")
            {
                ViewData["ActiveCategory"] = "F - K";
                letters = new string[] { "F", "G", "H", "I", "J", "K" };
            }
            else if (activeCategory == "L - R")
            {
                ViewData["ActiveCategory"] = "L - R";
                letters = new string[] { "L", "M", "N", "O", "P", "Q", "R" };
            }
            else
            {
                ViewData["ActiveCategory"] = "S - Z";
                letters = new string[] { "S", "T", "U", "V", "W", "X", "Y", "Z" };
            }

            foreach (string letter in letters)
            {
                vendors.AddRange(_apContext.Vendors.Where(v => (v.VendorName.StartsWith(letter))
                    && (v.IsDeleted == false)).OrderBy(v => v.VendorName).ToList());
            }

            return View(vendors);
        }
    }
}
