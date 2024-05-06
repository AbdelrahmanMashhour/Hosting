using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Admin:User
    {
        // public virtual List<Assignment> Assignments { get; set; } = new ();

        [JsonIgnore]
        public virtual List<Course> Courses { get; set; } = new ();
        

    }
}
