﻿using System;
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
    public class PartnersController : ControllerBase
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
