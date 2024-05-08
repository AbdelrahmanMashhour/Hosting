using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Enums;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<IdentityTokenVerification> IdentityTokenVerifications { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Unite> Unites { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Assignment>(x =>
            {
                x.HasMany(x => x.Solutions).WithOne(x => x.Assignment).HasForeignKey(x => x.AssignmentId).OnDelete(DeleteBehavior.Cascade);



                x.HasOne(x=>x.Unite).WithOne(x=>x.Assignment).HasForeignKey<Assignment>(x=>x.UniteId).OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Course>(x =>
            {
                x.Property(w => w.CoursStage).HasConversion(w => w.ToString(), w => (Stages)Enum.Parse(typeof(Stages), w)).HasMaxLength(10).HasColumnType("varchar");

                x.HasOne(s => s.Admin).WithMany(s => s.Courses).HasForeignKey(s => s.AdminId).OnDelete(DeleteBehavior.ClientCascade);



                x.HasMany(x => x.Students).WithMany(x => x.Courses).UsingEntity<StudentCourse>(l=>l.HasOne(x=>x.Student).WithMany(x=>x.StudentCourses).HasForeignKey(x=>x.StudentId).OnDelete(DeleteBehavior.Cascade), r => r.HasOne(s=>s.Course).WithMany(s=>s.StudentCourses).HasForeignKey(s=>s.CourseId).OnDelete(DeleteBehavior.Cascade));



                x.HasOne(x=>x.Unite).WithOne(x=>x.Course).HasForeignKey<Unite>(x=>x.CourseId).OnDelete(DeleteBehavior.Cascade);

                
                    
            });
            modelBuilder.Entity<RefreshToken>(x =>
            {
                x.HasOne(a => a.User).WithMany(r => r.RefreshTokens).HasForeignKey(f => f.UserId);
                x.HasKey(x => new { x.UserId, x.Token });
                x.Property(w => w.Token).HasColumnType("varchar").HasMaxLength(44);

            });
            modelBuilder.Entity<EmailVerificationCode>(x =>
            {
                x.HasKey(x => new { x.UserId, x.Code });
                x.Property(w => w.Code).HasMaxLength(10).HasColumnType("varchar");
            });
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(x => x.UserId);
                x.Property(w => w.FirstName).HasMaxLength(100);
                x.Property(w => w.LastName).HasMaxLength(100);
                x.HasDiscriminator(w => w.Role);
                x.HasData(
                new User { UserId = 1, FirstName = "The Knight", LastName= "Platform", Role=UserRole.Admin.ToString(), Email= "theknightahmedgaber@gmail.com", EmailConfirmed=true,Password= BCrypt.Net.BCrypt.EnhancedHashPassword("admin_password") }
                );

                x.Property(e => e.Role)
                .HasDefaultValue(UserRole.Student.ToString());

                x.HasOne(w => w.EmailVerificationCode).WithOne(w => w.User).HasForeignKey<EmailVerificationCode>(w => w.UserId);
                x.Property(w => w.Email).HasMaxLength(100).HasColumnType("varchar");
                x.Property(w => w.Password).HasMaxLength(100);
                x.HasIndex(w => w.Email).IsUnique();
                x.Property(w => w.ProfilePictureUrl).IsSparse();
                
            });
            modelBuilder.Entity<Student>(x =>
            {
                x.Property(b => b.Blocked).HasDefaultValue(false);
               
                x.HasMany(x => x.Assignments)
                .WithMany(x => x.Students)
                .UsingEntity<Solution>(l=>l.HasOne(o=>o.Assignment).WithMany(w=>w.Solutions).HasForeignKey(f=>f.AssignmentId),r=>r.HasOne(s=>s.Student).WithMany(m=>m.Solution).HasForeignKey(s=>s.StudentId));


            });
            //modelBuilder.Entity<Solution>(x =>
            //{

            //    //x.HasKey(x => new { x.AssignmentId,x.StudentId,x.SolutionId });

            //    x.HasOne(x => x.Student).WithOne(x => x.Solution).HasForeignKey<Solution>(x=>x.StudentId);
            //    x.HasOne(x => x.Assignment).WithMany(x => x.Solutions).HasForeignKey(x => x.AssignmentId);

            //});
            modelBuilder.Entity<IdentityTokenVerification>(x =>
            {
                x.HasOne(e => e.User).WithOne(x => x.IdentityTokenVerification).HasForeignKey<IdentityTokenVerification>(x => x.UserId);
                x.Property(x => x.Token).HasMaxLength(100);
                x.HasKey(x =>  new{x.UserId,x.Token});
            });
            modelBuilder.Entity<StudentPhones>(x =>
            {
                x.HasKey(x => new { x.StudentId, x.Phone, x.DadPhone });
                x.Property(x => x.Phone).HasMaxLength(11);
                x.Property(x => x.DadPhone).HasMaxLength(11);
            });
            modelBuilder.Entity<Unite>(x =>
            {
                x.HasKey(x => x.UnitId);

            });
        }
    }
}
