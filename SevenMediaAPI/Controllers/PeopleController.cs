using Microsoft.AspNetCore.Mvc;
using SevenMediaAPI.Model;
using SevenMediaAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenMediaAPI.Controllers
{
    [Route("sample")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private ITPAPIClientService sampleReadService { get; set; }

        public PeopleController(ITPAPIClientService apiService)
        {
            this.sampleReadService = apiService;
        }

        [Route("testapi")]
        public async Task<string> testAPI()
        {
            return await Task.FromResult("API Ok");
        }

        [Route("people/{id}")]
        public async Task<People> GetPeopleById(int id)
        {
            var people = await sampleReadService.ReadSample();

            if (people != null)
            {
                var findPeople = people.Where(p => p.Id == id).FirstOrDefault();
                return findPeople;
            }
            else
            {
                return null;
            }
        }

        [Route("getall")]
        public async Task<List<People>> GetAll()
        {
            var people = await sampleReadService.ReadSample();

            return people;
        }

        [Route("allfirstnames")]
        public async Task<List<string>> GetAllFirstNames()
        {
            var people = await sampleReadService.ReadSample();

            var returnNames = people.Select(p => p.First).ToList();
            return returnNames;
        }

        [Route("gendersummary")]
        public async Task<List<GenderSummary>> GetGenderSummary()
        {
            GenderSummary genderAge;
            var people = await sampleReadService.ReadSample();

            var ageSummary = new List<GenderSummary>();

            if (people != null)
            {
                foreach(var person in people)
                {
                    genderAge = ageSummary.Where(i => i.Age == person.Age).FirstOrDefault();

                    if (genderAge == null)
                    {
                        genderAge = new GenderSummary()
                        {
                            Age = person.Age
                        };

                        ageSummary.Add(genderAge);
                    }

                    genderAge.FemaleCount += (person.Gender == "F" ? 1 : 0);
                    genderAge.MaleCount += (person.Gender == "M" ? 1 : 0);
                    genderAge.OtherCount += (!person.Gender.Any(g => g.ToString() == "M" || g.ToString() == "F") ? 1 : 0);
                }
            }

            return ageSummary;
        }
    }
}
