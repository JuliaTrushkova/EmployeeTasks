using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasksLib.App_Code.DomainModel
{
    /// <summary>
    /// Модель персоны
    /// </summary>
    [Table("Person")]
    public class Person
	{
        public Person()
        {
        }

        /// <summary>
        /// Идентификатор персоны
        /// </summary>
        public int id_Person { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return LastName + " " + FirstName;
        }
    }
}