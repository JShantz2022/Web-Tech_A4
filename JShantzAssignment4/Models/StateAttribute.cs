using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JShantzAssignment4.Models
{
    public class StateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] states = {"AL","AK","AS","AZ","AR","CA","CO","CT","DE","DC","FL","GA","GU","HI","ID","IL","IN","IA","KS","KY",
                "LA","ME","MD","MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","MP","OH","OK","OR","PA","PR",
                "RI","SC","SD","TN","TX","UT","VT","VA","VI","WA","WV","WI","WY"};

            if (states.Contains(((string)value).ToUpper()))
            {
                return ValidationResult.Success;
            }

            string msg = base.ErrorMessage ?? $"{validationContext.DisplayName} must be a valid state code";
            return new ValidationResult(msg);
        }
    }
}
