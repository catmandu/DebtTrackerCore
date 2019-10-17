using DebtTracker.Entities;
using DebtTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DebtTracker.Controllers
{
    [Route("api/persons/{personId}/debts")]
    public class DebtController : ControllerBase
    {
        private readonly IRepository _repository;

        public DebtController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetDebts(Guid personId)
        {
            if (!_repository.PersonExists(personId))
            {
                return NotFound();
            }

            var debts = _repository.GetDebts(personId);

            return Ok(debts);
        }

        [HttpGet("{id}", Name = "GetDebtForPerson")]
        public IActionResult GetDebtForPerson(Guid personId, Guid id)
        {
            if (!_repository.PersonExists(personId))
            {
                return NotFound();
            }

            var debts = _repository.GetDebtForPerson(personId, id);

            return Ok(debts);
        }

        [HttpPost]
        public IActionResult CreateDebtForPerson(Guid personId, [FromBody]Debt debt)
        {
            if (!_repository.PersonExists(personId))
            {
                return NotFound();
            }
            
            _repository.AddDebtForPerson(personId, debt);

            if (!_repository.Save())
            {
                return StatusCode(500, $"Creating debt for person with id {personId} failed on save.");
            }

            return CreatedAtRoute("GetDebtForPerson",
                new { personId = personId, id = debt.Id},
                debt);
        }

    }
}
