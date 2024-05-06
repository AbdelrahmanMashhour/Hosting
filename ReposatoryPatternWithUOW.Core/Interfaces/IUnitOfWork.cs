using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBaseRepository<UserConnection> UserConnection { get; }
        public IBaseRepository<IdentityTokenVerification> IdentityTokenVerification { get; }
        public IBaseRepository<Course> CourseRepository { get; }
        public IBaseRepository<Unite> UniteRepository { get; }
        public IBaseRepository<Student> StudentRepository { get; }
        public IBaseRepository<Assignment> AssignmentRepository { get; }
        public IBaseRepository<Solution> SolutionRepository { get; }
        public IBaseRepository<StudentCourse> StudentCourseRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
