using DebtTracker.Entities;
using DebtTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DebtTracker.Controllers
{
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IRepository _repository;

        public PersonController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetPersons()
        {
            var persons = _repository.GetPersons();
            return Ok(persons);
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult GetPerson(Guid id)
        {
            if (!_repository.PersonExists(id))
            {
                return NotFound();
            }

            var person = _repository.GetPerson(id);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody]Person person)
        {
            _repository.AddPerson(person);

            if (!_repository.Save())
            {
                return StatusCode(500, $"Creating person with id {person.Id} failed on save.");
            }

            return CreatedAtRoute("GetPerson",
                new { id = person.Id },
                person);
        }
    }
}
