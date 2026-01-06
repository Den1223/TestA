
namespace TestA.Models
{
    class CodeExecutionRequest
    {
        public string source { get; set; }
        public string lang { get; set; }
        public int time_limit { get; set; }
        public int memory_limit { get; set; }
    }
}
