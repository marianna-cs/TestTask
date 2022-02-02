using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestTask.Controllers.Entities;
using TestTask.Data;
using TestTask.Repo;
using TestTask.Service;


namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        IncidentService _incidentService;
        AccountService _accountService;
        ContactService _contactService;

        public IncidentController(BaseContext baseContext)
        {
            _accountService = new AccountService(baseContext);
            _contactService = new ContactService(baseContext);
            _incidentService = new IncidentService(baseContext);
        }

        /// <summary>
        /// Get all incidents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Incident), 200)]
        public IActionResult Get() 
        {
            var incident = _incidentService.GetAllIncidentsWithEntities();
            return Ok(incident);
        }

        /// <summary>
        /// Create incident
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(IncidentCreationRequest request)
        {
            if (!_accountService.UniquenessVerification(request.AccountName)
                && !_contactService.UniquenessVerification(request.Email))
            {
                return BadRequest();
            }
            Incident incident = new Incident()
            {
                Description = request.IncidentDescription
            };
            Account account = new Account()
            {
                Name = request.AccountName
            };
            Contact contact = new Contact()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            _incidentService.BuildIncident(incident, account);
            _accountService.BuildAccount(account, contact);

            _contactService.Add(contact);
            _accountService.Add(account);
            _incidentService.Add(incident);

            _contactService.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Update incident
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(IncidentCreationRequest request)
        {
            if (_accountService.UniquenessVerification(request.AccountName))
            {
                return NotFound();
            }

            Account account = _accountService.GetAccountByName(request.AccountName);

            _accountService.Update(account);
            _accountService.SaveChanges();

            var entitiesProducer = new EntitiesProducer();

            Incident incident = new Incident()
            {
                IncidentName = account.IncidentName,
                Description = request.IncidentDescription
            };

            _incidentService.Update(incident);
            _incidentService.SaveChanges();

            if (!_contactService.UniquenessVerification(request.Email))
            {
                Contact contact = _contactService.GetContactByEmail(request.Email);
                contact = entitiesProducer.UpdateContactFromRequest(contact, account, request);
                _contactService.Update(contact);
                _contactService.SaveChanges();
            }
            else
            if(_contactService.UniquenessVerification(request.Email))
            {
                Contact contact = entitiesProducer.CreateContactFromRequest(account,request);
                _contactService.Add(contact);
                _contactService.SaveChanges();
            }

            return Ok();
        }
    }
    public class IncidentCreationRequest
    {
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string IncidentDescription { get; set; }

    }
}
