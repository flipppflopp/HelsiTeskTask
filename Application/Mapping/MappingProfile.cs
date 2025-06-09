using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateDTO>();

            CreateMap<MongoUser, User>();
            CreateMap<User, MongoUser>();


            CreateMap<TaskListDTO, TaskList>();
            CreateMap<TaskList, TaskListDTO>();

            CreateMap<TaskListCreateDTO, TaskList>();
            CreateMap<TaskList, TaskListCreateDTO>();

            CreateMap<MongoTaskList, TaskList>();
            CreateMap<TaskList, MongoTaskList>();
        }
    }
}
