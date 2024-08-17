using Microsoft.AspNetCore.JsonPatch;
using RepositoryPatternWithUOW.Core.DTOs.AssignmentDtos;
using RepositoryPatternWithUOW.Core.DTOs.CourseDTOs;
using RepositoryPatternWithUOW.Core.DTOs.PayProcess;
using RepositoryPatternWithUOW.Core.DTOs.Updates;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.ReturnedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IBaseRepository<T>where T : class
    {
        Task<T> AddAsync(T obj);
        Task<List<T>> AddRangeAsync(List<T> obj);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<bool> IsExist(Expression<Func<T,bool>> criteria);
        Task<int> AddUnitAsync(UnitDto unitDto);
        public Task<int> LastUnitId();
        public Task<int> LastCourseId();
        Task<T> GetById(int id);
        public void Delete(T entity);
        public Task<bool> DeleteById(Expression<Func<T, bool>> predicate);
        public void DeleteAll(IEnumerable<T> entities);
       // public Task<bool> DeleteUniteById(int id);
        public Task<bool> AddAssignmentAsync(AssignmentDto dto);
        public Task<bool> UploadSolution(SolutionDto dto);
        public Task<bool> GiveGradeToStudent(GradeDto dto);
        public Task<IEnumerable<StudentExams>> GetAllResolvedExam(int id);
        public Task<IEnumerable<Unite>?> AllUnitesByCourseId(int id,int UserId);
        public Task<IEnumerable<User>> AllStudentInCoursByCourseId(int CourseId);

        ////////////////////////////////////////////////////////////////
        ///Payment process
        ///
        public Task<string> AddStudentToCourse(PayInputDto dto);
        public Task<string> DeleteStudentToCourse(PayInputDto dto);
        public Task<bool> DeleteCourseById(int id);
        public Task<bool> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<StudentSolutionsDto>> GetSolutionsData();

        public Task<IEnumerable<RetriveCourses>> AllCoursesAsync();
        public Task<List<int>> GetFreeCoursesId();
        //for spadefic student
        public Task<IEnumerable<RetriveCourses>> AllCoursesAsync(int stuId);

        ////
        ///
        public Task<bool> UpdateCourse(JsonPatchDocument<Course> dto,int id);
        public Task<bool> UpdateUniteAsync(UnitDto dto,int id);

        public Task<bool> IsPayOrNot(int studentId, int courseId);

        public Task<IEnumerable<StudenPayment>> GetStudenPaymentByCourseId(int courseId);

        public Task<bool> ExistsAsync(int userId, int courseId);


    }
}
