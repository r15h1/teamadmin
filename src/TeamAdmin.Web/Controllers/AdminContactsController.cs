using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using System.Xml.Serialization;
using System.IO;
using TeamAdmin.Lib.zz;
using System.Text;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/contacts")]
    public class AdminContactsController : Controller
    {
        private IClubRepository clubRepository;

        public AdminContactsController(IClubRepository clubRepository)
        {
            this.clubRepository = clubRepository;
        }

        public IActionResult Index()
        {
            var messages = clubRepository.GetMessages();
            return View(messages);
        }

        [HttpGet("{messageId}")]
        public IActionResult Details(long messageId)
        {
            var message = clubRepository.GetMessage(messageId);
            switch (message.MessageType)
            {
                case MessageType.Registration:
                    message.Body = FormatRegistration(message.Body);
                    break;

                case MessageType.SummerCamp:
                    message.Body = FormatSummerCamp(message.Body);
                    break;

                case MessageType.TryOut:
                    message.Body = FormatTryOut(message.Body);
                    break;
            }
            return View(message);
        }

        //implement these and deploy
        private string FormatTryOut(string body)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TryOutModel));
            StringReader rdr = new StringReader(body);
            var result = (TryOutModel)serializer.Deserialize(rdr);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Academy: {result.Academy}");
            builder.AppendLine($"Age Group: {result.AgeGroup}\n");
            builder.AppendLine($"-----Player's Information---------------------------------------");
            builder.AppendLine($"Player's Gender: {result.PlayerGender}");
            builder.AppendLine($"Player's Registration No.: {result.PlayerRegistrationNumber}");
            builder.AppendLine($"Player's Last Name: {result.PlayerLastName}");
            builder.AppendLine($"Player's First Name: {result.PlayerFirstName}");
            builder.AppendLine($"Address: {result.Address}");
            builder.AppendLine($"City: {result.City}");
            builder.AppendLine($"Province: {result.Province}");
            builder.AppendLine($"Primary Contact#: {result.PrimaryContactNumber}");
            builder.AppendLine($"Secondary Contact#: {result.SecondaryContactNumber}");
            builder.AppendLine($"Player's Date of Birth: {result.PlayerDateOfBirth}");
            builder.AppendLine($"Player's Primary Email: {result.PlayerEmail}\n");
            builder.AppendLine($"-----Guardian's Information---------------------------------------");
            builder.AppendLine($"Primary Guardian's Full Name: {result.PrimaryGuardianFullName}");
            builder.AppendLine($"Primary Guardian's Contact#: {result.PrimaryGuardianContactNumber}");
            builder.AppendLine($"Primary Guardian's Alt Contact#: {result.PrimaryGuardianAltContactNumber}");
            builder.AppendLine($"Primary Guardian's Email: {result.PrimaryGuardianEmail}");
            builder.AppendLine($"Secondary Guardian's Full Name: {result.SecondaryGuardianFullName}");
            builder.AppendLine($"Secondary Guardian's Contact#: {result.SecondaryGuardianContactNumber}\n");
            builder.AppendLine($"-----Emergency Contact---------------------------------------");
            builder.AppendLine($"Emergency Contact's Full Name: {result.EmergencyContactFullName}");
            builder.AppendLine($"Emergency Contact#: {result.EmergencyContactNumber}\n");
            builder.AppendLine($"-----Medical Information---------------------------------------");
            builder.AppendLine($"Doctor's Full Name: {result.DoctorFullName}");
            builder.AppendLine($"Doctor's Contact#: {result.DoctorContactNumber}");
            builder.AppendLine($"Medical Conditions: {result.MedicalConditions}");
            builder.AppendLine($"Consent to Medical Treatment of a minor: {(result.ConsentToMedicalTreatmentOfMinor? "Yes" : "No")}\n");
            builder.AppendLine($"--------------------------------------------");
            builder.AppendLine($"How did you hear about us: {result.HowDidYouHearAboutUs}");
            return builder.ToString();
        }

        private string FormatSummerCamp(string body)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SummerCamp));
            StringReader rdr = new StringReader(body);
            var result = (SummerCamp)serializer.Deserialize(rdr);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"\n-----Player's Information---------------------------------------");
            builder.AppendLine($"Player's Full Name: {result.PlayerFullName}");
            builder.AppendLine($"Player's Date of Birth: {result.DateOfBirth}");
            builder.AppendLine($"Street Address: {result.StreetAddress}");
            builder.AppendLine($"Address Line 2: {result.AddressLine2}");
            builder.AppendLine($"City: {result.City}");
            builder.AppendLine($"State: {result.State}");
            builder.AppendLine($"Postal / Zip: {result.PostalOrZipCode}");
            builder.AppendLine($"Country: {result.Country}");
            builder.AppendLine($"Player's Email: {result.Email}");
            builder.AppendLine($"Player's Phone#: {result.Phone}");
            builder.AppendLine($"T-Shirt Size: {result.TShirtSize}");
            builder.AppendLine($"Medical Conditions: {result.MedicalCondition}");
            builder.AppendLine($"Gender: {result.Gender}");
            builder.AppendLine($"Player's Grade / Name of School: {result.PlayerGradeAndSchool}");
            builder.AppendLine($"How did you hear about us: {result.HowDidYouHearAboutSummerCamp}");
            builder.AppendLine($"\n-----Terms and Conditions---------------------------------------");
            builder.AppendLine($"Waiver Agreement: {(result.WaiverForm ? "Yes" : "No")}");
            builder.AppendLine($"Use of Child Image Agreement: {(result.MapolaFCRangersFCCanUseChildImage ? "Yes" : "No")}");
            builder.AppendLine($"Affirmation of my/our Child's Participation: {(result.ParentLegalGuardianAffirmParticipationOfChild ? "Yes" : "No")}");
            builder.AppendLine($"Cancellation Policy Agreement: {(result.CancellationPolicy ? "Yes" : "No")}");
            builder.AppendLine($"Full Payment Agreement: {(result.FullPaymentAgreementWithin24HrsOfApplication ? "Yes" : "No")}");
            builder.AppendLine($"Modes of Payments: {(result.TypesOfPaymentAccepted ? "Yes" : "No")}");
           
            return builder.ToString();
        }

        private string FormatRegistration(string body)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Registration));
            StringReader rdr = new StringReader(body);
            var result = (Registration)serializer.Deserialize(rdr);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Academy: {result.Academy}");
            builder.AppendLine($"Age Group: {result.AgeGroup}\n");
            builder.AppendLine($"-----Player's Information---------------------------------------");
            builder.AppendLine($"Player's Gender: {result.PlayerGender}");
            builder.AppendLine($"Player's Last Name: {result.PlayerLastName}");
            builder.AppendLine($"Player's First Name: {result.PlayerFirstName}");
            builder.AppendLine($"Address: {result.Address}");
            builder.AppendLine($"City: {result.City}");
            builder.AppendLine($"Province: {result.Province}");
            builder.AppendLine($"PostalCode: {result.PostalCode}");
            builder.AppendLine($"Primary Contact#: {result.PrimaryContactNumber}");
            builder.AppendLine($"Player's Primary Email: {result.PlayerEmail}");
            builder.AppendLine($"Player's Date of Birth: {result.PlayerDateOfBirth}\n");
            builder.AppendLine($"-----Guardian's Information---------------------------------------");
            builder.AppendLine($"Primary Guardian's Full Name: {result.PrimaryGuardianFullName}");
            builder.AppendLine($"Primary Guardian's Contact#: {result.PrimaryGuardianContactNumber}");
            builder.AppendLine($"Primary Guardian's Alt Contact#: {result.PrimaryGuardianAltContactNumber}");
            builder.AppendLine($"Primary Guardian's Email: {result.PrimaryGuardianEmail}");
            builder.AppendLine($"Secondary Guardian's Full Name: {result.SecondaryGuardianFullName}");
            builder.AppendLine($"Secondary Guardian's Contact#: {result.SecondaryGuardianContactNumber}");
            builder.AppendLine($"Secondary Guardian's Alt Contact#: {result.SecondaryGuardianContactNumber2}\n");
            builder.AppendLine($"-----Emergency Contact---------------------------------------");
            builder.AppendLine($"Emergency Contact's Full Name: {result.EmergencyContactFullName}");
            builder.AppendLine($"Emergency Contact#: {result.EmergencyContactNumber}");
            builder.AppendLine($"Doctor's Full Name: {result.DoctorFullName}");
            builder.AppendLine($"Doctor's Contact#: {result.DoctorContactNumber}");
            builder.AppendLine($"Medical Conditions: {result.MedicalConditions}");
            builder.AppendLine($"Consent to Medical Treatment of a minor: {(result.ConsentToMedicalTreatmentOfMinor ? "Yes" : "No")}\n");
            builder.AppendLine($"-----Terms and Conditions---------------------------------------");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Liability Waiver Agreement: {(result.LiabilityWaiverAgreement ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Player Commitment Agreement: {(result.PlayerCommitmentAgreement ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Model & Image Release Agreement: {(result.ModelImageReleaseAgreement ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Harassment Policy: {(result.HarassmentPolicy ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Financial Contract Agreement: {(result.FinancialContractAgreement ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Concussion Policy: {(result.ConcussionPolicy ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Player Code of Conduct and the Parent's Code of Conduct: {(result.PlayerAndParentsCodeOfConduct ? "Yes" : "No")}");
            builder.AppendLine($"I have read and agree to the terms outlined in the MFC Competitive Uniforms Policy: {(result.CompetetiveUniformsPolicy ? "Yes" : "No")}");
            builder.AppendLine($"Summer Camp Participation Agreement: {(result.SummerCampParticipationAgreement ? "Yes" : "No")}");
            builder.AppendLine($"I will abide by the rules of the MFC, its affiliated organizations and sponsors: {(result.GeneralTermsAgreement ? "Yes" : "No")}\n");
            builder.AppendLine($"---------------------------------------------------------------");
            builder.AppendLine($"How did you hear about us: {result.HowDidYouHearAboutUs}");
            return builder.ToString();
        }
    }
}