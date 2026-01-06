using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestA.Models
{
    class CodeExecutionResult
    {
        public string status { get; set; }
        public Result run_status { get; set; }

        public class Result
        {
            public string output { get; set; }
            public string stderr { get; set; }
            public string status_detail { get; set; }
        }
    }
}
