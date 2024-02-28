using AutoMapper;
using FluentResults;
using MyFlix.Data.Dtos;
using MyFlix.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyFlix.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MyFlix.Services

{
    public class CategoriaService
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork context;


        public CategoriaService(IMapper map, UnitOfWork context)
        {
            this.mapper = map;
            this.context = context;

        }

        public Result GetCategorias(int pageNumber, int pageSize)
        {
            Result result;

            try
            {
                var tarefa = context.CategoriaRepository.Get(pageNumber, pageSize);


                result = new Result().WithSuccess(new Success("Sucesso!")
                        .WithMetadata("data", mapper.Map<List<ReadCategoriaDto>>(tarefa)));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }



        }
        public Result AddVideos(CreateCategoriaDto categoria)
        {
            Result result;
            try
            {
                var Dto = mapper.Map<Categoria>(categoria);
                context.CategoriaRepository.Add(Dto);
                context.Commit();

                result = new Result().WithSuccess("Categoria Criada");
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }

        public Result SearchCategoria(int id)
        {
            Result result;
            try
            {
                var categorias = context.CategoriaRepository.GetAll();
                var item = categorias.FirstOrDefault(categoria => categoria.Id == id);

                if (item != null)
                {
                    result = new Result().WithSuccess(new Success("Sucesso")
                        .WithMetadata("Item", mapper.Map<ReadCategoriaDto>(item)));
                    return result;
                }
                result = new Result().WithError(("Categoria nao encontrada"));
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }



        public Result AtualizaVideo(int id, UpdateCategoriaDto update)
        {
            Result result;
            try
            {
                var categorias = context.CategoriaRepository.GetAll();

                var item = categorias.FirstOrDefault(categoria => categoria.Id == id);

                if (item == null)
                {
                    result = new Result().WithError("Erro ao atualizar Video");
                    return result;
                }

                mapper.Map(update, item);
                context.Commit();

                result = new Result().WithSuccess(new Success("Sucesso")
                      .WithMetadata("Video modificado:", mapper.Map<ReadCategoriaDto>(update)));
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }

        }



        public Result AtualizaParcial(int id, JsonPatchDocument<UpdateCategoriaDto> patch)
        {
            Result result;

            try
            {
                var categorias = context.CategoriaRepository.GetAll();

                var item = categorias.FirstOrDefault(item => item.Id == id);
                if (item == null)
                {
                    result = new Result().WithError("Falha ao editar categoria: Categoria não encontrada");
                    return result;
                }


                var categoria = mapper.Map<UpdateCategoriaDto>(item);

                patch.ApplyTo(categoria);


                mapper.Map(categoria, item);
                context.Commit();

                result = new Result().WithSuccess(new Success("Sucesso")
                    .WithMetadata("Categoria Modificada:", mapper.Map<ReadCategoriaDto>(item)));
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;

            }

        }


        public Result Delete(int id)
        {
            Result result;
            try
            {
                var categorias = context.CategoriaRepository.GetAll();
                var item = categorias.FirstOrDefault(categoria => categoria.Id == id);
                if (item == null)
                {
                    result = new Result().WithError("Erro ao encontrar Categoria");
                    return result;
                }

                context.CategoriaRepository.Delete(item);
                context.Commit();
                result = new Result().WithSuccess("Sucesso");
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }
        public Result GetVideoAgrupado(int id)
        {
            Result result;
            try
            {
                var categorias = context.CategoriaRepository.GetAll();
                var item = categorias.FirstOrDefault(cat => cat.Id == id);

                if(item == null)
                {
                    result = new Result().WithError("Categoria nao encontrada");
                    return result;
                }
                var videos = item.Videos?.ToList();

                var readVideos = mapper.Map<List<ReadVideoDto>>(videos);

                result = new Result().WithSuccess(new Success("Sucesso").WithMetadata("Videos", readVideos));
                return result;
            }catch(Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }
    }
}




