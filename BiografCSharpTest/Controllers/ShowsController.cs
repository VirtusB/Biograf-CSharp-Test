using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _repo;
        private readonly IMapper _mapper;

        public ShowsController(IShowRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShow(int id) {
            var show = await _repo.GetShow(id);

            if (show != null) {
                return Ok(show);
            }

            return NoContent();
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetShows ([FromQuery]ShowParams showParams) {
            
            var shows = await _repo.GetShows(showParams);
            var showsToReturn = _mapper.Map<IEnumerable<ShowForListDto>>(shows);


            Response.AddPagination(shows.CurrentPage, showParams.PageSize, shows.TotalCount, shows.TotalPages);

            return Ok (showsToReturn);
        }
    }
}