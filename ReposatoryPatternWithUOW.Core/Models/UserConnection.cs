using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class UserConnection
    {
        public int StudentId { get; set; }
        public string ConnectionId { get; set; }
        public bool RequestedToVideo { get; set; }
   
        public virtual Student Student { get; set; }

    }
}
