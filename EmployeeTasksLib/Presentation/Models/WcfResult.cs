using System.Runtime.Serialization;

namespace EmployeeTasksLib.Presentation.Models
{
    [DataContract]
    public class WcfResult<T>
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public T Data { get; set; }
    }
}
