using ConsumePersonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsumePersonApi.Controllers
{
    public class PersonsController : Controller
    {
        HttpClient _client;
        string _url;
        public PersonsController(IConfiguration config)
        {
            var handler = new HttpClientHandler();
            _client = new HttpClient(handler);
            _url = config["BaseUrl"]!;
        }

        public async Task<IActionResult> Index()
        {
            var url = $@"{_url}/GetAllPeople";
            var data = await _client.GetStringAsync(url);
            var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(data)!;
            return View(persons);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var url = $@"{_url}/GetPerson/{id}";
            var data = await _client.GetStringAsync(url);
            var person = JsonConvert.DeserializeObject<Person>(data)!;
            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            var url = $@"{_url}/CreatePerson";

            if (ModelState.IsValid)
            {
                await _client.PostAsJsonAsync<Person>(url, person);

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var url = $@"{_url}/GetPerson/{id}";

            // Consume API
            var person = JsonConvert.DeserializeObject<Person>(await _client.GetStringAsync(url));
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Person person)
        {
            var url = $@"{_url}/UpdatePerson";
            if (ModelState.IsValid)
            {
                try
                {
                    // Consume API
                    await _client.PutAsJsonAsync<Person>(url, person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var url = $@"{_url}/GetPerson/{id}";
            // Consume API
            var person = JsonConvert.DeserializeObject<Person>(await _client.GetStringAsync(url));

            return View(person);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var url = $@"{_url}/DeletePerson/{id}";
            await _client.DeleteAsync(url);

            return RedirectToAction(nameof(Index));
        }
    }
}
