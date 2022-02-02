using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Data
{
    public class Incident
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IncidentName { get; set; }
        public string Description { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
