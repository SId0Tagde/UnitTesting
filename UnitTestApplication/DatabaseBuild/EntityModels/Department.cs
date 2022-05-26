using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employeemanagement.DatabaseBuild.EntityModels
{
    public class Department
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } 
        public string DepartmentName { get; set; } = null!;

    }
}
