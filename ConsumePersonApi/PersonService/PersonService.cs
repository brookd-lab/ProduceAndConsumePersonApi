﻿using Newtonsoft.Json;
using PersonApiDAL.Models;

public class PersonService
{
    HttpClient _client;
    public string? _url { get; set; }

    public PersonService()
    {   
        var handler = new HttpClientHandler();
        _client = new HttpClient(handler);
    }

    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        var url = $@"{_url}/GetAllPeople";
        var data = await _client.GetStringAsync(url);
        var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(data)!;
        return persons;
    }

    public async Task<Person> GetPerson(int? id)
    {
        var url = $@"{_url}/GetPerson/{id}";
        var data = await _client.GetStringAsync(url);
        var person = JsonConvert.DeserializeObject<Person>(data)!;
        return person;
    }

    public async Task CreatePerson(Person person)
    {
        var url = $@"{_url}/CreatePerson";
        await _client.PostAsJsonAsync<Person>(url, person);
    }

    public async Task UpdatePerson(Person person)
    {
        var url = $@"{_url}/UpdatePerson";
        await _client.PutAsJsonAsync<Person>(url, person);
    }

    public async Task DeletePerson(int id)
    {
        var url = $@"{_url}/DeletePerson/{id}";
        await _client.DeleteAsync(url);
    }
}


