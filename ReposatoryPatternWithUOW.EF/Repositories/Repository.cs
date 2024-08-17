using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tsp;
using RepositoryPatternWithUOW.Core.DTOs.AssignmentDtos;
using RepositoryPatternWithUOW.Core.DTOs.CourseDTOs;
using RepositoryPatternWithUOW.Core.DTOs.PayProcess;
using RepositoryPatternWithUOW.Core.DTOs.Updates;
using RepositoryPatternWithUOW.Core.Enums;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.ReturnedModels;
using RepositoryPatternWithUOW.EfCore.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class Repository<T>:IBaseRepository<T> where T:class
    {
        AppDbContext context;
        Mapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(AppDbContext context, Mapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<int>> GetFreeCoursesId()
        {
            var result = new List<int>();
            result=await context.Courses.Where(c=>c.CoursePrice==0).Select(c=>c.CourseId).ToListAsync();
            return result;

        }
        public async Task<bool> ExistsAsync(int userId,int courseId)
        {
            return await context.StudentCourses.AnyAsync(sc => sc.StudentId == userId && sc.CourseId == courseId);
        }

        public async Task<T> AddAsync(T obj)
        {

            await context.AddAsync(obj);
            
            return obj;

        }
        public async Task<List<T>> AddRangeAsync(List<T> obj)
        {

            await context.AddRangeAsync(obj);
            
            return obj;

        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            var result= await query.Where(criteria).ToListAsync();
            return result;
        }
        
        public void Delete(T entity)
        {

           context.Remove(entity); 
            
        }
        public void DeleteAll(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                context.Remove(entity);
            }
        }



        public async Task<int> AddUnitAsync(UnitDto unitDto)
        {
            var unite = new Unite();
            Func<IFormFile, string,Task<string>> AddFile = async(file,fileName) =>
            {
                if (file != null && file.Length != 0)
                {
                    //Path.Combine(webHostEnvironment.WebRootPath, fileName);
                    var filePath = Path.Combine($"wwwroot", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                    }
                    return $"/{fileName}";
                }
                return null;
            };
            Task t1, t2, t3, t4, t5, t6, t7, t8, t9, t10;
            
            
            string s1, s2, s3, s4, s5, s6, s7, s8, s9;

            //s1="video"+Guid.NewGuid().ToString() + Path.GetExtension(unitDto.Exam?.FileName);
            //s2="video"+Guid.NewGuid().ToString() + Path.GetExtension(unitDto.Skill?.FileName);
            //s3="video"+Guid.NewGuid().ToString() + Path.GetExtension(unitDto.Translation?.FileName);
            //s4="video"+Guid.NewGuid().ToString() + Path.GetExtension(unitDto.Story?.FileName);
            //s5="video"+Guid.NewGuid().ToString() + Path.GetExtension(unitDto.Vocablary?.FileName);
            s6= Guid.NewGuid().ToString() + Path.GetExtension(unitDto.VocablaryPdf?.FileName);
            s7= Guid.NewGuid().ToString() + Path.GetExtension(unitDto.StoryPdf?.FileName);
            s8= Guid.NewGuid().ToString() + Path.GetExtension(unitDto.SkillPdf?.FileName);
            s9= Guid.NewGuid().ToString() + Path.GetExtension(unitDto.TranslationPdf?.FileName);

            unite.ExamUrl = unitDto.Exam;
            unite.SkillUrl = unitDto.Skill ;
            unite.TranslationUrl = unitDto.Translation ;
            unite.StoryUrl = unitDto.Story ;
            unite.VocablaryUrl = unitDto.Vocablary;



            unite.VocablaryPdfUrl = unitDto.VocablaryPdf is not null ? "/" + s6:null;
            unite.StoryPdfUrl = unitDto.StoryPdf is not null ? "/" + s7:null;
            unite.SkillPdfUrl = unitDto.SkillPdf is not null ? "/" +s8:null;
            unite.TranslationPdfUrl = unitDto.TranslationPdf is not null ? "/" +s9:null;


            //t1 =AddFile(unitDto.Exam, s1);

            //t2 = AddFile(unitDto.Skill, s2);

            //t3=AddFile(unitDto.Translation,s3);    
            //t4=AddFile(unitDto.Story,s4);
            //t5=AddFile(unitDto.Vocablary,s5);

            t6=AddFile(unitDto.VocablaryPdf,s6);
            t7=AddFile(unitDto.StoryPdf, s7);
            t8=AddFile(unitDto.SkillPdf, s8);
            t9=AddFile(unitDto.TranslationPdf,s9);

           

            unite.CourseId = unitDto.CourseId;
            
            
            await context.Unites.AddAsync(unite);

            t10 = context.SaveChangesAsync();
           await Task.WhenAll(/*t1, t2, t3, t4, t5, */t6, t7, t8, t9,t10);

            return unite.UnitId;

            
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> criteria)
        {
            return await context.Set<T>().AnyAsync(criteria);
        }
        public async Task<int> LastUnitId()
        {
            var unites= await context.Unites.ToListAsync();
            return unites is null? 0:unites.Last().UnitId;
        }
        public async Task<int> LastCourseId()
        {
            var unites= await context.Courses.ToListAsync();
            return unites is null? 0:unites.Last().CourseId;
        }

        public async Task<bool> DeleteById(Expression<Func<T, bool>> predicate)
        {
            var item = await context.Set<T>().FirstOrDefaultAsync(predicate);
            if (item is null)
            {
                return false;
            }
            context.Remove(item);
            return true;
        }

        public async Task<bool> DeleteFileById(int id)
        {

            var result = await context.Set<Unite>().Where(x => x.UnitId == id).ExecuteDeleteAsync();
            return result > 0;


        }
        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<bool> AddAssignmentAsync(AssignmentDto dto)
        {
            var assignment = new Assignment
            {
                FullMark = dto.FullMark,
                UniteId = dto.UnitId,
                Name=dto.Name
            };
            if (dto.AssFile != null && dto.AssFile.Length != 0)
            {
                var extintion = Path.GetExtension(dto.AssFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extintion;
                string? filePath;
                if (extintion==".txt")
                {
                     filePath = Path.Combine("wwwroot", fileName);
                    assignment.AssFiles = $"/{fileName}";

                }
                else if (extintion.Equals(".pdf",StringComparison.OrdinalIgnoreCase) || extintion.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                     filePath = Path.Combine("wwwroot", fileName);
                    assignment.AssFiles = $"/{fileName}";
                }
                else
                    return false;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.AssFile.CopyToAsync(stream);

                }


                await context.AddAsync(assignment);
                return true;
            }
            return false;
        }
        public async Task<bool> UploadSolution(SolutionDto dto)
        {
            var solution = new Solution
            {
                AssignmentId=dto.AssignmentId,
                StudentId=dto.StudentId
            };
            if (!await context.Students.AnyAsync(s=>s.UserId==dto.StudentId))
                return false;
            
            if (dto.SolutionFileUrl != null && dto.SolutionFileUrl.Length != 0)
            {
                var extintion= Path.GetExtension(dto.SolutionFileUrl.FileName);
                var fileName = Guid.NewGuid().ToString() + extintion;
                string? filePath;
                if (extintion == ".txt")
                {
                    filePath = Path.Combine("wwwroot", fileName);
                    solution.SolutionFileUrl= $"/{fileName}";

                }
                else if (extintion.Equals(".pdf", StringComparison.OrdinalIgnoreCase) || extintion.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    filePath = Path.Combine("wwwroot", fileName);
                    solution.SolutionFileUrl = $"/{fileName}";
                }
                else
                    return false;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.SolutionFileUrl.CopyToAsync(stream);
                }


                await context.AddAsync(solution);
                return true;
            }
            return false;
        }

        public async Task<bool> GiveGradeToStudent(GradeDto dto)
        {

            var solution = await context.Set<Solution>().SingleOrDefaultAsync(x=>x.AssignmentId==dto.AssignmentId&&x.StudentId==dto.StudentId);
            if(solution == null)
                return false;
            var assignment = await context.Set<Assignment>().Select(x => new { x.FullMark ,x.AssignmentId}).SingleOrDefaultAsync(x => x.AssignmentId == solution.AssignmentId);
            if (assignment == null)
                return false;
            if (dto.Grade > assignment.FullMark||dto.Grade<0)
                return false;
            solution.StudentDegree = dto.Grade;

            context.Update(solution); 
            return true;
        }
        //chek performance here
        public async Task<IEnumerable<StudentExams>> GetAllResolvedExam(int id)
        {
            var studentExams = new List<StudentExams>();
            var studentExam = new StudentExams();
            var assignments = await context.Solutions
            .Where(s => s.StudentId == id)
            .Select(s => s.Assignment) // Select only the Assignment objects
            .ToListAsync();

            foreach (var ass in assignments)
            {
                studentExam.StudentGrade = ass.Solutions.FirstOrDefault().StudentDegree;
                studentExam.FullMarkExam = ass.FullMark;
                studentExam.CourseName = ass.Unite.Course.CourseName;
                studentExam.ExamName = ass.Name;
                studentExams.Add(studentExam);
            }

            return studentExams;

           

        }
        public async Task<IEnumerable<Unite>?> AllUnitesByCourseId(int id,int UserId)
        {
            if (!await context.Courses.AnyAsync(x => x.CourseId == id))
            {
                return null;
            }
            context.ChangeTracker.LazyLoadingEnabled = false;

            var unites = await context.Unites.Include(x=>x.Assignment).Where(x => x.CourseId == id).ToListAsync();

            foreach (var item in unites)
            {
                item.SkillUrl= item.SkillUrl is null ?null:item.SkillUrl;


                item.SkillPdfUrl =item.SkillPdfUrl is null?null: _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + item.SkillPdfUrl;


                item.TranslationPdfUrl = item.TranslationPdfUrl is null?null: _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + item.TranslationPdfUrl;


                item.TranslationUrl = item.TranslationUrl is null?null: item.TranslationUrl;


                item.ExamUrl =item.ExamUrl is null?null:item.ExamUrl;

                item.VocablaryPdfUrl = item.VocablaryPdfUrl is null ? null : _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + item.VocablaryPdfUrl;

                item.VocablaryUrl = item.VocablaryUrl is null ? null :item.VocablaryUrl;


                item.StoryPdfUrl = item.StoryPdfUrl is null ? null : _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + item.StoryPdfUrl;
                
                
                item.StoryUrl = item.StoryUrl is null ? null : item.StoryUrl;


               
                item.Assignment.AssFiles= _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + item.Assignment.AssFiles;

               
                
            }
            return unites;

        }
        public async Task<IEnumerable<User>> AllStudentInCoursByCourseId(int CourseId)
        {
            var studentCours = await context.StudentCourses.Where(c => c.CourseId == CourseId).ToListAsync();
            if (studentCours==null) return Enumerable.Empty<User>();
            var users = new List<User>();
            foreach (var item in studentCours)
            {
                var user=await context.Users.SingleOrDefaultAsync(x => x.UserId == item.StudentId);
                if (user!=null)
                {
                    users.Add(user);
                }

            }
            return users;
        }
       

        public async Task<string> AddStudentToCourse(PayInputDto dto)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (student==null)
            {
                return $"You Don't have this Student Email {dto.Email} ";
            }
            var course = await context.Courses.SingleOrDefaultAsync(x => x.CourseId == dto.CourseId);
            if (course == null)
            {
                return $"You Don't have this Course id {dto.CourseId} ";
            }
            var JoinedAtStr = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            var studentCourse = new StudentCourse
            {
                CourseId = dto.CourseId,
                StudentId = student.UserId,
                
             JoinedAt = DateOnly.ParseExact(JoinedAtStr, "dd/MM/yyyy", CultureInfo.InvariantCulture)


            };
            await context.AddAsync(studentCourse);
            return "Sucsess Process";

        }
        
        
        public async Task<string> DeleteStudentToCourse(PayInputDto dto)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Email == dto.Email);
            if (student==null)
            {
                return $"You Don't have this Student Email {dto.Email} ";
            }
            var course = await context.Courses.SingleOrDefaultAsync(x => x.CourseId == dto.CourseId);
            if (course == null)
            {
                return $"You Don't have this Course id {dto.CourseId} ";
            }
            var result = context.StudentCourses.Where(x => x.CourseId == dto.CourseId && x.StudentId == student.UserId).ExecuteDelete();
            
            return "Sucsess Process";

        }

        public async Task<bool> DeleteCourseById(int id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course is null)
            {
                return false;
            }
            var unites = await context.Unites.Where(x => x.CourseId == id).ToListAsync();
            Func<string, bool> DeleteFile = e =>
            {
                if (e is null)
                    return false;
                string pathUnite = "wwwroot" + e;
                
                if (File.Exists(pathUnite))
                {
                    File.Delete(pathUnite);
                    return true;
                }
                return false;
            };

            foreach (var unite in unites)
            {
                var assignments = await context.Assignments.Where(a => a.UniteId == unite.UnitId).ToListAsync();
                foreach (var assignment in assignments)
                {
                    var solutions = await context.Solutions.Where(sol => sol.AssignmentId == assignment.AssignmentId).ToListAsync();
                    foreach (var solution in solutions)
                    {
                        string path = "wwwroot" + solution.SolutionFileUrl;

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                    }
                    string pathAss = "wwwroot" + assignment.AssFiles;

                    if (File.Exists(pathAss))
                    {
                        File.Delete(pathAss);
                    }

                }
                DeleteFile(unite.VocablaryUrl);
                DeleteFile(unite.VocablaryPdfUrl);
                DeleteFile(unite.SkillUrl);
                DeleteFile(unite.SkillPdfUrl);
                DeleteFile(unite.StoryUrl);
                DeleteFile(unite.StoryPdfUrl);
                DeleteFile(unite.TranslationUrl);
                DeleteFile(unite.TranslationPdfUrl);
                DeleteFile(unite.ExamUrl);

            }
            DeleteFile(course.ProfileUrl);
            context.Courses.Where(c=>c.CourseId==id).ExecuteDelete();

            return true;
       
        }

        public async Task<IEnumerable<StudentSolutionsDto>> GetSolutionsData()
        {
            var studentSolutionsDto = new List<StudentSolutionsDto>();
            var solutions = await context.Solutions.ToListAsync();

            foreach (var solution in solutions)
            {
                var studentSolution = new StudentSolutionsDto(); // Create a new instance in each iteration

                studentSolution.AssignmentId = solution.AssignmentId;
                studentSolution.StudentId = solution.StudentId;

                var assignment = await context.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == solution.AssignmentId);

                studentSolution.FullMark = assignment.FullMark;
                studentSolution.AssignmentName = assignment.Name;

                var student = await context.Students.FirstOrDefaultAsync(s => s.UserId == solution.StudentId);

                studentSolution.StudentName = student.FirstName + " " + student.LastName;

                var course = await context.Courses.FirstOrDefaultAsync(s => s.CourseId == assignment.Unite.CourseId);

                studentSolution.CourseName = course.CourseName;
                studentSolution.Stage = course.CoursStage;

                solution.SolutionFileUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + solution.SolutionFileUrl;

                studentSolution.SolutionUrl = solution.SolutionFileUrl;
                studentSolutionsDto.Add(studentSolution);
            }

            return studentSolutionsDto;
        }


        public async Task<IEnumerable<RetriveCourses>> AllCoursesAsync()
        {

            
            var TotalCourses = await context.Courses.ToListAsync();
            
          

            var courses = new List<RetriveCourses>();

            foreach (var course in TotalCourses)
            {
                courses.Add(new RetriveCourses 
                {
                    CourseId = course.CourseId,
                    CourseDescription=course.CourseDescription,
                    CourseName=course.CourseName,
                    CoursePrice=course.CoursePrice,
                    CoursStage=course.CoursStage,
                    
                    
                    profileUrl=course.ProfileUrl is null ? null : _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + course.ProfileUrl

                });

            }

            return courses;
        }
        /// <summary>
        /// for admine
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RetriveCourses>> AllCoursesAsync(int stuId)
        {


            var TotalCourses = await context.StudentCourses
            .Where(sc => sc.StudentId == stuId)
            .Join(context.Courses,
                sc => sc.CourseId,
                c => c.CourseId,
                (sc, c) => c)
            .ToListAsync();

            var courses = new List<RetriveCourses>();

            foreach (var course in TotalCourses)
            {
                courses.Add(new RetriveCourses
                {
                    CourseId = course.CourseId,
                    CourseDescription = course.CourseDescription,
                    CourseName = course.CourseName,
                    CoursePrice = course.CoursePrice,
                    CoursStage = course.CoursStage,
                    TotoalHoure=course.TotoalHoure,

                    profileUrl = course.ProfileUrl is null ? null : _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + course.ProfileUrl

                });

            }

            return courses;
        }

  

        public async Task<bool> UpdateCourse(JsonPatchDocument<Course> dto,int id)
        {
            //To Prevent Update CourseId  and AdminId
            if (dto.Operations.Exists(x=>x.path.Equals("CourseId",StringComparison.OrdinalIgnoreCase)|| x.path.Equals("AdminId", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            var course = await context.Courses.FindAsync(id);
            if (course is null) return false;

            dto.ApplyTo(course);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUniteAsync(UnitDto dto,int id)
        {
            var unite = await context.Unites.FindAsync(id);
            if (unite is null) return false;
            

            Func<IFormFile,string,Task<string>> updateHandler=async (file,fileUrl)=>
            {
                if (file is not null && fileUrl is not null)
                {//update
                    using (var stream = new FileStream("wwwroot" + fileUrl, FileMode.Truncate, FileAccess.Write))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return "true";
                }
                else if (file is not null && fileUrl is null)
                {
                    //add

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var extention = Path.GetExtension(fileName);
                  

                    var filePath = Path.Combine("wwwroot", fileName);

                    fileUrl = $"/{fileName}";


                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return fileUrl;
                    
                }

                else if (file is null && fileUrl is not null)
                {
                    //delete
                    if (File.Exists("wwwroot" + fileUrl))
                    {
                        File.Delete("wwwroot" + fileUrl);
                        fileUrl = null;
                        return "null";
                    }
                    else
                    {
                        return null;
                    }


                }
                //if he doesn't send and i dont have
                return "true";
            };

            Task<string> //t1,
                         //t2,
                         t3,
                         //t4,
                         t5,
                         //t6,
                         t7,
                         //t8,
                         t9;
            //t1= updateHandler(dto.Exam, unite.ExamUrl);
            //t2= updateHandler(dto.Vocablary, unite.VocablaryUrl);
            //t4 =updateHandler(dto.Story, unite.StoryUrl);
            //t6 = updateHandler(dto.Skill, unite.SkillUrl);
            //t8 = updateHandler(dto.Translation, unite.TranslationUrl);

            t3= updateHandler(dto.VocablaryPdf, unite.VocablaryPdfUrl);
            t5 = updateHandler(dto.StoryPdf, unite.StoryPdfUrl);
            t7 = updateHandler(dto.SkillPdf, unite.SkillPdfUrl);
            t9 = updateHandler(dto.TranslationPdf, unite.TranslationPdfUrl);

            var resultTask = await Task.WhenAll(/*t1, t2,*/ t3, /*t4,*/ t5, /*t6,*/ t7, /*t8,*/ t9);


            //if (resultTask[0] == "null")
            //    unite.ExamUrl = null;
            //else if (resultTask[0] == null)
            //    return false;
            //else if (resultTask[0] != "true")
            //    unite.ExamUrl = resultTask[0];
            unite.ExamUrl = dto.Exam;


            //if (resultTask[1] == "null")
            //    unite.VocablaryUrl = null;
            //else if (resultTask[1] == null)
            //    return false;
            //else if (resultTask[1] != "true")
            //    unite.VocablaryUrl = resultTask[1];
            unite.VocablaryUrl = dto.Vocablary;

            if (resultTask[0] == "null")
                unite.VocablaryPdfUrl = null;
            else if (resultTask[0] == null)
                return false;
            else if (resultTask[0] != "true")
                unite.VocablaryPdfUrl = resultTask[0];


            //if (resultTask[3] == "null")
            //    unite.StoryUrl = null;
            //else if (resultTask[3] == null)
            //    return false;
            //else if (resultTask[3] != "true")
            //    unite.StoryUrl = resultTask[3];
            unite.StoryUrl = dto.Story;



            if (resultTask[1] == "null")
                unite.StoryPdfUrl = null;
            else if (resultTask[1] == null)
                return false;
            else if (resultTask[1] != "true")
                unite.StoryPdfUrl = resultTask[1];


            //if (resultTask[5] == "null")
            //    unite.SkillUrl = null;
            //else if (resultTask[5] == null)
            //    return false;
            //else if (resultTask[5] != "true")
            //    unite.SkillUrl = resultTask[5];
            unite.SkillUrl=dto.Skill;

            if (resultTask[2] == "null")
                unite.SkillPdfUrl = null;
            else if (resultTask[2] == null)
                return false;
            else if (resultTask[2] != "true")
                unite.SkillPdfUrl = resultTask[2];

            //if (resultTask[7] == "null")
            //    unite.TranslationUrl = null;
            //else if (resultTask[7] == null)
            //    return false;
            //else if (resultTask[7] != "true")
            //    unite.TranslationUrl = resultTask[7];
            unite.TranslationUrl=dto.Translation;

            if (resultTask[3] == "null")
                unite.TranslationPdfUrl = null;
            else if (resultTask[3] == null)
                return false;
            else if (resultTask[3] != "true")
                unite.TranslationPdfUrl = resultTask[3];



            return true;




        }
        public async Task<bool> IsPayOrNot(int studentId, int courseId)
        {
            return await context.StudentCourses.AnyAsync(st=>st.StudentId==studentId && st.CourseId==courseId);
        }

        public async Task<IEnumerable<StudenPayment>> GetStudenPaymentByCourseId(int courseId)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;

            var studenCourses = await context.StudentCourses.Include(x=>x.Course).Include(x=>x.Student).Where(x => x.CourseId == courseId).AsNoTracking().ToListAsync();

            var studentPayments = new List<StudenPayment>();
            foreach (var studentCourse in studenCourses)
            {

                var studentPayment = new StudenPayment();
                studentPayment.FirstName= studentCourse.Student.FirstName;
                studentPayment.LastName= studentCourse.Student.LastName;
                studentPayment.Email= studentCourse.Student.Email;
                studentPayment.JoinedAt = studentCourse.JoinedAt;
                studentPayment.Stage = studentCourse.Course.CoursStage;
                studentPayment.CourseName = studentCourse.Course.CourseName;

                studentPayments.Add(studentPayment);
            }
            return studentPayments;
        }

        public async Task<bool> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ExecuteDeleteAsync()>0;
        }
    }
}
