using System;
using System.Collections.Generic;

namespace MYASummerEnrollment.Models
{
    public partial class ApplicantCopy
    {
        public int ApplicantId { get; set; }
        public string EnrollmentType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterAccount { get; set; }
        public string Dob { get; set; }
        public string Ssn { get; set; }
        public string CurrentAge { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string ParentNames { get; set; }
        public string ParentEmail { get; set; }
        public string ParentHomePhone { get; set; }
        public string ParentCellPhone { get; set; }
        public string School { get; set; }
        public string CurrentGrade { get; set; }
        public string Gpa { get; set; }
        public string GraduateYesNo { get; set; }
        public string SpecialAccomodations { get; set; }
        public string SpecialAccomodationsExplain { get; set; }
        public string MyaenrollmentPrev { get; set; }
        public string MyaenrollmentPrevExp { get; set; }
        public string Myaenrollment2Year { get; set; }
        public string Ecpiseries { get; set; }
        public string PreviousCompanyA { get; set; }
        public string JobTitleA { get; set; }
        public string LengthOfServiceA { get; set; }
        public string ResponsibilitiesA { get; set; }
        public string PreviousCompanyB { get; set; }
        public string JobTitleB { get; set; }
        public string ResponsibilitiesB { get; set; }
        public string LengthOfServiceB { get; set; }
        public string LifeStageChoices { get; set; }
        public string Citchoices { get; set; }
        public string SummerWorkExpChoices { get; set; }
        public string TrainingChoices { get; set; }
        public string CoomunityRelatedChoices { get; set; }
        public string AboutMyaexperience { get; set; }
        public string ApplicantSignature { get; set; }
        public DateTime? ApplicantSignatureDate { get; set; }
        public string VerifiedResidency { get; set; }
        public string Accepted { get; set; }
        public string StaffIntl { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string RichmondTechnicalCenter { get; set; }
        public string RichmondTechnicalCenterExplain { get; set; }
        public string BlueSkyFundProgram { get; set; }
        public string YearSubmitted { get; set; }
    }
}
