using System;
using Microsoft.EntityFrameworkCore;

namespace Db.OneBase.Model;

public partial class OneBaseContext__ : DbContext
{
  public OneBaseContext__() { }
  public OneBaseContext__(DbContextOptions<OneBaseContext__> options) : base(options) { }

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
  public virtual DbSet<GuestbookMsg> GuestbookMsg { get; set; } // 2021-01 manually added 

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      throw new NotImplementedException("▄▀▄▀▄▀▄▀▄▀▄▀ Never happenned yet ▄▀▄▀▄▀▄▀▄▀▄▀");
    }
  }

  [Obsolete]
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    _ = modelBuilder.HasAnnotation("Relational:DefaultSchema", "APi");

    _ = modelBuilder.Entity<AppStng>(entity =>
    {
      _ = entity.ToTable("AppStng", "TCh");

      _ = entity.Property(e => e.CreatedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.FullName).HasMaxLength(50);

      _ = entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.Note).HasColumnType("text");

      _ = entity.Property(e => e.ProLtgl).HasColumnName("ProLTgl");

      _ = entity.Property(e => e.SubLesnId).HasMaxLength(8);

      _ = entity.Property(e => e.UserId).HasMaxLength(16);
    });

    _ = modelBuilder.Entity<Audition>(entity =>
    {
      _ = entity.ToTable("Audition", "SpB");

      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.DoneAt).HasColumnType("datetime");

      _ = entity.Property(e => e.PlayerId)
                .HasColumnName("Player_ID")
                .HasMaxLength(128);

      _ = entity.Property(e => e.ProblemId).HasColumnName("Problem_ID");

      _ = entity.HasOne(d => d.Player)
                .WithMany(p => p.Audition)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_dbo.Audition_dbo.Player_Player_ID");

      _ = entity.HasOne(d => d.Problem)
                .WithMany(p => p.Audition)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("FK_dbo.Audition_dbo.Problem_Problem_ID");
    });

    _ = modelBuilder.Entity<LkuLanguage>(entity =>
    {
      _ = entity.ToTable("LkuLanguage", "SpB");

      _ = entity.Property(e => e.Id).HasColumnName("ID");
    });

    _ = modelBuilder.Entity<LkuLevel>(entity =>
    {
      _ = entity.ToTable("LkuLevel", "SpB");

      _ = entity.Property(e => e.Id).HasColumnName("ID");
    });

    _ = modelBuilder.Entity<LkuSubject>(entity =>
    {
      _ = entity.ToTable("LkuSubject", "SpB");

      _ = entity.Property(e => e.Id).HasColumnName("ID");
    });

    _ = modelBuilder.Entity<Player>(entity =>
    {
      _ = entity.ToTable("Player", "SpB");

      _ = entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasMaxLength(128);

      _ = entity.Property(e => e.AddedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.DeletedAt).HasColumnType("datetime");
    });

    _ = modelBuilder.Entity<Problem>(entity =>
    {
      _ = entity.ToTable("Problem", "SpB");

      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.AddedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.AddedBy).HasMaxLength(24);

      _ = entity.Property(e => e.BatchSource).HasMaxLength(24);

      _ = entity.Property(e => e.DeletedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.DeletedBy).HasMaxLength(24);

      _ = entity.Property(e => e.HintMessage).HasMaxLength(64);

      _ = entity.Property(e => e.LanguageId).HasColumnName("Language_ID");

      _ = entity.Property(e => e.LevelId).HasColumnName("Level_ID");

      _ = entity.Property(e => e.Notes).HasMaxLength(128);

      _ = entity.Property(e => e.ProblemText).HasMaxLength(24);

      _ = entity.Property(e => e.SolutionText).HasMaxLength(24);

      _ = entity.HasOne(d => d.Language)
                .WithMany(p => p.Problem)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_dbo.Problem_dbo.LkuLanguage_Language_ID");

      _ = entity.HasOne(d => d.Level)
                .WithMany(p => p.Problem)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK_dbo.Problem_dbo.LkuLevel_Level_ID");
    });

    _ = modelBuilder.Entity<SessionResult>(entity =>
    {
      _ = entity.ToTable("SessionResult", "TCh");

      _ = entity.Property(e => e.DoneAt).HasColumnType("datetime");

      _ = entity.Property(e => e.ExcerciseName)
                .IsRequired()
                .HasMaxLength(50);

      _ = entity.Property(e => e.Note).HasColumnType("text");

      _ = entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(16);

      _ = entity.HasOne(d => d.User)
                .WithMany(p => p.SessionResult)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.SessionResult_dbo.User_UserId");
    });

    _ = modelBuilder.Entity<TombStone>(entity =>
    {
      _ = entity.ToTable("TombStone", "SpB");

      _ = entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasMaxLength(128);

      _ = entity.Property(e => e.PlayerId)
                .HasColumnName("Player_ID")
                .HasMaxLength(128);

      _ = entity.HasOne(d => d.Player)
                .WithMany(p => p.TombStone)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_dbo.TombStone_dbo.Player_Player_ID");
    });

    _ = modelBuilder.Entity<User>(entity =>
    {
      _ = entity.ToTable("User", "TCh");

      _ = entity.Property(e => e.UserId).HasMaxLength(16);

      _ = entity.Property(e => e.CreatedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.FullName).HasMaxLength(50);

      _ = entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.Note).HasColumnType("text");
    });

    _ = modelBuilder.Entity<VwEventUserUtc>(entity =>
    {
      _ = entity.HasNoKey();

      _ = entity.ToView("vw_EventUserUtc");

      _ = entity.Property(e => e.DoneAt).HasColumnType("datetime");

      _ = entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(32);

      _ = entity.Property(e => e.Nickname).HasMaxLength(32);
    });

    _ = modelBuilder.Entity<VwLast100>(entity =>
    {
      _ = entity.HasNoKey();

      _ = entity.ToView("vw_Last100");

      _ = entity.Property(e => e.DoneAtLocalTime)
                .HasColumnName("DoneAt LocalTime")
                .HasColumnType("datetime");

      _ = entity.Property(e => e.EventData)
                .IsRequired()
                .HasMaxLength(450);

      _ = entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(32);

      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.Nickname).HasMaxLength(32);
    });

    _ = modelBuilder.Entity<VwUser>(entity =>
    {
      _ = entity.HasNoKey();

      _ = entity.ToView("vw_User");

      _ = entity.Property(e => e.CreatedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.EventData)
                .IsRequired()
                .HasMaxLength(450);

      _ = entity.Property(e => e.LastEvEst).HasColumnType("datetime");

      _ = entity.Property(e => e.Nickname).HasMaxLength(32);
    });

    _ = modelBuilder.Entity<VwUserHopsUtc>(entity =>
    {
      _ = entity.HasNoKey();

      _ = entity.ToView("vw_UserHopsUtc");

      _ = entity.Property(e => e.Finished).HasColumnType("datetime");

      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.Nickname).HasMaxLength(32);

      _ = entity.Property(e => e.ReviewedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.Started).HasColumnType("datetime");
    });

    _ = modelBuilder.Entity<WebEventLog>(entity =>
    {
      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.DoneAt).HasColumnType("datetime");

      _ = entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(32);

      _ = entity.Property(e => e.WebsiteUserId).HasColumnName("WebsiteUserID");

      _ = entity.HasOne(d => d.WebsiteUser)
                .WithMany(p => p.WebEventLog)
                .HasForeignKey(d => d.WebsiteUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WebEventLog_WebsiteUser");
    });

    _ = modelBuilder.Entity<WebsiteUser>(entity =>
    {
      _ = entity.HasIndex(e => e.EventData)
                .HasName("IX_WebsiteUser")
                .IsUnique();

      _ = entity.Property(e => e.Id).HasColumnName("ID");

      _ = entity.Property(e => e.CreatedAt).HasColumnType("datetime");

      _ = entity.Property(e => e.EventData).IsRequired();

      _ = entity.Property(e => e.LastVisitAt).HasColumnType("datetime");

      _ = entity.Property(e => e.Nickname).HasMaxLength(32);

      _ = entity.Property(e => e.Note).HasColumnType("ntext");

      _ = entity.Property(e => e.ReviewedAt).HasColumnType("datetime");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
