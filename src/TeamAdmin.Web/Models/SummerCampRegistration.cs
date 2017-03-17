using System;
using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Web
{
    public class SummerCampRegistration
    {

        [Display(Name = "Ages 5-7 / 9am to 11am / $120 + HST")]
        public bool? Ages5To7_9amTo11am_120 { get; set; }

        [Display(Name = "Ages 8-18 / 9am - 12pm / $150 + HST")]
        public bool? Ages8To18_9amTo12pm_150 { get; set; }

        [Display(Name = "Ages 8-18 / 9am - 3pm / $250 + HST")]
        public bool? Ages8To18_9amTo3pm_250 { get; set; }

        [Display(Name = "Team Training / 4pm - 6pm / $45 + HST")]
        public bool? TeamTraining_4pmTo6pm_45 { get; set; }

        [Display(Name = "Coaches Clinics 6pm - 8pm / $110 + HST for 3 days")]
        public bool? CoachesClinics_6pmTo8pm_110 { get; set; }

        [Display(Name = "Parents Education Session / Saturday 15th July 6pm-8pm / $30 per family mom & dad")]
        public bool? ParentsEducationSession { get; set; }

        [Required]
        [Display(Name = "Player Full Name")]
        public string PlayerFullName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth{ get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal / Zip Code")]
        public string PostalOrZipCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Player Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Player Phone #")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "T-Shirt Size (Price Included)")]
        public string TShirtSize { get; set; }

        [Required]
        [Display(Name = "Indicate Any Medical Consition")]
        public string MedicalCondition { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Player Grade / Name of Current School")]
        public string PlayerGradeAndSchool { get; set; }

        [Required]
        [Display(Name = "How Did You Hear About Our Summer Camp")]
        public string HowDidYouHearAboutSummerCamp { get; set; }

        [Required]
        [Display(Name = "By checking this WAIVER FORM I am fully aware that he/she will be training and that there are inherent risks of no fault accidents for which I will not hold MAPOLA FC SOCCER CAMP or its COACHES RESPONSIBLE.")]
        public bool WaiverForm { get; set; }

        [Required]
        [Display(Name = "I accept that MAPOLA FC / RANGERS FC can use my child image for promotion purposes or post on the Mapola FC / Rangers FC website, social media any pictures on which I / my child is present.")]
        public bool MapolaFCRangersFCCanUseChildImage { get; set; }

        [Required]
        [Display(Name = "By clicking this box I the parents / legal guardian affirm the participation of my/our child")]
        public bool ParentLegalGuardianAffirmParticipationOfChild { get; set; }

        [Required]
        [Display(Name = "By clicking this I am aware that a $50 fee will be applied to any request of cancellation of camp application with in the 4 days of submitting the application, after the 5th days there will be no refund for any cancellation.")]
        public bool CancellationPolicy { get; set; }

        [Required]
        [Display(Name = "I am aware that by submitting application my child camp spot will not be reserve unless full payment is made with in 24 hours of my application been submitted.")]
        public bool FullPaymentAgreementWithin24HrsOfApplication { get; set; }

        [Required]
        [Display(Name = "Type of payment accepted: EMAIL TRANSFER, CASH, CHECKS. ALL PAYMENT SHOULD BE MADE TO MARCUS LAQUIE at mapolafc@gmail.com / 647-391-0429")]
        public bool TypesOfPaymentAccepted { get; set; }

    }
}
