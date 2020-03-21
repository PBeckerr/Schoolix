using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaApi.Dtos;
using CoronaApi.MediatR.SchoolYear.Queries;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    public class SchoolYearController : BaseV1ApiController
    {
        private UserManager<ApplicationUser> _userManager;

        public SchoolYearController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<List<SchoolYearDto>> GetAll([FromQuery]GetAllSchoolYearsQuery query)
        {
            query.User = await _userManager.GetUserAsync(User);
            
            return await Mediator.Send(query);
        }
    }
}
