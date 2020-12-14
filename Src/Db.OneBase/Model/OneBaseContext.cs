using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Db.OneBase.Model
{
    public partial class OneBaseContext : DbContext
    {
        public OneBaseContext()
        {
        }

        public OneBaseContext(DbContextOptions<OneBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppStng> AppStng { get; set; }
        public virtual DbSet<Audition> Audition { get; set; }
        public virtual DbSet<LkuLanguage> LkuLanguage { get; set; }
        public virtual DbSet<LkuLevel> LkuLevel { get; set; }
        public virtual DbSet<LkuSubject> LkuSubject { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Problem> Problem { get; set; }
        public virtual DbSet<SessionResult> SessionResult { get; set; }
        public virtual DbSet<TombStone> TombStone { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VwEventUserUtc> VwEventUserUtc { get; set; }
        public virtual DbSet<VwLast100> VwLast100 { get; set; }
        public virtual DbSet<VwUser> VwUser { get; set; }
        public virtual DbSet<VwUserHopsUtc> VwUserHopsUtc { get; set; }
        public virtual DbSet<WebEventLog> WebEventLog { get; set; }
        public virtual DbSet<WebsiteUser> WebsiteUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=True;user id=;Password='';");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "APi");

            modelBuilder.Entity<AppStng>(entity =>
            {
                entity.ToTable("AppStng", "TCh");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.ProLtgl).HasColumnName("ProLTgl");

                entity.Property(e => e.SubLesnId).HasMaxLength(8);

                entity.Property(e => e.UserId).HasMaxLength(16);
            });

            modelBuilder.Entity<Audition>(entity =>
            {
                entity.ToTable("Audition", "SpB");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DoneAt).HasColumnType("datetime");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("Player_ID")
                    .HasMaxLength(128);

                entity.Property(e => e.ProblemId).HasColumnName("Problem_ID");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Audition)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_dbo.Audition_dbo.Player_Player_ID");

                entity.HasOne(d => d.Problem)
                    .WithMany(p => p.Audition)
                    .HasForeignKey(d => d.ProblemId)
                    .HasConstraintName("FK_dbo.Audition_dbo.Problem_Problem_ID");
            });

            modelBuilder.Entity<LkuLanguage>(entity =>
            {
                entity.ToTable("LkuLanguage", "SpB");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<LkuLevel>(entity =>
            {
                entity.ToTable("LkuLevel", "SpB");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<LkuSubject>(entity =>
            {
                entity.ToTable("LkuSubject", "SpB");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player", "SpB");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(128);

                entity.Property(e => e.AddedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.ToTable("Problem", "SpB");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddedAt).HasColumnType("datetime");

                entity.Property(e => e.AddedBy).HasMaxLength(24);

                entity.Property(e => e.BatchSource).HasMaxLength(24);

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(24);

                entity.Property(e => e.HintMessage).HasMaxLength(64);

                entity.Property(e => e.LanguageId).HasColumnName("Language_ID");

                entity.Property(e => e.LevelId).HasColumnName("Level_ID");

                entity.Property(e => e.Notes).HasMaxLength(128);

                entity.Property(e => e.ProblemText).HasMaxLength(24);

                entity.Property(e => e.SolutionText).HasMaxLength(24);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Problem)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_dbo.Problem_dbo.LkuLanguage_Language_ID");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Problem)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK_dbo.Problem_dbo.LkuLevel_Level_ID");
            });

            modelBuilder.Entity<SessionResult>(entity =>
            {
                entity.ToTable("SessionResult", "TCh");

                entity.Property(e => e.DoneAt).HasColumnType("datetime");

                entity.Property(e => e.ExcerciseName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionResult)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.SessionResult_dbo.User_UserId");
            });

            modelBuilder.Entity<TombStone>(entity =>
            {
                entity.ToTable("TombStone", "SpB");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(128);

                entity.Property(e => e.PlayerId)
                    .HasColumnName("Player_ID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.TombStone)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_dbo.TombStone_dbo.Player_Player_ID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "TCh");

                entity.Property(e => e.UserId).HasMaxLength(16);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.Note).HasColumnType("text");
            });

            modelBuilder.Entity<VwEventUserUtc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_EventUserUtc");

                entity.Property(e => e.DoneAt).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Nickname).HasMaxLength(32);
            });

            modelBuilder.Entity<VwLast100>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Last100");

                entity.Property(e => e.DoneAtLocalTime)
                    .HasColumnName("DoneAt LocalTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventData)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nickname).HasMaxLength(32);
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_User");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EventData)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastEvEst).HasColumnType("datetime");

                entity.Property(e => e.Nickname).HasMaxLength(32);
            });

            modelBuilder.Entity<VwUserHopsUtc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_UserHopsUtc");

                entity.Property(e => e.Finished).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nickname).HasMaxLength(32);

                entity.Property(e => e.ReviewedAt).HasColumnType("datetime");

                entity.Property(e => e.Started).HasColumnType("datetime");
            });

            modelBuilder.Entity<WebEventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DoneAt).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.WebsiteUserId).HasColumnName("WebsiteUserID");

                entity.HasOne(d => d.WebsiteUser)
                    .WithMany(p => p.WebEventLog)
                    .HasForeignKey(d => d.WebsiteUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebEventLog_WebsiteUser");
            });

            modelBuilder.Entity<WebsiteUser>(entity =>
            {
                entity.HasIndex(e => e.EventData)
                    .HasName("IX_WebsiteUser")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EventData).IsRequired();

                entity.Property(e => e.LastVisitAt).HasColumnType("datetime");

                entity.Property(e => e.Nickname).HasMaxLength(32);

                entity.Property(e => e.Note).HasColumnType("ntext");

                entity.Property(e => e.ReviewedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
