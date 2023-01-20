using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using JShantzAssignment4.Models.DbGenerated;
using JShantzAssignment4.Models;

namespace JShantzAssignment4.Controllers
{
    public class ValidationController : Controller
    {
        private apContext _apContext;

        public ValidationController(apContext apContext)
        {
            _apContext = apContext;
        }

        public IActionResult CheckPhone(string vendorPhone)
        {
            string msg;
            if(Regex.IsMatch(vendorPhone, @"^[(\(\d{3}\))(\d{3}-)]\d{3}-\d{4}$"))
            {
                msg = CheckIffPhoneIsInDb(vendorPhone);
                if (string.IsNullOrEmpty(msg))
                {
                    TempData["okPhone"] = true;
                    return Json(true);
                }
            }
            else
            {
                msg = "Phone number must be in the format \"(###) ###-####\"";
            }
            return Json(msg);
        }

        private string CheckIffPhoneIsInDb(string phone)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(phone))
            {
                var vendor = _apContext.Vendors.Where(v => v.VendorPhone == phone).FirstOrDefault();
                if(vendor != null)
                {
                    msg = $"The phone number {phone} is already use";
                }
            }
            return msg;
        }
    }
}
