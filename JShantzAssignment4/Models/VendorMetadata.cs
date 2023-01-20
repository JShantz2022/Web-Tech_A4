using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JShantzAssignment4.Models
{
    public class VendorMetadata
    {
        [Required(ErrorMessage = "Please enter a name")]
        [RegularExpression("^[a-zA-Z\\d\\s]+$", ErrorMessage = "Name can not contain special characters")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Please enter address 1")]
        public string VendorAddress1 { get; set; }

        [Required(ErrorMessage = "Please enter a city")]
        public string VendorCity { get; set; }

        [Required(ErrorMessage = "Please enter a state")]
        [State(ErrorMessage = "State must be a valid state code")]
        public string VendorState { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Zip code must be 5 digits")]
        public string VendorZipCode { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [Remote("CheckPhone", "Validation")]
        public string VendorPhone { get; set; }

        [Required(ErrorMessage = "Please choose a default term")]
        public int DefaultTermsId { get; set; }

        [Required(ErrorMessage = "Plese enter a default account number")]
        public int DefaultAccountNumber { get; set; }
    }
}
