using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.EF.Reposatories;
using RepositoryPatternWithUOW.Core.OptionsPatternClasses;
using RepositoryPatternWithUOW.EfCore.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPatternWithUOW.EfCore.Mapper;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RepositoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBaseRepository<IdentityTokenVerification> IdentityTokenVerification { get; }
        public IBaseRepository<Course> CourseRepository{get; }
        public IBaseRepository<Unite> UniteRepository { get; }
        public IBaseRepository<Student> StudentRepository { get; }
        public IBaseRepository<Assignment> AssignmentRepository { get; }
        public IBaseRepository<Solution> SolutionRepository { get; }
        public IBaseRepository<StudentCourse> StudentCourseRepository { get; }
        AppDbContext Context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UnitOfWork(AppDbContext context, TokenOptionsPattern options, Mapper mapper, ISenderService senderService, IHttpContextAccessor httpContextAccessor)
        {
            UserRepository = new UserRepository(context, options, mapper, senderService);
            Context = context;
            this.httpContextAccessor = httpContextAccessor;
            CourseRepository =new Repository<Course>(context,mapper, this.httpContextAccessor);
            IdentityTokenVerification=new Repository<IdentityTokenVerification>(context,mapper, this.httpContextAccessor);  
           UniteRepository =new Repository<Unite>(context,mapper, this.httpContextAccessor);
            StudentRepository=new Repository<Student>(context,mapper,this.httpContextAccessor);
            AssignmentRepository=new Repository<Assignment>(context, mapper, this.httpContextAccessor);
            SolutionRepository= new Repository<Solution>(context, mapper, this.httpContextAccessor);
            StudentCourseRepository = new Repository<StudentCourse>(context, mapper, httpContextAccessor);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await Context.SaveChangesAsync();
        }
    }
}
