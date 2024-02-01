using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFlix.Data;
using MyFlix.Data.Dtos;
using MyFlix.Models;

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


        public VideosController(MyFlixContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost]
        public IActionResult AddVideo([FromBody] CreateVideoDto videoDto)
        {
            if (videoDto.CategoriaId == 0)
            {
                videoDto.CategoriaId = 1;
            }

            Videos video = _mapper.Map<Videos>(videoDto);
            _context.Videos.Add(video);
            _context.SaveChanges();
            return Ok(video);
        }

        [HttpGet]
    
        public IEnumerable<ReadVideoDto> GetVideos([FromQuery]int page=0)
        {
            return _mapper.Map<List<ReadVideoDto>>(_context.Videos.Skip(page*3).Take(3).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetVideo(int id)
        {
            var video = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (video == null)
            {
                return BadRequest("video nao encontrado");
            }
            var readDto= _mapper.Map<ReadVideoDto>(video);
            return Ok(readDto);
            
        }

        [HttpGet("Gratuitos")]
        [AllowAnonymous]
      
        public IEnumerable<ReadVideoDto> GetVideosGratuitos()
        {
            return _mapper.Map<List<ReadVideoDto>>(_context.Videos.Take(2).ToList());
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaVideo(int id, [FromBody] UpdateVideoDto videoDto)
        {
            var video = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (video == null)
            {
                return NotFound("erro ao atualizar Video");

            }

          if( video.CategoriaId == 0){
                video.CategoriaId = 1;
          }

            var cat = _context.Categorias.FirstOrDefault(c => c.Id == videoDto.CategoriaId);
            if (cat == null)
            {
                return NotFound("Erro ao atualizar Video");
            }
          


            _mapper.Map(videoDto, video);   
     
            _context.SaveChanges();



            return Ok(_mapper.Map<ReadVideoDto>(video));
        }

        [HttpPatch("{id}")]
        public IActionResult AtulizaParcial(int id, JsonPatchDocument<UpdateVideoDto> patch) 
        {
            var video= _context.Videos.FirstOrDefault(video => video.Id==id);
            if (video == null) return NotFound();

            var videoAtt = _mapper.Map<UpdateVideoDto>(video);

            patch.ApplyTo(videoAtt,ModelState);

            if (!TryValidateModel(videoAtt)) return ValidationProblem(ModelState);



            _mapper.Map(videoAtt, video);
            var cat = _context.Categorias.FirstOrDefault(c => c.Id == video.CategoriaId);
            if (cat == null)
            {
                return NotFound("Erro ao atualizar Video");
            }

            _context.SaveChanges();
            return Ok(_mapper.Map<ReadVideoDto>(video));
        }

        [HttpDelete("{id}")]
        public IActionResult deleteVideo(int id)
        {
            var video= _context.Videos.FirstOrDefault(v => v.Id==id);
            if (video == null) return BadRequest("Falha ao deletar ");

            _context.Videos.Remove(video);
            _context.SaveChanges();
            return Ok("Video deletado");
        }
         
        
        }
    }