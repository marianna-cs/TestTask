using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Repo;

namespace TestTask.Service
{
    public class IncidentService : Repository<Incident>
    {
        public IncidentService(BaseContext context) : base(context)
        {
        }
        public void BuildIncident(Incident incident, Account account)
        {
            incident.Accounts = new List<Account>() {account};
        }
        public Incident GetByName(string incidentName)
        {
            return _context.Incidents.Find(incidentName);
            
        }
        public IEnumerable<Incident> GetAllIncidentsWithEntities()
        {
            var incidencs =  _context.Incidents.Include(a=>a.Accounts).ToList();
                             _context.Accounts.Include(c=>c.Contacts).ToList(); 
            return incidencs;
        }
        public bool IncidentExists(Incident incident)
        {
            return _context.Incidents.Any(i => i.IncidentName == incident.IncidentName);
        }
    }
}
