using System;
using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Lib.zz
{
    public class Registration
    {
        public int FormId { get; set; } =(int) Forms.Registration;

        [Required]
        [Display(Name = "Academy")]
        public string Academy { get; set; }

        [Required]
        [Display(Name = "Age Group")]
        public string AgeGroup { get; set; }

        [Required]
        [Display(Name = "Player Gender")]
        public string PlayerGender { get; set; }

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
        public string PostalCode { get; set; }

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
        [Display(Name = "Guardian #1 Full Name")]
        public string PrimaryGuardianFullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Guardian #1 Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string PrimaryGuardianContactNumber { get; set; }

        [Phone]
        [Display(Name = "Guardian #1 Alt. Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string PrimaryGuardianAltContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Guardian #1 Email Address")]
        public string PrimaryGuardianEmail { get; set; }

        [Required]
        [Display(Name = "Guardian #2 Full Name")]
        public string SecondaryGuardianFullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Guardian #2 Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string SecondaryGuardianContactNumber { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Guardian #2 Alt. Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string SecondaryGuardianContactNumber2 { get; set; }

        [Required]
        [Display(Name = "Emergency Contact's Full Name ")]
        public string EmergencyContactFullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Emergency Contact's Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string EmergencyContactNumber { get; set; }
        
        //[Required]
        [Display(Name = "Doctor to Notify Full Name")]
        public string DoctorFullName { get; set; }

        //[Required]
        [Phone]
        [Display(Name = "Doctor's Phone")]
        [RegularExpression(@"(\+\d)?\d{10}(\s)?(x\d+)?$", ErrorMessage = "Valid phone# formats: 6479991234, +16479991234, 6479991234 x1234, +16479991234 x1234")]
        public string DoctorContactNumber { get; set; }

        [Display(Name = "List all Medical Conditions, Sensitivities and Observations")]
        public string MedicalConditions { get; set; }

        [Required]
        [Display(Name = "How did you hear about us?")]
        public string HowDidYouHearAboutUs { get; set; }

        [Required]
        [Display(Name = "I consent to the medical treatment of a minor")]
        public bool ConsentToMedicalTreatmentOfMinor { get; set; }

        [Required]
        public bool LiabilityWaiverAgreement { get; set; }

        [Required]
        public bool PlayerCommitmentAgreement { get; set; }

        [Required]
        public bool ModelImageReleaseAgreement { get; set; }

        [Required]
        public bool HarassmentPolicy { get; set; }

        [Required]
        public bool ConcussionPolicy { get; set; }

        [Required]
        public bool FinancialContractAgreement { get; set; }

        [Required]
        public bool PlayerAndParentsCodeOfConduct { get; set; }

        [Required]
        public bool CompetetiveUniformsPolicy { get; set; }

        [Required]
        public bool SummerCampParticipationAgreement { get; set; }

        [Required]
        public bool GeneralTermsAgreement { get; set; }

    }


}
