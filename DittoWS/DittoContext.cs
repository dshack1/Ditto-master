using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DittoWS.Models;

namespace DittoWS
{
    public partial class DittoContext : DbContext
    {
        #region Ditto
        public virtual DbSet<Models.AuthToken> AuthToken { get; set; }
        public virtual DbSet<Models.Attribute> Attribute { get; set; }
        public virtual DbSet<Models.MiscLocale> MiscLocale { get; set; }
        public virtual DbSet<Models.Question> Question { get; set; }
        public virtual DbSet<Models.QuestionAttribute> QuestionAttribute { get; set; }
        public virtual DbSet<Models.QuestionLocale> QuestionLocale { get; set; }
        public virtual DbSet<Models.QuestionLogic> QuestionLogic { get; set; }
        public virtual DbSet<Models.QuestionMulti> QuestionMulti { get; set; }
        public virtual DbSet<Models.QuestionMultiLocale> QuestionMultiLocale { get; set; }
        public virtual DbSet<Models.QuestionType> QuestionType { get; set; }
        public virtual DbSet<Models.QuestionValue> QuestionValue { get; set; }
        public virtual DbSet<Models.QuestionValueLocale> QuestionValueLocale { get; set; }
        public virtual DbSet<Models.Response> Response { get; set; }
        public virtual DbSet<Models.ResponseAlert> ResponseAlert { get; set; }
        public virtual DbSet<Models.ResponseHistory> ResponseHistory { get; set; }
        public virtual DbSet<Models.ResponseInvalidSubject> ResponseInvalidSubject { get; set; }
        public virtual DbSet<Models.ResponseItem> ResponseItem { get; set; }
        public virtual DbSet<Models.Survey> Survey { get; set; }
        public virtual DbSet<Models.SurveyLocale> SurveyLocale { get; set; }
        #endregion

