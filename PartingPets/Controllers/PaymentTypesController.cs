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

        [HttpPost]
        public ActionResult AddPaymentType(CreatePaymentTypeRequest createPaymentType)
        {
            if (!_validator.Validate(createPaymentType))
            {
                return BadRequest(new { error = "Payment Information is either not valid or not complete." });
            }
            var newPaymentType = _paymentRepository.addPaymentType(createPaymentType);

            return Created($"api/paymentTypes/{newPaymentType.Id}", newPaymentType);
        }


    }
}