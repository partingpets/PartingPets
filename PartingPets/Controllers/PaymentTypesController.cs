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
    public class PaymentTypesController : ControllerBase
    {
        readonly PaymentRepository _paymentRepository;
        readonly PaymentTypeRequestValidator _validator;

        public PaymentTypesController()
        {
            _paymentRepository = new PaymentRepository();
            _validator = new PaymentTypeRequestValidator();
        }

        [HttpGet("user-pt/{id}")]
        public ActionResult GetPTByUser(int id)
        {
            return Ok(_paymentRepository.GetUserPT(id));
        }

        [HttpGet("{id}")]
        public ActionResult GetSinglePT(int id)
        {
            return Ok(_paymentRepository.GetSinglePT(id));
        }

        [HttpPost]
        public ActionResult AddPaymentType(CreatePaymentTypeRequest createPaymentType)
        {
            if (!_validator.Validate(createPaymentType))
            {
                return BadRequest(new { error = "Payment Information is either not valid or not complete." });
            }
            var newPaymentType = _paymentRepository.AddPaymentType(createPaymentType);

            return Created($"api/paymentTypes/{newPaymentType.Id}", newPaymentType);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePT(int id, PaymentType pTToUpdate)
        {
            if (id != pTToUpdate.Id)
            {
                return BadRequest(new { Error = "There was an authentication error with your update" });
            }
            var updatedPT = _paymentRepository.UpdatePT(pTToUpdate);

            return Ok(updatedPT);
        }

        [HttpPut("delete-pt/{id}")]
        public ActionResult DeletePT(int id)
        {
            _paymentRepository.DeletePT(id);

            return NoContent();
        }

    }
}