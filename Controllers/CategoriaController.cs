using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoriaController : ControllerBase
    {
        private MyFlixContext _context;
        private IMapper _mapper;

        public CategoriaController(MyFlixContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCategorias([FromBody] CreateCategoriaDto categoriaDto)
        {
            try
            {
                Categoria cat = _mapper.Map<Categoria>(categoriaDto);

                _context.Categorias.Add(cat);
                _context.SaveChanges();
                return Ok(cat);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet]
        [Authorize]
        public IEnumerable<ReadCategoriaDto> GetCategorias()
        {

            return _mapper.Map<List<ReadCategoriaDto>>(_context.Categorias.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoria(int Id)
        {
            try
            {
                var cat = _context.Categorias.FirstOrDefault(cat => cat.Id == Id);

                if (cat == null)
                {
                    return BadRequest("Categoria nao encontrado");
                }
                var readDto = _mapper.Map<ReadCategoriaDto>(cat);
                return Ok(readDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/videos")]
        public IActionResult GetVideoAgrupado(int id)
        {
            try
            {
                var cat = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
                if (cat == null)
                {
                    return NotFound("Categoria nao encontrada");
                }
                var videos= cat.Videos.ToList();

                if (videos==null)
                {
                    return Ok("Esta categoria nao possui videos");
                }
               

                    

                return Ok(_mapper.Map<List<ReadVideoDto>>(cat.Videos.ToList()));


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

    }

}


           

