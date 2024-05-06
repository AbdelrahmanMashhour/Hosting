//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public  class StudentPhones
    {
        public int StudentId { get; set; }
        public string Phone { get; set; }
        public string DadPhone { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
    }
}
