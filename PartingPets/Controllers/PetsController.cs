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
    public class PetsController : SecureControllerBase
    {
        readonly PetRepository _petRepository;
        readonly PetRequestValidator _validator;

        public PetsController()
        {
            _petRepository = new PetRepository();
            _validator = new PetRequestValidator();
        }

        [HttpGet]
        public ActionResult GetAllPets()
        {
            return Ok(_petRepository.GetAllPets());
        }

        [HttpGet("my-pets/{id}")]
        public ActionResult GetPetsById(int id)
        {
            return Ok(_petRepository.GetMyPets(id));
        }

        [HttpGet("{id}")]
        public ActionResult GetSinglePet(int id)
        {
            return Ok(_petRepository.GetSinglePet(id));
        }


        [HttpPost]
        public ActionResult AddPet(CreatePetRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "pets must have a name, user Id, breed, date of birth, and date of death." });
            }
            var newPet = _petRepository.AddPet(createRequest);

            return Created($"api/pets/{newPet.Id}", newPet);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePet(int id, Pet petToUpdate)
        {
            if (id != petToUpdate.Id)
            {
                return BadRequest(new { Error = "There was an authentication error with your update." });
            }
            var updatedPet = _petRepository.UpdatePet(petToUpdate);

            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePet(int id)
        {
            _petRepository.DeletePet(id);

            return Ok("Your pet's data has been deleted :(");
        }
    }
}