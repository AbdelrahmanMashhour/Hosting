using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int UniteId { get; set; }
        public string Name { get; set; }
        public string? AssFiles { get; set; }

        public int ? FullMark { get; set; }

        [JsonIgnore]
        public virtual List<Solution> Solutions { get; set; } = new();
        [JsonIgnore]
        public virtual List<Student> Students { get; set; } = new();
        [JsonIgnore]

        public virtual Unite Unite { get; set; }
        //[JsonIgnore]
        //public virtual List<StudentAssignment> StudentAssignments { get; set; } = new();

    }
}
