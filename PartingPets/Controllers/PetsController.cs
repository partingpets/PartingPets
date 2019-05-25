using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        readonly PetRepository _petRepository;

        public PetsController()
        {
            _petRepository = new PetRepository();
        }

        [HttpGet]
        public ActionResult GetAllPets()
        {
            return Ok(_petRepository.GetAllPets());
        }
    }
}