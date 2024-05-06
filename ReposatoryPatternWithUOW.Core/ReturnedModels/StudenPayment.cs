using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.ReturnedModels
{
    public class StudenPayment
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly JoinedAt { get; set; }
        public string CourseName { get; set; }
        public Stages Stage { get; set; }

    }
}
