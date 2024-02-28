using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFlix.Data.Context;
using MyFlix.Data.Dtos;
using MyFlix.Models;
using MyFlix.Services;


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
        private UnitOfService _service;

        public CategoriaController(MyFlixContext context, IMapper mapper, UnitOfService service)
        {
            this._context = context;
            this._mapper = mapper;
            this._service = service;

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
            var result= _service.CategoriaService.GetVideoAgrupado(id);

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

    }

}


           

