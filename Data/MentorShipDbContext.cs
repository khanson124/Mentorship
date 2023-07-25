using System;
using System.Collections.Generic;
using Mentorship.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Data;

public partial class MentorShipDbContext : DbContext
{
    public MentorShipDbContext()
    {
    }

    public MentorShipDbContext(DbContextOptions<MentorShipDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mentee> Mentees { get; set; }

    public virtual DbSet<Mentor> Mentors { get; set; }

    public virtual DbSet<MentorshipSession> MentorshipSessions { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MentorShipDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mentee>(entity =>
        {
            entity.HasKey(e => e.MenteeId).HasName("PK__Mentees__C079F297B66B7ADD");

            entity.Property(e => e.MenteeId)
                .ValueGeneratedNever()
                .HasColumnName("mentee_id");
            entity.Property(e => e.Bio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bio");
            entity.Property(e => e.Interests)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("interests");
            entity.Property(e => e.PreferredMentorAgeRange)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("preferred_mentor_age_range");
            entity.Property(e => e.PreferredMentorGender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("preferred_mentor_gender");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Mentees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Mentees__user_id__29572725");
        });

        modelBuilder.Entity<Mentor>(entity =>
        {
            entity.HasKey(e => e.MentorId).HasName("PK__Mentors__E5D27EF3521379FF");

            entity.Property(e => e.MentorId)
                .ValueGeneratedNever()
                .HasColumnName("mentor_id");
            entity.Property(e => e.AreaOfExpertise)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("area_of_expertise");
            entity.Property(e => e.Availability)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("availability");
            entity.Property(e => e.Bio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bio");
            entity.Property(e => e.HourlyRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("hourly_rate");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Mentors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Mentors__user_id__267ABA7A");
        });

        modelBuilder.Entity<MentorshipSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Mentorsh__69B13FDCEE44AE1E");

            entity.Property(e => e.SessionId)
                .ValueGeneratedNever()
                .HasColumnName("session_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.MenteeId).HasColumnName("mentee_id");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.SessionNotes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("session_notes");
            entity.Property(e => e.SessionRating).HasColumnName("session_rating");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Mentee).WithMany(p => p.MentorshipSessions)
                .HasForeignKey(d => d.MenteeId)
                .HasConstraintName("FK__Mentorshi__mente__2D27B809");

            entity.HasOne(d => d.Mentor).WithMany(p => p.MentorshipSessions)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__Mentorshi__mento__2C3393D0");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__0BBF6EE642E1AD51");

            entity.Property(e => e.MessageId)
                .ValueGeneratedNever()
                .HasColumnName("message_id");
            entity.Property(e => e.MessageContent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("message_content");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__Messages__receiv__30F848ED");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__sender__300424B4");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D90458EA0C0");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("review_id");
            entity.Property(e => e.MenteeId).HasColumnName("mentee_id");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasColumnType("date")
                .HasColumnName("review_date");
            entity.Property(e => e.ReviewText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("review_text");

            entity.HasOne(d => d.Mentee).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MenteeId)
                .HasConstraintName("FK__Reviews__mentee___34C8D9D1");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__Reviews__mentor___33D4B598");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF27E283D");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_picture");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("date")
                .HasColumnName("registration_date");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
