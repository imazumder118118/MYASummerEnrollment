using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MYASummerEnrollment.Models;
using Microsoft.Extensions.Options;

namespace MYASummerEnrollment.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IOptions<ApplicationConfig> _config;
        private readonly MYAEmploymentContext _db;
        //private readonly FormCollection _fm;
        //private ApplicantRepository _applicantRepo;

        public HomeController(MYAEmploymentContext db, IOptions<ApplicationConfig> config)
        {
            _db = db;
            _config = config;
            
            //_applicantRepo = apRepo;
            //_logger = logger;
        }

        public IActionResult Index()
        {

            DateTime today = DateTime.Today; // As DateTime
            string s_today = today.ToString("MM/dd/yyyy"); // As String
            TempData["applicantSignatureDate"] = s_today;
           // throw new Exception("Error in details view ");
            return View();
            
        }
        //[AcceptVerbsAttribute(HttpVerbs.Post)]
        [HttpPost()]

        public IActionResult Create (Applicant applicants)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    string fullName = Request.Form["FullName"].ToString();
                    string businessName = Request.Form["BusinessName"].ToString();

                    string lastName = Request.Form["LastName"].ToString();
                    string firstName = Request.Form["FirstName"].ToString();
                    string SSN = Request.Form["SSN"].ToString();
                    DateTime dateValue;
                    string db = Request.Form["DOB"].ToString();
                    if (DateTime.TryParse(db, out dateValue) == true)
                    {
                        DateTime dateOfBirth = DateTime.Parse(Request.Form["DOB"]);

                        string age = Age(dateOfBirth);
                        int intAge = int.Parse(age);

                        if ((intAge < 13) || (intAge > 19))
                        {
                        
                            return RedirectToAction("AgeAlertMessage");
                        }

                        switch (applicants.EnrollmentType.ToUpper())
                        {
                            //case "LIFE STAGE":
                            //    if (intAge < 14 || intAge > 15)
                            //    {
                            //        return RedirectToAction("AgeAlertMessage");
                            //    }
                            case "TEEN WORKFORCE":
                                if (intAge < 14 || intAge > 15)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                break;
                            case "VIRTUAL EARN & LEARN":
                                if (intAge < 16 || intAge > 19)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                if (AdditionalValidation(applicants) == false)
                                {   //Reusing the existing page for the alert with the old existing page 
                                    return RedirectToAction("ArtsInstituteAlertMessage");

                                }
                                break;
                            case "COUNSELORS IN TRAINING":
                                if (intAge < 14 || intAge > 15)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                if (AdditionalValidation(applicants) == false)
                                {
                                    return RedirectToAction("CITAlertMessage");
                                }
                                break;
                            case "COMMUNITY HEALTH WORKER":
                                if (intAge < 17 || intAge > 19)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                if (AdditionalValidation(applicants) == false)
                                {
                                    return RedirectToAction("SWEAlertMessage");
                                }
                                //if (applicants.CITChoices == null || applicants.CITChoices.Trim().Length == 0)
                                //{
                                //    return RedirectToAction("SWEAlertMessage");
                                //}
                                //if (applicants.SummerWorkExpChoices == null || applicants.SummerWorkExpChoices.Trim().Length == 0)
                                //{
                                //    return RedirectToAction("SWEAlertMessage");
                                //}
                                //if (applicants.LifeStageChoices == null || applicants.LifeStageChoices.Trim().Length == 0)
                                //{
                                //    return RedirectToAction("SWEAlertMessage");
                                //}
                                //if (applicants.BlueSkyFundProgram == null || applicants.BlueSkyFundProgram.Trim().Length == 0)
                                //{
                                //    return RedirectToAction("SWEAlertMessage");
                                //}
                                break;
                            case "KINGS DOMINION":
                                if (intAge < 16 || intAge > 19)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                break;


                            case "MYA JUNIOR":
                                if (intAge < 13 || intAge > 13)
                                {
                                    return RedirectToAction("AgeAlertMessage");
                                }
                                break;

                            default:
                                break;
                        }

                        //Do the capcha check 
                        if (fullName == "1CityOurCity1" && businessName.Length == 0)
                        {
                            //MYAEmploymentEntities _dbMYAEmp = new MYAEmploymentEntities();
                            applicants.SubmitDate = DateTime.Now;
                            applicants.ApplicantSignatureDate = DateTime.Now;
                            applicants.CurrentAge = age;

                            ApplicantRepository applRep = new ApplicantRepository(_db);
                            var ApplicantList = applRep.GetAllApplicants();
                            
                           
                            bool checklist = ApplicantList.Any(x => x.LastName == lastName && x.FirstName.ToUpper() == firstName && x.Ssn == SSN);
                            //var CheckIfApplicantExists = _db.Applicant.Any(x => x.LastName == lastName && x.FirstName.ToUpper() == firstName && x.Ssn == SSN);
                            if(checklist)
                            //if (CheckIfApplicantExists)
                            // if (getApplicant.Any(a => a.LastName.ToUpper() == lastName && a.FirstName.ToUpper() == firstName && a.SSN == SSN))
                            {
                                //ModelState.AddModelError("Code", "Applicant Already Exists");
                                return RedirectToAction("RecordExistAlertMessage");
                            }
                            {
                                //applicants.CITChoices= forms["FullName"]
                                applicants.YearSubmitted = DateTime.Now.Year.ToString();
                                //its a required field in db , trying to avoid db changes.So hardcode ApplicationSignature field
                                if (String.IsNullOrEmpty(applicants.HomePhone))
                                {
                                    applicants.HomePhone = "(000)000-0000";
                                }
                                applicants.ApplicantSignature = "NA";
                                _db.Applicant.Add(applicants);
                                _db.SaveChanges();
                                this.sEmail(applicants);
                                this.sendConfEmailApplicants(applicants);
                                this.sendParentEmailApplicants(applicants);
                            }
                        }
                        else
                        {
                            //Bot email
                            StringBuilder strBodyBot = new StringBuilder();
                            strBodyBot.Append("The application form sent on <strong>'" + DateTime.Now + "'</strong> may have been submitted by a Bot!! <p>");
                            strBodyBot.Append("The fields 'BusinessName' and 'Fullname' were hidden and should have specific values. If those values have been edited then its possible a bot added them.<p>");
                            strBodyBot.Append("The values that are currently the hidden fields are:BusinessName- <strong>'" + businessName + "'</strong> and FullName - <strong>'" + fullName + "'</strong><p>");
                            strBodyBot.Append("The values that are suppose to be in the hidden fields are:BusinessName - <strong>''</strong> and FullName - <strong>'1CityOurCity'</strong><p>");
                            strBodyBot.Append("Please hold on to this email for reference purposes. if this continues please call the DIT helpdesk and let them know.");
                            strBodyBot.Append("<br /><br /><br /><br />");
                            strBodyBot.Append("<strong>For DIT Use Only:</strong><br />");
                            //strBodyBot.Append("<strong>Client IP Address:</strong> " + Request.UserHostAddress + "<br />");
                            //strBodyBot.Append("<strong>Client Operating System: </strong> " + Request.Browser.Platform.ToString() + "<br />");
                            //strBodyBot.Append("<strong>Client Browser Name:</strong> " + Request.Browser.Browser.ToString() + "<br />");
                            //strBodyBot.Append("<strong>Client Browser Version:</strong> " + Request.Browser.Version.ToString() + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("Please enter the collected information manually again if it is not bot!.");
                            strBodyBot.Append("<strong>I am applying for: </strong> " + applicants.EnrollmentType + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>PERSONAL INFORMATION</strong>" + "<br />");
                            strBodyBot.Append("<strong>Name: </strong> " + applicants.FirstName + " " + applicants.MiddleName + " " + applicants.LastName + "<br />");
                            strBodyBot.Append("<strong>Address: </strong> " + applicants.Address + " " + applicants.Apt + " " + applicants.City + " " + applicants.State + " " + applicants.Zip + "<br/>");
                            strBodyBot.Append("<strong>Home Phone: </strong> " + applicants.HomePhone + "<br/>");
                            strBodyBot.Append("<strong>Cell Phone: </strong> " + applicants.CellPhone + "<br/>");
                            strBodyBot.Append("<strong>Email Address: </strong> " + applicants.EmailAddress + "<br/>");
                            strBodyBot.Append("<strong>Date of Birth: </strong> " + applicants.Dob + "<br/>");
                            strBodyBot.Append("<strong>Current Age: </strong> " + applicants.CurrentAge + "<br/>");
                            strBodyBot.Append("<strong>SSN (Last 4 digits): </strong>" + applicants.Ssn + "<br />");
                            strBodyBot.Append("<strong>Gender: </strong>" + applicants.Gender + "<br />");
                            strBodyBot.Append("<strong>Race: </strong>" + applicants.Race + "<br />");
                            strBodyBot.Append("<strong>Parent/Guardian Names: </strong>" + applicants.ParentNames + "<br />");
                            strBodyBot.Append("<strong>Parent/Guardian Email: </strong>" + applicants.ParentEmail + "<br />");
                            strBodyBot.Append("<strong>Parent/Guardian Phone: </strong>" + applicants.ParentHomePhone + "<br />");
                            strBodyBot.Append("<strong>Parent/Guardian Cell: </strong>" + applicants.ParentCellPhone + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            /////////////////////Changes Made in the email format//////////////////////////////////
                            strBodyBot.Append("<strong>EDUCATION</strong>" + "<br />");
                            strBodyBot.Append("<strong>School: </strong>" + applicants.School + "<br />");
                            strBodyBot.Append("<strong>Current Grade: </strong>" + applicants.CurrentGrade + "<br />");
                            strBodyBot.Append("<strong>GPA: </strong>" + applicants.Gpa + "<br />");
                            //strBodyBot.Append("<strong>If out of school did you graduate? </strong>" + applicants.GraduateYesNo + "<br />");
                            strBodyBot.Append("<strong>Will you require any special accommodations to perform placement duties? </strong>" + applicants.SpecialAccomodations + "<br />");
                            strBodyBot.Append("<strong>If yes, please describe: </strong>" + applicants.SpecialAccomodationsExplain + "<br />");
                            strBodyBot.Append("<strong>Were you enrolled in 2020 MYA's programming? </strong>" + applicants.MyaenrollmentPrev + "<br />");
                            strBodyBot.Append("<strong>If yes, which program? </strong>" + applicants.MyaenrollmentPrevExp + "<br />");
                            strBodyBot.Append("<strong>Did you participate in any City of Richmond Youth Programs in 2020? </strong>" + applicants.Myaenrollment2Year + "<br />");
                            strBodyBot.Append("<strong>If yes, type the one(s) you have participated in: </strong>" + applicants.TwitterAccount + "<br />");
                            strBodyBot.Append("<strong>Are you a student at the Richmond Technical Center? </strong>" + applicants.RichmondTechnicalCenter + "<br />");
                            strBodyBot.Append("<strong>If yes, what are you studying? </strong>" + applicants.RichmondTechnicalCenterExplain + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>PREVIOUS EXPERIENCE #1</strong>" + "<br />");
                            strBodyBot.Append("<strong>Company: </strong>" + applicants.PreviousCompanyA + "<br />");
                            strBodyBot.Append("<strong>Job Title: </strong>" + applicants.JobTitleA + "<br />");
                            strBodyBot.Append("<strong>Responsibilities: </strong>" + applicants.ResponsibilitiesA + "<br />");
                            strBodyBot.Append("<strong>Length of Service: </strong>" + applicants.LengthOfServiceA + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>PREVIOUS EXPERIENCE #2</strong>" + "<br />");
                            strBodyBot.Append("<strong>Company: </strong>" + applicants.PreviousCompanyB + "<br />");
                            strBodyBot.Append("<strong>Job Title: </strong>" + applicants.JobTitleB + "<br />");
                            strBodyBot.Append("<strong>Responsibilities: </strong>" + applicants.ResponsibilitiesB + "<br />");
                            strBodyBot.Append("<strong>Length of Service: </strong>" + applicants.LengthOfServiceB + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>SELECT YOUR AREAS OF INTEREST AND EXPERIENCE</strong>" + "<br />");
                            //strBodyBot.Append("<strong>Please indicate any training, certifications you have</strong>" + "<br />");
                            //strBodyBot.Append("<strong>Type the training, certifications you have received: </strong>" + applicants.TrainingChoices + "<br />");
                            strBodyBot.Append("<strong>Please indicate any school/community related activities you’ve participated in, in the last 3 years </strong>" + "<br />");
                            strBodyBot.Append("<strong>Type the school/community related activities you have participated in: </strong>" + applicants.CoomunityRelatedChoices + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>COUNSELORS IN TRAINING -COMMUNITY HEALTH WORKER AND VIRTUAL ENARN & LEARN EXPERIENCE APPLICANTS ONLY</strong>" + "<br />");
                            strBodyBot.Append("<strong>Do you prefer a part time or full time summer work experience placement? </strong>" + "<br />");
                            strBodyBot.Append("<strong>Yes/No: </strong>" + applicants.CITchoices + "<br />");
                            strBodyBot.Append("<strong>Select your top three (3) areas of interest for career exploration: </strong>" + "<br />");
                            strBodyBot.Append("<strong>Choice #1: </strong>" + applicants.SummerWorkExpChoices + "<br />");
                            strBodyBot.Append("<strong>Choice #2: </strong>" + applicants.LifeStageChoices + "<br />");
                            strBodyBot.Append("<strong>Choice #3: </strong>" + applicants.BlueSkyFundProgram + "<br />");
                            strBodyBot.AppendLine("<br/>");
                            strBodyBot.Append("<strong>ALL APPLICANTS</strong>" + "<br />");
                            strBodyBot.Append("<strong>Please provide a brief response to the appropriate question below. </strong>" + "<br />");
                            //strBodyBot.Append("<strong>* Counselor In Training applicants (500): </strong>" + "<br />");
                            // strBodyBot.Append("<strong>Why do you want to participate in the MYA Counselor in Training program? </strong>" + "<br />");
                            strBodyBot.Append("<strong>Briefly describe your career goals and how participating in the Mayor’s Youth Academy’s summer work experience program will help you meet these goals. </strong>" + "<br />");
                            //strBodyBot.Append("<strong>* Summer Work Experience applicants (250): </strong>" + "<br />");
                            //strBodyBot.Append("<strong>Why do you want to participate in the MYA Summer Work Experience program? </strong>" + "<br />");
                            //strBodyBot.Append("<strong>* Kings Dominion applicants (250): </strong>" + "<br />");
                            //strBodyBot.Append("<strong>What do you hope to gain by participating in the MYA Pre-Employment Trainings and your Kings Dominion Summer Employment? </strong>" + "<br />");
                            strBodyBot.Append("<strong>I am applying for: </strong>" + applicants.EnrollmentType + "<br />");
                            strBodyBot.Append("<strong>MY TYPED RESPONSE (250 characters MAX): </strong>" + "<br />");
                            strBodyBot.Append(applicants.AboutMyaexperience + "<br />");
                            //strBodyBot.Append("<strong>Acknowledgement of Application Process, Disclaimer, and Parental Consent </strong>");
                            strBodyBot.AppendLine("<br/>");
                            //strBodyBot.AppendLine("<strong>Applicant Signature: </strong>" + applicants.ApplicantSignature + "<br />");
                            //strBodyBot.Append("<strong>Parent Signature: </strong>" + applicants.ParentNames + "<br />");
                            //strBodyBot.AppendLine("<strong>Date: </strong>" + applicants.ApplicantSignatureDate + "<br />");


                            this.SendEmail(_config.Value.SummerWorkEmail, string.Format("{0} {1}", "Possible BOT MYA Enrollment - ", applicants.EnrollmentType.ToUpper()), strBodyBot.ToString());

                        }
                        //return View("Index");
                    }
                    else
                    {
                        
                        ModelState.AddModelError(string.Empty, "Date of Birth is not correct.");
                        return View("Index");
                    }
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                    return View("Index");
                }
            }

            catch (Exception exp)
            {
                StringBuilder sb = new StringBuilder();
                ErrorViewModel errormodel = new ErrorViewModel
                {
                    ErrorSource = exp.Source.ToString(),
                    ExceptionMessage = exp.Message,
                    StackTrace = exp.StackTrace
                };
                ErrorController err = new ErrorController(_config) { ControllerContext = ControllerContext };
                err.EmailException(errormodel, sb);
                return View("Error");
            }
            //end of outer catch
            // Use Post/Redirect/Get pattern
            switch (applicants.EnrollmentType.ToUpper())
            {
                case "TEEN WORKFORCE":
                    return RedirectToAction("ConfirmLifeStage");

                case "COUNSELORS IN TRAINING":
                    return RedirectToAction("ConfirmCIT");

                case "COMMUNITY HEALTH WORKER":
                    return RedirectToAction("ConfirmSummerWork");

                case "VIRTUAL EARN & LEARN":
                    return RedirectToAction("ConfirmVirtualEarnLearn");

                case "KINGS DOMINION":
                    return RedirectToAction("ConfirmKingsDominion");

                default:
                    return RedirectToAction("Confirm");
            }
        }
        

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AgeAlertMessage()
        {
            return View("AgeAlertMessage");
        }
        public ActionResult CITAlertMessage()
        {
            return View("CITAlertMessage");
        }
        public ActionResult ArtsInstituteAlertMessage()
        {
            return View("ArtsInstituteAlertMessage");
        }


        public ActionResult SWEAlertMessage()
        {
            return View("SWEAlertMessage");
        }

        public ActionResult RecordExistAlertMessage()
        {
            return View("RecordExistAlertMessage");
        }

        public ActionResult ConfirmLifeStage()//USED FOR TEEN WORKFORCE
        {
            return View("ThankYouLifeStage");
        }

        public ActionResult ConfirmCIT()
        {
            return View("ThankYouCIT");
        }

        public ActionResult ConfirmSummerWork()
        {
            return View("ThankYouSummerWork");// Replaced with COMMUNITY HEALTH WORKER
        }

        public ActionResult ConfirmVirtualEarnLearn()
        {
            return View("ThankYouMYAJunior");// Replaced with Virtual Learn and Earn
        }

        public ActionResult ConfirmKingsDominion()
        {
            return View("ThankYouKingsDominion");
        }

        public ActionResult Confirm()
        {
            return View("ThankYouMessage");
        }

        public static string Age(DateTime birthday)
        {
            DateTime newToday = DateTime.Today;
            DateTime today = new DateTime(newToday.Year, 07, 1);
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age.ToString();
        }
        public bool AdditionalValidation(Applicant applicants)
        {
            if (applicants.CITchoices == null || applicants.CITchoices.Trim().Length == 0)
            {
                return false;
            }
            if (applicants.SummerWorkExpChoices == null || applicants.SummerWorkExpChoices.Trim().Length == 0)
            {
                return false;
            }
            if (applicants.LifeStageChoices == null || applicants.LifeStageChoices.Trim().Length == 0)
            {
                return false;
            }
            if (applicants.BlueSkyFundProgram == null || applicants.BlueSkyFundProgram.Trim().Length == 0)
            {
                return false;
            }
            else
                return true;

        }

        public void sEmail(Applicant app)
        {

            StringBuilder strBody = new StringBuilder();

            try
            {
                strBody.Append("<strong>I am applying for: </strong> " + app.EnrollmentType + "<br />");
                strBody.Append("<strong>Will you have access to a reliable phone, computer, or tablet and internet?: </strong> " + app.Ecpiseries + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>APPLICANT ID: </strong> " + app.ApplicantId + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>PERSONAL INFORMATION</strong>" + "<br />");
                strBody.Append("<strong>Name: </strong> " + app.FirstName + " " + app.MiddleName + " " + app.LastName + "<br />");
                strBody.Append("<strong>Address: </strong> " + app.Address + " " + app.Apt + " " + app.City + " " + app.State + " " + app.Zip + "<br/>");
                strBody.Append("<strong>Home Phone: </strong> " + app.HomePhone + "<br/>");
                strBody.Append("<strong>Cell Phone: </strong> " + app.CellPhone + "<br/>");
                strBody.Append("<strong>Email Address: </strong> " + app.EmailAddress + "<br/>");
                strBody.Append("<strong>Date of Birth: </strong> " + app.Dob + "<br/>");
                strBody.Append("<strong>Current Age: </strong> " + app.CurrentAge + "<br/>");
                strBody.Append("<strong>SSN (Last 4 digits): </strong>" + app.Ssn + "<br />");
                strBody.Append("<strong>Gender: </strong>" + app.Gender + "<br />");
                strBody.Append("<strong>Race: </strong>" + app.Race + "<br />");
                strBody.Append("<strong>Parent/Guardian Names: </strong>" + app.ParentNames + "<br />");
                strBody.Append("<strong>Parent/Guardian Email: </strong>" + app.ParentEmail + "<br />");
                strBody.Append("<strong>Parent/Guardian Phone: </strong>" + app.ParentHomePhone + "<br />");
                strBody.Append("<strong>Parent/Guardian Cell: </strong>" + app.ParentCellPhone + "<br />");
                strBody.AppendLine("<br/>");
                /////////////////////Changes Made in the email format//////////////////////////////////
                strBody.Append("<strong>EDUCATION</strong>" + "<br />");
                strBody.Append("<strong>School: </strong>" + app.School + "<br />");
                strBody.Append("<strong>Current Grade: </strong>" + app.CurrentGrade + "<br />");
                strBody.Append("<strong>GPA: </strong>" + app.Gpa + "<br />");
                strBody.Append("<strong>If out of school did you graduate? </strong>" + app.GraduateYesNo + "<br />");
                strBody.Append("<strong>Will you require any special accommodations to perform placement duties? </strong>" + app.SpecialAccomodations + "<br />");
                strBody.Append("<strong>If yes, please describe: </strong>" + app.SpecialAccomodationsExplain + "<br />");
                strBody.Append("<strong>Were you enrolled in 2017 MYA's programming? </strong>" + app.MyaenrollmentPrev + "<br />");
                strBody.Append("<strong>If yes, which program? </strong>" + app.MyaenrollmentPrevExp + "<br />");
                strBody.Append("<strong>Did you participate in any City of Richmond Youth Programs in 2017? </strong>" + app.Myaenrollment2Year + "<br />");
                strBody.Append("<strong>If yes, type the one(s) you have participated in: </strong>" + app.TwitterAccount + "<br />");
                strBody.Append("<strong>Are you a student at the Richmond Technical Center? </strong>" + app.RichmondTechnicalCenter + "<br />");
                strBody.Append("<strong>If yes, what are you studying? </strong>" + app.RichmondTechnicalCenterExplain + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>PREVIOUS EXPERIENCE #1</strong>" + "<br />");
                strBody.Append("<strong>Company: </strong>" + app.PreviousCompanyA + "<br />");
                strBody.Append("<strong>Job Title: </strong>" + app.JobTitleA + "<br />");
                strBody.Append("<strong>Responsibilities: </strong>" + app.ResponsibilitiesA + "<br />");
                strBody.Append("<strong>Length of Service: </strong>" + app.LengthOfServiceA + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>PREVIOUS EXPERIENCE #2</strong>" + "<br />");
                strBody.Append("<strong>Company: </strong>" + app.PreviousCompanyB + "<br />");
                strBody.Append("<strong>Job Title: </strong>" + app.JobTitleB + "<br />");
                strBody.Append("<strong>Responsibilities: </strong>" + app.ResponsibilitiesB + "<br />");
                strBody.Append("<strong>Length of Service: </strong>" + app.LengthOfServiceB + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>SELECT YOUR AREAS OF INTEREST AND EXPERIENCE</strong>" + "<br />");
                //strBody.Append("<strong>Please indicate any training, certifications you have</strong>" + "<br />");
                //strBody.Append("<strong>Type the training, certifications you have received: </strong>" + app.TrainingChoices + "<br />");
                strBody.Append("<strong>Please indicate any school/community related activities you’ve participated in, in the last 3 years </strong>" + "<br />");
                strBody.Append("<strong>Type the school/community related activities you have participated in: </strong>" + app.CoomunityRelatedChoices + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>COUNSELORS IN TRAINING -COMMUNITY HEALTH WORKER AND VIRTUAL ENARN & LEARN EXPERIENCE APPLICANTS ONLY</strong>" + "<br />");
                strBody.Append("<strong>Do you prefer a part time or full time summer work experience placement? </strong>" + "<br />");
                strBody.Append("<strong>PT=20/FT=40: </strong>" + app.CITchoices + "<br />");
                strBody.Append("<strong>Select your top three (3) areas of interest for career exploration: </strong>" + "<br />");
                strBody.Append("<strong>Choice #1: </strong>" + app.SummerWorkExpChoices + "<br />");
                strBody.Append("<strong>Choice #2: </strong>" + app.LifeStageChoices + "<br />");
                strBody.Append("<strong>Choice #3: </strong>" + app.BlueSkyFundProgram + "<br />");
                strBody.AppendLine("<br/>");
                strBody.Append("<strong>ALL APPLICANTS</strong>" + "<br />");
                strBody.Append("<strong>Please provide a brief response to the appropriate question below. </strong>" + "<br />");
                //strBody.Append("<strong>* Counselor In Training applicants (500): </strong>" + "<br />");
                //strBody.Append("<strong>Why do you want to participate in the MYA Counselor in Training program? </strong>" + "<br />");
                //strBody.Append("<strong>* Summer Work Experience applicants (250): </strong>" + "<br />");
                //strBody.Append("<strong>Why do you want to participate in the MYA Summer Work Experience program? </strong>" + "<br />");
                //strBody.Append("<strong>* Kings Dominion applicants (250): </strong>" + "<br />");
                strBody.Append("<strong>Briefly describe your career goals and how participating in the Mayor’s Youth Academy’s summer work experience program will help you meet these goals? </strong>" + "<br />");
                strBody.Append("<strong>I am applying for: </strong>" + app.EnrollmentType + "<br />");
                strBody.Append("<strong>MY TYPED RESPONSE (250 characters MAX): </strong>" + "<br />");
                strBody.Append(app.AboutMyaexperience + "<br />");
                //strBody.Append("<strong>Acknowledgement of Application Process, Disclaimer, and Parental Consent </strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<strong>Applicant Signature: </strong>" + app.ApplicantSignature + "<br />");
                //strBody.Append("<strong>Parent Signature: </strong>" + app.ParentNames + "<br />");
                //strBody.AppendLine("<strong>Date: </strong>" + app.ApplicantSignatureDate + "<br />");

                /////////////////////////////////////////////////////////
                //strBody.Append("<strong>EDUCATION</strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>School: </strong>" + app.School + "<br />");
                //strBody.Append("<strong>Current Grade: </strong>" + app.CurrentGrade + "<br />");
                //strBody.Append("<strong>GPA : </strong>" + app.GPA + "<br />");
                //strBody.Append("<strong>If out of school did you graduate? </strong>" + app.GraduateYesNo + "<br />");
                //strBody.Append("<strong>Will you require any special accommodations to perform placement duties? </strong>" + app.SpecialAccomodations + "<br />");
                //strBody.Append("<strong>If yes, please describe : </strong>" + app.SpecialAccomodationsExplain + "<br />");
                //strBody.Append("<strong>Were you enrolled in 2013 MYA's program? </strong>" + app.MYAEnrollmentPrev + "<br />");
                //strBody.Append("<strong>MYA 2013 Placement: </strong>" + app.MYAEnrollmentPrevExp + "<br />");
                //strBody.Append("<strong>Did you participate in any City of Richmond Youth Programs in 2013? </strong>" + app.MYAEnrollment2Year + "<br />");
                //strBody.Append("<strong>Are you a student at the Richmond Technical Center? </strong>" + app.RichmondTechnicalCenter + "<br />");
                //strBody.Append("<strong>If yes, what are you studying? </strong>" + app.RichmondTechnicalCenterExplain + "<br />");
                //strBody.Append("<strong>If you are a Junior or Senior, are you applying for the ECPI Technology Series: </strong>" + app.ECPISeries + "<br />");
                //strBody.Append("<strong>Have you participated in the Blue Sky Fund Program? </strong>" + app.ECPISeries + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>PREVIOUS EXPERIENCE A</strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>Company: </strong>" + app.PreviousCompanyA + "<br />");
                //strBody.Append("<strong>Job Title: </strong>" + app.JobTitleA + "<br />");
                //strBody.Append("<strong>Responsibilities: </strong>" + app.ResponsibilitiesA + "<br />");
                //strBody.Append("<strong>Length of Service: </strong>" + app.LengthOfServiceA + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>PREVIOUS EXPERIENCE B</strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>Company: </strong>" + app.PreviousCompanyB + "<br />");
                //strBody.Append("<strong>Job Title: </strong>" + app.JobTitleB + "<br />");
                //strBody.Append("<strong>Responsibilities: </strong>" + app.ResponsibilitiesB + "<br />");
                //strBody.Append("<strong>Length of Service: </strong>" + app.LengthOfServiceB + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>SELECT YOUR AREAS OF INTEREST FOR PROGRAM APPLYING TO</strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>Type your choices: </strong>" + app.SummerWorkExpChoices + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>PLEASE INDICATE ANY TRAINING </strong>" + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>Type the training(s) you had: </strong>" + app.TrainingChoices + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>SCHOOL / COMMUNITY Related Activities </strong>" + "<br />");
                //strBody.Append("<strong>Type the activities you had: </strong>" + app.CoomunityRelatedChoices + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>Why do you want to participate in the MYA program? <br /> Please describe what you hope to gain from your experience in MYA: </strong>" + app.AboutMYAExperience + "<br />");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.Append("<strong>ACKNOWLEDGEMENT OF APPLICATION PROCESS AND PARENTAL CONSENT</strong>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<br/>");
                //strBody.AppendLine("<strong>Applicant Signature: </strong>" + app.ApplicantSignature + "<br />");
                //strBody.AppendLine("<strong>Date: </strong>" + app.ApplicantSignatureDate + "<br />");
                switch (app.EnrollmentType.ToUpper())
                {
                    case "TEEN WORKFORCE":
                        this.SendEmail(_config.Value.SummerWorkEmail, string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        //this.SendEmail(ConfigurationManager.AppSettings["toEmail"].ToString(), string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        break;
                    case "COUNSELORS IN TRAINING":
                        this.SendEmail(_config.Value.CITEmail, string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        //this.SendEmail(ConfigurationManager.AppSettings["toEmail"].ToString(), string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        break;
                    case "VIRTUAL EARN & LEARN":
                        this.SendEmail(_config.Value.SummerWorkEmail, string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        break;
                    case "COMMUNITY HEALTH WORKER":
                        this.SendEmail(_config.Value.SummerWorkEmail, string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        break;
                    case "KINGS DOMINION":
                        this.SendEmail(_config.Value.SummerWorkEmail, string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        //this.SendEmail(ConfigurationManager.AppSettings["toEmail"].ToString(), string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
                        break;
                }
                //this.SendEmail("madhavi.uppala@richmondgov.com", string.Format("{0} {1}", "MYA Enrollment - ", app.EnrollmentType.ToUpper()), strBody.ToString());
            }
            catch (Exception exp)
            {
                StringBuilder sb = new StringBuilder();
                ErrorViewModel errormodel = new ErrorViewModel
                {
                    ErrorSource = exp.Source.ToString(),
                    ExceptionMessage = exp.Message,
                    StackTrace = exp.StackTrace
                };
                ErrorController err = new ErrorController(_config) { ControllerContext = ControllerContext };
                err.EmailException(errormodel, sb);
                //return View("Error");
            }

        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient(_config.Value.SMTPExchangeEmailServer, 25);
            smtpClient.Credentials = new System.Net.NetworkCredential("", "");
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            

            MailMessage email = new MailMessage(_config.Value.fromEmail, toEmail, subject, body);
            email.IsBodyHtml = true;

            smtpClient.Send(email);
            //throw new Exception("Error in Details View");

            //MYASummerEnrollmentApplication.CORemail.COREmailSoapClient email = new MYASummerEnrollmentApplication.CORemail.COREmailSoapClient();
            //email.SendEmail(toEmail, ConfigurationManager.AppSettings["fromEmail"].ToString(), subject, body, null);

        }

        public void sendConfEmailApplicants(Applicant applicant)
        {
            StringBuilder sBodyApp = new StringBuilder();
            try
            {
                switch (applicant.EnrollmentType.ToUpper())
                {
                    case "LIFE STAGE":
                        sBodyApp.Append("Thank you for submitting your application for the MYA Life Stage program. " + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("Auditions are crucial in the selection process for the MYA Life Stage program. You will be contacted via telephone or e-mail at the telephone number/e-mail listed on your application to give you additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 646-7480 or e-mail us at MYALifeStage@richmondgov.com " + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "COUNSELORS IN TRAINING":
                        sBodyApp.Append("Thank you for submitting your application for the Counselors In Training program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("You will be contacted via telephone or e-mail at the telephone number/e-mail listed on your application to give you additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or email us at MYACIT@richmondgov.com " + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "COMMUNITY HEALTH WORKER":
                        sBodyApp.Append("Thank you for submitting the application. This completes your application process." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have any questions please contact the MYA office at 804-646-7933 or mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "TEEN WORKFORCE":
                        sBodyApp.Append("Thank you for submitting the application. This completes your application process." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have any questions please contact the MYA office at 804-646-7933 or mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;


                    case "KINGS DOMINION":
                        sBodyApp.Append("Thank you for submitting the application. This completes your application process." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have any questions please contact the MYA office at 804-646-7933 or mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "VIRTUAL EARN & LEARN":
                        //sBodyApp.Append("Thank you for submitting your application for the MYA Junior program." + "<br />");
                        sBodyApp.Append("Thank you for submitting the application. This completes your application process." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("You will be contacted via telephone or email listed on your application to give you additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or e-mail us at mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.EmailAddress, _config.Value.MYAYearOfEnrollment + "MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;
                }
            }
            catch (Exception exp)
            {
                StringBuilder sb = new StringBuilder();
                ErrorViewModel errormodel = new ErrorViewModel
                {
                    ErrorSource = exp.Source.ToString(),
                    ExceptionMessage = exp.Message,
                    StackTrace = exp.StackTrace
                };
            
                ErrorController err = new ErrorController(_config) { ControllerContext = ControllerContext };
                err.EmailException(errormodel, sb);
                //return View("Error");
            }
        }

        public void sendParentEmailApplicants(Applicant applicant)
        {
            StringBuilder sBodyApp = new StringBuilder();
            try
            {
                switch (applicant.EnrollmentType.ToUpper())
                {
                    case "ARTS INSTITUTE":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA Arts Institute program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("Auditions are crucial in the selection process for the MYA Arts Institute program. The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or e-mail us at MYAARTSInstitute@richmondgov.com " + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "TEEN WORKFORCE":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA TEEN WORKFORCE program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or e-mail us at mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "COUNSELORS IN TRAINING":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA Counselors In Training program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or email us at MYACIT@richmondgov.com " + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "COMMUNITY HEALTH WORKER":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA COMMUNITY HEALTH WORKER program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have any questions please contact the MYA office at 804-646-7933 or mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "VIRTUAL EARN & LEARN":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA VIRTUAL EARN & LEARN program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or e-mail us at mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;

                    case "KINGS DOMINION":
                        sBodyApp.Append("The Mayor's Youth Academy has received an application from " + applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName + " for the MYA Kings Dominion program." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("The applicant will be contacted via e-mail at the e-mail listed on the application to give additional information or instructions." + "<br />");
                        sBodyApp.AppendLine("<br/>");
                        sBodyApp.Append("If you have questions, please contact the MYA office at 804-646-7933 or e-mail us at mayorsyouthacademy@richmondgov.com" + "<br />");
                        this.SendEmail(applicant.ParentEmail, _config.Value.MYAYearOfEnrollment + " MYA Summer Enrollment Application", sBodyApp.ToString());
                        break;
                }
            }
            catch (Exception exp)
            {
                StringBuilder sb = new StringBuilder();
                ErrorViewModel errormodel = new ErrorViewModel();
                errormodel.ErrorSource = exp.Source.ToString();
                errormodel.ExceptionMessage = exp.Message;
                errormodel.StackTrace = exp.StackTrace;
                ErrorController err = new ErrorController(_config) { ControllerContext = ControllerContext };
                err.EmailException(errormodel, sb);
                //return View("Error");
            }
        }
    }
}
