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
        readonly CreatePetRequestValidator _validator;

        public PetsController()
        {
            _petRepository = new PetRepository();
            _validator = new CreatePetRequestValidator();
        }

        [HttpGet]
        public ActionResult GetAllPets()
        {
            return Ok(_petRepository.GetAllPets());
        }

        [HttpPost]
        public ActionResult AddPet(CreatePetRequest createRequest)
        {
            if (_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "pets must have a name, user Id, breed, date of birth, and date of death." });
            }
            var newPet = _petRepository.AddPet(createRequest.Name, createRequest.UserId, createRequest.Breed, createRequest.DateOfBirth, createRequest.DateOfDeath);

            return Created($"api/pets/{newPet.Id}", newPet);
        }

        public class CreatePetRequestValidator
        {
            public bool Validate(CreatePetRequest requestToValidate)
            {
                return string.IsNullOrEmpty(requestToValidate.Name)
                        || string.IsNullOrEmpty(requestToValidate.Breed)
                        || string.IsNullOrEmpty(requestToValidate.DateOfBirth.ToString());
            }
        }
    }
}