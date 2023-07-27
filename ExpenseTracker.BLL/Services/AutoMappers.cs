using AutoMapper;
using ExpenseTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerLogicLayer.Services
{
    internal class AutoMappers
    {
        public static Mapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.BLCategory, Category>().ReverseMap();
                cfg.CreateMap<Models.BLTransaction, Transaction>().ReverseMap();
                cfg.CreateMap<Models.BLUser, User>().ReverseMap();
            });
           return new Mapper(config);
        }
    }
}
