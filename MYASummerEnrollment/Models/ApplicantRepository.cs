using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYASummerEnrollment.Models;

namespace MYASummerEnrollment.Models
{
    
    public class ApplicantRepository
    {
        private readonly MYAEmploymentContext _db;
        public ApplicantRepository(MYAEmploymentContext db)
        {
            _db = db;
        }

        public IQueryable<Applicant> GetAllApplicants()
        {
            
            IQueryable<Applicant> Applicants = null;
            Applicants = from g in _db.Applicant
                            //where g.GovAllowed == true
                            select g;
            //return this.empEntity.Applicants;

            return Applicants;
        }
    }
}
