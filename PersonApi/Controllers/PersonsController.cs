using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Data;
using PersonApi.Models;
using PersonApi.Services;
using System;

namespace PersonApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet] public async Task<JsonResult> GetAllPeople()
        {
            var people = await _personService.GetAllPeople();
            if (people.Count() == 0)
            {
                return new JsonResult(null);
            }
            else
            {
                return new JsonResult(people);
            }
        }

        [HttpGet("{id}")] public async Task<JsonResult> GetPerson(int id)
        {
            Person? person = await _personService.GetPerson(id);
            if (person == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(person); 
        }

        [HttpPost]
        public async Task<JsonResult> CreatePerson(Person person)
        {
            await _personService.CreatePerson(person);
            return new JsonResult(person);
        }

        [HttpPut]
        public async Task<JsonResult> UpdatePerson(Person person)
        {
            Person? findPerson = await _personService.GetPerson(person.Id);
            if (findPerson == null)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                findPerson.Name = person.Name;
                findPerson.Age = person.Age;
            }
            await _personService.UpdatePerson(findPerson);
            return new JsonResult(findPerson);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeletePerson(int id)
        {
            Person? person = await _personService.GetPerson(id);
            if (person == null)
            {
                return new JsonResult(NotFound());
            }
            await _personService.DeletePerson(person);
            return new JsonResult($"Person Deleted: {person.Id}:{person.Name}");
        }
    }
}
