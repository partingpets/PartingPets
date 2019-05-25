using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {

        readonly PartnersRepository _partnersRepository;

        public PartnersController()
        {
            _partnersRepository = new PartnersRepository();
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
        public void Post([FromBody] string value)
        {
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
