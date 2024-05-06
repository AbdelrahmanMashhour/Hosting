using RepositoryPatternWithUOW.Core.Dto;
using RepositoryPatternWithUOW.Core.DTOs.CourseDTOs;
using RepositoryPatternWithUOW.Core.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EfCore.Mapper
{
    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnumMappingIgnoreCase = true)]
    public partial class Mapper
    {

        public partial Student MapToStudent(SignUpDto signUpDto);

        public partial Admin MapToAdmin(SignUpDto signUpDto);
        //public partial Course MapToCourse(AddCourseDto addCourseDto);
        //public partial Unite MapToUnite(UnitDto unitDto);

    }
}