        public DittoContext(DbContextOptions<DittoContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Ditto
            modelBuilder.Entity<Models.AuthToken>(entity =>
            {
                entity.HasKey(e => new { e.AuthUser_ID, e.Token })
                    .HasName("PK_AuthToken");

                entity.ToTable("AuthToken", "dbo");

                entity.Property(e => e.AuthUser_ID).HasColumnName("AuthUser_ID");

                entity.Property(e => e.Token)
                    .HasColumnType("varchar(38)")
                    .HasDefaultValueSql("newid()");

                entity.Property(e => e.Expired).HasDefaultValueSql("0");

                entity.Property(e => e.Issued)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getutcdate()");

                entity.Property(e => e.Last_Used)
                    .HasColumnName("Last_Used")
                    .HasColumnType("datetime");
            });
            modelBuilder.Entity<Models.Attribute>(entity =>
            {
                entity.HasKey(e => e.Attribute_Code)
                    .HasName("PK_Attribute");

                entity.ToTable("Attribute", "dbo");

                entity.Property(e => e.Attribute_Code)
                    .HasColumnName("Attribute_Code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Attribute_Desc)
                    .IsRequired()
                    .HasColumnName("Attribute_Desc")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<MiscLocale>(entity =>
            {
                entity.HasKey(e => new { e.Misc_Code, e.Locale_Code })
                    .HasName("PK_Misc_Locale");

                entity.ToTable("Misc_Locale", "dbo");

                entity.Property(e => e.Misc_Code)
                    .HasColumnName("Misc_Code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Misc_Text)
                    .IsRequired()
                    .HasColumnName("Misc_Text")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => new { e.Question_ID }).HasName("PK_Question");
                entity.ToTable("Question", "dbo");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Default_Answer)
                    .HasColumnName("Default_Answer")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Is_Active)
                    .HasColumnName("Is_Active")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Is_Required)
                    .HasColumnName("Is_Required")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Ord_By).HasColumnName("Ord_By");

                entity.Property(e => e.Question_Alias)
                    .IsRequired()
                    .HasColumnName("Question_Alias")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Question_Type_ID).HasColumnName("Question_Type_ID");

                entity.Property(e => e.Survey_ID).HasColumnName("Survey_ID");
            });
            modelBuilder.Entity<QuestionAttribute>(entity =>
            {
                entity.HasKey(e => new { e.Question_ID, e.Attribute_Code })
                    .HasName("PK_Question_Attribute");

                entity.ToTable("Question_Attribute", "dbo");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Attribute_Code)
                    .HasColumnName("Attribute_Code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Attribute_Value)
                    .HasColumnName("Attribute_Value")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<QuestionLocale>(entity =>
            {
                entity.HasKey(e => new { e.Question_ID, e.Locale_Code })
                    .HasName("PK_Question_Locale");

                entity.ToTable("Question_Locale", "dbo");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Question_Short)
                    .HasColumnName("Question_Short")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Question_Subtext)
                    .HasColumnName("Question_Subtext")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Question_Text)
                    .HasColumnName("Question_Text")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<QuestionLogic>(entity =>
            {
                entity.HasKey(e => new { e.Question_Logic_ID }).HasName("PK_Question_Logic");
                entity.ToTable("Question_Logic", "dbo");

                entity.Property(e => e.Question_Logic_ID).HasColumnName("Question_Logic_ID");

                entity.Property(e => e.Logic)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Target_Question_ID).HasColumnName("Target_Question_ID");

                entity.Property(e => e.Target_Value)
                    .HasColumnName("Target_Value")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Target_Visible).HasColumnName("Target_Visible");
            });
            modelBuilder.Entity<QuestionMulti>(entity =>
            {
                entity.HasKey(e => new { e.Question_Multi_ID }).HasName("PK_Question_Multi");
                entity.ToTable("Question_Multi", "dbo");

                entity.Property(e => e.Question_Multi_ID).HasColumnName("Question_Multi_ID");

                entity.Property(e => e.Always_Show_Comment).HasColumnName("Always_Show_Comment");

                entity.Property(e => e.Has_Comment_Field).HasColumnName("Has_Comment_Field");

                entity.Property(e => e.Ord_By).HasColumnName("Ord_By");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Question_Multi_Alias)
                    .IsRequired()
                    .HasColumnName("Question_Multi_Alias")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<QuestionMultiLocale>(entity =>
            {
                entity.HasKey(e => new { e.Question_Multi_ID, e.Locale_Code })
                    .HasName("PK_Question_Multi_Locale");

                entity.ToTable("Question_Multi_Locale", "dbo");

                entity.Property(e => e.Question_Multi_ID).HasColumnName("Question_Multi_ID");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Question_Text)
                    .HasColumnName("Question_Text")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.HasKey(e => new { e.Question_Type_ID }).HasName("PK_Question_Type");
                entity.ToTable("Question_Type", "dbo");

                entity.Property(e => e.Question_Type_ID)
                    .HasColumnName("Question_Type_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Question_Type_Name)
                    .IsRequired()
                    .HasColumnName("Question_Type_Name")
                    .HasColumnType("varchar(100)");
            });
            modelBuilder.Entity<QuestionValue>(entity =>
            {
                entity.HasKey(e => new { e.Question_ID, e.Value_ID })
                    .HasName("PK_Question_Value");

                entity.ToTable("Question_Value", "dbo");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Value_ID).HasColumnName("Value_ID");

                entity.Property(e => e.Is_Selected).HasColumnName("Is_Selected");

                entity.Property(e => e.Ord_By).HasColumnName("Ord_By");

                entity.Property(e => e.Value_Alias)
                    .HasColumnName("Value_Alias")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<QuestionValueLocale>(entity =>
            {
                entity.HasKey(e => new { e.Question_ID, e.Value_ID, e.Locale_Code })
                    .HasName("PK_Question_Value_locale");

                entity.ToTable("Question_Value_Locale", "dbo");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Value_ID).HasColumnName("Value_ID");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Value_Text)
                    .IsRequired()
                    .HasColumnName("Value_Text")
                    .HasColumnType("varchar(max)");
            });
            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => new { e.Response_ID }).HasName("PK_Response");

                entity.ToTable("Response", "dbo");

                entity.Property(e => e.Response_ID)
                    .HasColumnName("Response_ID")
                    .HasDefaultValueSql("newsequentialid()");

                entity.Property(e => e.End_DateTime)
                    .HasColumnName("End_DateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Is_Deleted)
                    .HasColumnName("Is_Deleted")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Notes).HasColumnType("varchar(max)");

                entity.Property(e => e.Start_DateTime)
                    .HasColumnName("Start_DateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Subject_ID)
                    .IsRequired()
                    .HasColumnName("Subject_ID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Survey_ID).HasColumnName("Survey_ID");
            });
            modelBuilder.Entity<ResponseAlert>(entity =>
            {
                entity.HasKey(e => e.Response_ID)
                    .HasName("PK_Response_Alert");

                entity.ToTable("Response_Alert", "dbo");

                entity.Property(e => e.Response_ID)
                    .HasColumnName("Response_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Acknowledged).HasColumnType("datetime");

                entity.Property(e => e.AuthUser_ID).HasColumnName("AuthUser_ID");

                entity.Property(e => e.EmailQueue_ID).HasColumnName("EmailQueue_ID");
            });
            modelBuilder.Entity<ResponseHistory>(entity =>
            {
                entity.HasKey(e => new { e.Response_History_ID }).HasName("PK_Response_History");

                entity.ToTable("Response_History", "dbo");

                entity.Property(e => e.Response_History_ID)
                    .HasColumnName("Response_History_ID")
                    .HasDefaultValueSql("newsequentialid()");

                entity.Property(e => e.Response_Item_ID)
                    .HasColumnName("Response_Item_ID");

                entity.Property(e => e.Edit_AuthUser_ID).HasColumnName("Edit_AuthUser_ID");

                entity.Property(e => e.Edited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Old_Answer)
                    .HasColumnName("Old_Answer")
                    .HasColumnType("varchar(max)");

            });
            modelBuilder.Entity<ResponseInvalidSubject>(entity =>
            {
                entity.HasKey(e => e.Response_ID)
                    .HasName("PK_Response_InvalidSubject");

                entity.ToTable("Response_InvalidSubject", "dbo");

                entity.Property(e => e.Response_ID)
                    .HasColumnName("Response_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Acknowledged).HasColumnType("datetime");

                entity.Property(e => e.AuthUser_ID).HasColumnName("AuthUser_ID");

                entity.Property(e => e.EmailQueue_ID).HasColumnName("EmailQueue_ID");

                entity.Property(e => e.Subject_ID)
                    .IsRequired()
                    .HasColumnName("Subject_ID")
                    .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<ResponseItem>(entity =>
            {
                entity.HasKey(e => new { e.Response_ID, e.Question_ID, e.Question_Multi_ID, e.Additional_ID })
                    .HasName("PK_Response_item");

                entity.ToTable("Response_Item", "dbo");

                entity.Property(e => e.Response_ID).HasColumnName("Response_ID");

                entity.Property(e => e.Question_ID).HasColumnName("Question_ID");

                entity.Property(e => e.Question_Multi_ID).HasColumnName("Question_Multi_ID");

                entity.Property(e => e.Additional_ID).HasColumnName("Additional_ID");

                entity.Property(e => e.Answer).HasColumnType("varchar(max)");

                entity.Property(e => e.Question_Alias)
                    .IsRequired()
                    .HasColumnName("Question_Alias")
                    .HasColumnType("varchar(200)");
            });
            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => new { e.Survey_ID }).HasName("PK_Survey");

                entity.ToTable("Survey", "dbo");

                entity.Property(e => e.Survey_ID)
                    .HasColumnName("Survey_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Addtl_JQuery)
                    .HasColumnName("Addtl_JQuery")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Is_Active).HasColumnName("Is_Active");

                entity.Property(e => e.Score_Limit).HasColumnName("Score_Limit");

                entity.Property(e => e.Survey_Alias)
                    .IsRequired()
                    .HasColumnName("Survey_Alias")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Task_ID).HasColumnName("Task_ID");
            });
            modelBuilder.Entity<SurveyLocale>(entity =>
            {
                entity.HasKey(e => new { e.Survey_ID, e.Locale_Code })
                    .HasName("PK_Survey_Locale");

                entity.ToTable("Survey_Locale", "dbo");

                entity.Property(e => e.Survey_ID).HasColumnName("Survey_ID");

                entity.Property(e => e.Locale_Code)
                    .HasColumnName("Locale_Code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Survey_Name)
                    .IsRequired()
                    .HasColumnName("Survey_Name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Survey_ShortName)
                    .IsRequired()
                    .HasColumnName("Survey_ShortName")
                    .HasColumnType("varchar(50)");
            });
            #endregion
        }
    }
}
