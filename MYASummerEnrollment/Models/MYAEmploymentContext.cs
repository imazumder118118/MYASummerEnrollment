using System;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata;

namespace MYASummerEnrollment.Models
{
    public partial class MYAEmploymentContext : DbContext
    {
        public MYAEmploymentContext()
        {
        }

        public MYAEmploymentContext(DbContextOptions<MYAEmploymentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicant { get; set; }
        //public virtual DbSet<ApplicantModel> ApplicantModel { get; set; }
        public virtual DbSet<ApplicantBkup20150225> ApplicantBkup20150225 { get; set; }
        public virtual DbSet<ApplicantCopy> ApplicantCopy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data source=DIT-SQL1603-Sv;initial catalog=MYAEmployment;persist security info=True;user id=MYAOnlineAppld;password=my@onlinest@g3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");

                entity.Property(e => e.AboutMyaexperience)
                    .HasColumnName("AboutMYAExperience")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Accepted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantSignature)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantSignatureDate).HasColumnType("datetime");

                entity.Property(e => e.Apt)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BlueSkyFundProgram)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CITchoices)
                    .HasColumnName("CITChoices")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoomunityRelatedChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAge)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentGrade)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Ecpiseries)
                    .HasColumnName("ECPISeries")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnrollmentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Gpa)
                    .HasColumnName("GPA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GraduateYesNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LifeStageChoices)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Myaenrollment2Year)
                    .HasColumnName("MYAEnrollment2Year")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrev)
                    .HasColumnName("MYAEnrollmentPrev")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrevExp)
                    .HasColumnName("MYAEnrollmentPrevExp")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentHomePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentNames)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenter)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenterExplain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodations)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodationsExplain)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StaffIntl)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.SummerWorkExpChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterAccount)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedResidency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.YearSubmitted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApplicantBkup20150225>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Applicant_Bkup_2015_02_25");

                entity.Property(e => e.AboutMyaexperience)
                    .HasColumnName("AboutMYAExperience")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Accepted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantId)
                    .HasColumnName("ApplicantID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ApplicantSignature)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantSignatureDate).HasColumnType("datetime");

                entity.Property(e => e.Apt)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BlueSkyFundProgram)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Citchoices)
                    .HasColumnName("CITChoices")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoomunityRelatedChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAge)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentGrade)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Ecpiseries)
                    .HasColumnName("ECPISeries")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnrollmentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Gpa)
                    .HasColumnName("GPA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GraduateYesNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LifeStageChoices)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Myaenrollment2Year)
                    .HasColumnName("MYAEnrollment2Year")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrev)
                    .HasColumnName("MYAEnrollmentPrev")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrevExp)
                    .HasColumnName("MYAEnrollmentPrevExp")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentHomePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentNames)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenter)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenterExplain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodations)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodationsExplain)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StaffIntl)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.SummerWorkExpChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterAccount)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedResidency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.YearSubmitted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApplicantCopy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Applicant_Copy");

                entity.Property(e => e.AboutMyaexperience)
                    .HasColumnName("AboutMYAExperience")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Accepted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantId)
                    .HasColumnName("ApplicantID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ApplicantSignature)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicantSignatureDate).HasColumnType("datetime");

                entity.Property(e => e.Apt)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BlueSkyFundProgram)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Citchoices)
                    .HasColumnName("CITChoices")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoomunityRelatedChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAge)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentGrade)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Ecpiseries)
                    .HasColumnName("ECPISeries")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnrollmentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Gpa)
                    .HasColumnName("GPA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GraduateYesNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitleB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfServiceB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LifeStageChoices)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Myaenrollment2Year)
                    .HasColumnName("MYAEnrollment2Year")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrev)
                    .HasColumnName("MYAEnrollmentPrev")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MyaenrollmentPrevExp)
                    .HasColumnName("MYAEnrollmentPrevExp")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCellPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentHomePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentNames)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousCompanyB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibilitiesB)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenter)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RichmondTechnicalCenterExplain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodations)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialAccomodationsExplain)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StaffIntl)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.SummerWorkExpChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingChoices)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterAccount)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedResidency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.YearSubmitted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
