using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyFlix.Data.Context;
using MyFlix.Data.Dtos;
using MyFlix.Services;

namespace MyFlix.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    public class VideosController: ControllerBase
    {
        private MyFlixContext _context;
        private IMapper _mapper;
        private UnitOfService _service;
        


        public VideosController(MyFlixContext context, IMapper mapper, UnitOfService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;

        }
        [HttpPost]
        public IActionResult AddVideo([FromBody] CreateVideoDto videoDto)
        { 

           var result= _service.VideoService.AddVideos(videoDto);

            if (result.IsSuccess)
            { 
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());

        }

        [HttpGet]
    
        public ActionResult GetVideos([FromQuery]int page=1)
        {
            var result = _service.VideoService.GetVideos(page,5 );

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
            
        }

        
        
        [HttpGet("{id}")]
        public IActionResult GetVideo(int id)
        {
           var result= _service.VideoService.SearchVideo(id);

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
            
        }

        [HttpGet("Gratuitos")]
        [AllowAnonymous]
      
        public ActionResult GetVideosGratuitos()
        {
            

            var result= _service.VideoService.GetGratuitos();
            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public ActionResult AtualizaVideo(int id, [FromBody] UpdateVideoDto videoDto)
        {   
            var result=  _service.VideoService.AtualizaVideo(id,videoDto);
            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
            

        }

        [HttpPatch("{id}")]
        public IActionResult AtulizaParcial(int id, JsonPatchDocument<UpdateVideoDto> patch) 
        {
            var result= _service.VideoService.AtualizaParcial(id,patch);
            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete("{id}")]
        public IActionResult deleteVideo(int id)
        {
           var result = _service.VideoService.Delete(id);  
            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
            
        }
         
        
        }
    }