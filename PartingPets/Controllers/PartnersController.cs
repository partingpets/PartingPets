using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;
using PartingPets.Validators;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : SecureControllerBase 
    {

        readonly PartnersRepository _partnersRepository;
        readonly PartnerRequestValidator _validator;

        public PartnersController()
        {
            _partnersRepository = new PartnersRepository();
            _validator = new PartnerRequestValidator();
        }

        // GET: api/Partners
        [HttpGet]
        public ActionResult GetAllPartners()
        {
            var partners = _partnersRepository.GetAll();
            return Ok(partners);
        }

        // GET: api/Partners/5
        [HttpGet("{id}", Name = "GetPartnerById")]
        public ActionResult GetPartnerById(int id)
        {
            var partner = _partnersRepository.GetPartner(id);
            return Ok(partner);
        }

        // GET: api/Partners/Code/8uyuy6y
        [HttpGet("Code/{registrationCode}", Name = "GetPartnerByPartnerCode")]
        public ActionResult GetPartnerByRegistrationCode(string registrationCode)
        {
            var partner = _partnersRepository.GetPartnerCode(registrationCode);
            if(partner == null)
            {
                return NotFound();
            }
            return Ok(partner);
        }

        // POST: api/Partners
        [HttpPost]
        public ActionResult AddPartner(CreatePartnerRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "We need all the information to become a Parting Pets Partner" });
            }

            var newPartner = _partnersRepository.AddPartner(
                createRequest.Name,
                createRequest.Description,
                createRequest.Street,
                createRequest.City,
                createRequest.State,
                createRequest.Zipcode
                );

            return Created($"api/partners/{newPartner.Id}", newPartner);

        }

        // PUT: api/Partners/5
        [HttpPut("{id}")]
        public ActionResult updatePartner(int id, Partners partnerToUpdate)
        {
            if (id != partnerToUpdate.Id)
            {
                return BadRequest();
            }
            var partner = _partnersRepository.UpdatePartner(partnerToUpdate);
            return Ok(partner);
        }
        

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult DeletePartner(int id)
        {
            _partnersRepository.DeletePartner(id);

            return Ok("The partner was deleted");
        }
    }
}
