using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYASummerEnrollment.Models
{
    [MetadataType(typeof(Applicant))]
    //public partial class Applicant
    //{ }
        public  class Applicant
        {
        [Key]
        public int ApplicantId { get; set; }
        [Required(ErrorMessage = "Enrollment type required")]
        public string EnrollmentType { get; set; }
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "M.I. required")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Apt { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        [RegularExpression(@"\d{5}(-\d{4})?", ErrorMessage = "Not a valid zipcode")]
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        [Required(ErrorMessage = "Cell phone is required")]
        public string CellPhone { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Not a valid email")]
        public string EmailAddress { get; set; }
        public string TwitterAccount { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [RegularExpression("^[0-9m]{1,2}/[0-9d]{1,2}/[0-9y]{4}$", ErrorMessage = "Invalid Date format")]
        public string Dob { get; set; }
        [Required(ErrorMessage = "SSN is required")]
        public string Ssn { get; set; }
        public string CurrentAge { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        public string Race { get; set; }
        [Required(ErrorMessage = "Names are required")]
        public string ParentNames { get; set; }

        [Required(ErrorMessage = "Parent email is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Not a valid email")]
        public string ParentEmail { get; set; }
        [Required(ErrorMessage = "Home phone is required")]
        public string ParentHomePhone { get; set; }
        public string ParentCellPhone { get; set; }
        [Required(ErrorMessage = "School is required")]

        public string School { get; set; }
        [Required(ErrorMessage = "Grade is required")]
        public string CurrentGrade { get; set; }
        public string Gpa { get; set; }
        public string GraduateYesNo { get; set; }
        public string SpecialAccomodations { get; set; }
        [StringLength(50, ErrorMessage = "Special acc description length cannot be more than 50 chars")]
        public string SpecialAccomodationsExplain { get; set; }
        public string MyaenrollmentPrev { get; set; }
        public string MyaenrollmentPrevExp { get; set; }
        public string Myaenrollment2Year { get; set; }
        [Required(ErrorMessage = "Please ansnwer if you have reliable phone,computer or tablet and internet or not ?")]
        public string Ecpiseries { get; set; }
        public string PreviousCompanyA { get; set; }
        public string JobTitleA { get; set; }
        public string LengthOfServiceA { get; set; }
        [StringLength(100, ErrorMessage = "Responsibilites field length cannot be more than 100 chars")]
        public string ResponsibilitiesA { get; set; }
        [StringLength(100, ErrorMessage = "Responsibilites field length cannot be more than 100 chars")]
        public string PreviousCompanyB { get; set; }
        public string JobTitleB { get; set; }
        public string ResponsibilitiesB { get; set; }
        public string LengthOfServiceB { get; set; }
        public string LifeStageChoices { get; set; }
        public string CITchoices { get; set; }
        [StringLength(100, ErrorMessage = "Choices field length cannot be more than 100 chars")]
        public string SummerWorkExpChoices { get; set; }
        [StringLength(100, ErrorMessage = "Trainings field length cannot be more than 100 chars")]
        public string TrainingChoices { get; set; }
        [StringLength(100, ErrorMessage = "Activities field length cannot be more than 100 chars")]
        public string CoomunityRelatedChoices { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(1000, ErrorMessage = "About MYA Experience field length cannot be more than 1000 chars")]
        public string AboutMyaexperience { get; set; }
        public string ApplicantSignature { get; set; }
        public DateTime? ApplicantSignatureDate { get; set; }
        public string VerifiedResidency { get; set; }
        public string Accepted { get; set; }
        public string StaffIntl { get; set; }
        public DateTime? SubmitDate { get; set; }

        [StringLength(50, ErrorMessage = "RIC Tech Center length cannot be more than 50 chars")]
        public string RichmondTechnicalCenter { get; set; }
        public string RichmondTechnicalCenterExplain { get; set; }
        [StringLength(50, ErrorMessage = "Blue Sky Fund Prg length cannot be more than 50 chars")]
        public string BlueSkyFundProgram { get; set; }
        public string YearSubmitted { get; set; }
    }
}
