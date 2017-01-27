using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class CompareDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private string message;
        public CompareDateAttribute(string message)
        {
            this.message = message;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //throw new NotImplementedException();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Event evnt = (Event)validationContext.ObjectInstance;

            if (evnt.StartDate > evnt.EndDate || evnt.StartDate < DateTime.Now)
            {
                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
