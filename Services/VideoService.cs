using AutoMapper;
using FluentResults;
using MyFlix.Data.Dtos;
using MyFlix.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyFlix.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MyFlix.Services 
  
{
    public class VideoService
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork context;


        public VideoService(IMapper map, UnitOfWork context)
        {
            this.mapper = map;
            this.context = context;

        }

        public Result GetVideos(int pageNumber, int pageSize)
        {
            Result result;

            try
            {
                var tarefa = context.VideoRepository.Get(pageNumber, pageSize);


                result = new Result().WithSuccess(new Success("Sucesso!")
                        .WithMetadata("data", mapper.Map<List<ReadVideoDto>>(tarefa)));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }



        }
        public Result AddVideos(CreateVideoDto video)
        {
            Result result;
            if (video.CategoriaId == 0)
            {
                video.CategoriaId = 1;
            }
            try
            {
                var Dto = mapper.Map<Videos>(video);
                context.VideoRepository.Add(Dto);
                context.Commit();

                result = new Result().WithSuccess("Video Criado");
                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }

        public Result SearchVideo(int id)
        {
            Result result;
            try{
                var videos = context.VideoRepository.GetAll();
                var  item= videos.FirstOrDefault(video => video.Id == id);

                if(item != null)
                {
                    result = new Result().WithSuccess(new Success("Sucesso").WithMetadata("Item", mapper.Map<ReadVideoDto>(item)));
                    return result;
                }
                result = new Result().WithError(("Video nao encontrado"));
                return result;
            }catch (Exception ex)
            {
                result= new Result().WithError(ex.Message);
                return result;
            }
        }


        public Result GetGratuitos()
        {
            Result result;
            try
            {
                var gratuitos = context.VideoRepository.Get(1, 3);
                
                result = new Result().WithSuccess(new Success("Sucesso")
                    .WithMetadata("Videos Gratuitos", mapper.Map<List<ReadVideoDto>>(gratuitos)));

                return result;
            }catch(Exception ex)
            {
                result= new Result().WithError(ex.Message);
                return result;
            }
        }

        public Result AtualizaVideo(int id, UpdateVideoDto update)
        {
            Result result;
            try
            {
                var videos = context.VideoRepository.GetAll();

                var item = videos.FirstOrDefault(video => video.Id == id);

                if (item == null)
                {
                    result = new Result().WithError("Erro ao atualizar Video");
                    return result;
                }

                if (update.CategoriaId == 0)
                {
                    update.CategoriaId = 1;
                }

                var cats = context.CategoriaRepository.GetAll();
                var catEscolhida = videos.FirstOrDefault(video => video.Id == id);
                if (catEscolhida == null)
                {
                    result = new Result().WithError("Erro ao atualizar Video: Categoria nao existente.");
                    return result;
                }


                mapper.Map(update, item);
                context.Commit();
                
                result= new Result().WithSuccess(new Success("Sucesso")
                      .WithMetadata("Video modificado:", mapper.Map<ReadVideoDto>(update)));
                return result;
            }catch(Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }

        }



        public Result AtualizaParcial(int id, JsonPatchDocument<UpdateVideoDto> patch)
        {
            Result result;

            try
            {
                var videos = context.VideoRepository.GetAll();

                var item = videos.FirstOrDefault(item => item.Id == id);
                if (item == null)
                {
                    result = new Result().WithError("Falha ao editar video: Video não encontrado");
                    return result;
                }

                var video = mapper.Map<UpdateVideoDto>(item);
                patch.ApplyTo(video);

                var cats = context.CategoriaRepository.GetAll();
                var catItem = cats.FirstOrDefault(item => item.Id == video.CategoriaId);
                if (catItem == null)
                {
                    result = new Result().WithError("Falha ao editar video: Categoria nao encontrada");
                    return result;
                }

                mapper.Map(video, item);
                context.Commit();

                result = new Result().WithSuccess(new Success("Sucesso")
                    .WithMetadata("Video Modificado:", mapper.Map<ReadVideoDto>(item)));
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
                var videos = context.VideoRepository.GetAll();
                var item = videos.FirstOrDefault(v => v.Id == id);
                if (item == null)
                {
                    result = new Result().WithError("Erro ao encontrar Video");
                    return result;
                }

                context.VideoRepository.Delete(item);
                context.Commit();
                result = new Result().WithSuccess("Sucesso");
                return result;
            } catch (Exception ex)
            {
                result = new Result().WithError(ex.Message);
                return result;
            }
        }
    }
}




   