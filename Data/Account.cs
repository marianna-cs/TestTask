using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Data
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Incident")]
        public string IncidentName { get; set; }
        public Incident? Incident { get; set; }
        public List<Contact>? Contacts { get; set; }
    }
}
