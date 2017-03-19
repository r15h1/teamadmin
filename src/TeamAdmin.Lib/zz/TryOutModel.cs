using System;
using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Lib.zz
{
    public enum Forms
    {
        TryOut = 1,
        SummerCampRegistration = 2,
        Registration = 3
    }

    public class TryOutModel
    {
        public int FormId { get; set; } =(int) Forms.TryOut;

        [Required]
        [Display(Name = "Would like to try out for")]
        public string Academy { get; set; }

        [Required]
        [Display(Name = "Age Group")]
        public string AgeGroup { get; set; }

        [Required]
        [Display(Name = "Player Gender")]
        public string PlayerGender { get; set; }

        [Display(Name = "Player's Registration Number (if already registered)")]
        public string PlayerRegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Player's Last Name")]
        public string PlayerLastName { get; set; }

        [Required]
        [Display(Name = "Player's First Name")]
        public string PlayerFirstName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Primary Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string PrimaryContactNumber { get; set; }

        [Phone]
        [Display(Name = "Secondary Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string SecondaryContactNumber { get; set; }

        [Required]
        [Display(Name = "Player's Date Of Birth")]
        public DateTime PlayerDateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Player's Primary Email Address")]        
        public string PlayerEmail { get; set; }

        [Required]
        [Display(Name = "Primary Guardian's Full Name")]
        public string PrimaryGuardianFullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Primary Guardian's Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string PrimaryGuardianContactNumber { get; set; }

        [Phone]
        [Display(Name = "Primary Guardian's Alt. Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string PrimaryGuardianAltContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Primary Guardian's Email Address")]
        public string PrimaryGuardianEmail { get; set; }

        [Display(Name = "Secondary Guardian's Full Name")]
        public string SecondaryGuardianFullName { get; set; }

        [Phone]
        [Display(Name = "Secondary Guardian's Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string SecondaryGuardianContactNumber { get; set; }

        [Required]
        [Display(Name = "Emergency Contact's Full Name ")]
        public string EmergencyContactFullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Emergency Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string EmergencyContactNumber { get; set; }
        
        //[Required]
        [Display(Name = "Doctor to Notify Full Name")]
        public string DoctorFullName { get; set; }

        //[Required]
        [Phone]
        [Display(Name = "Doctor's Contact #")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string DoctorContactNumber { get; set; }

        [Display(Name = "Medical Conditions")]
        public string MedicalConditions { get; set; }

        [Required]
        [Display(Name = "How did you hear about us?")]
        public string HowDidYouHearAboutUs { get; set; }

        [Required]
        [Display(Name = "I consent to the medical treatment of a minor")]
        public bool ConsentToMedicalTreatmentOfMinor { get; set; }
    }
}
