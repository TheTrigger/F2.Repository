using AutoMapper;
using F2.Repository.Demo.Mapper.Dto;
using F2.Repository.Demo.Models;
using System;
using System.Linq;

namespace F2.Repository.Demo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Implicit time to long
            CreateMap<long, TimeSpan>().ConstructUsing(x => TimeSpan.FromTicks(x));
            CreateMap<TimeSpan, long>().ConstructUsing(x => x.Ticks);

            CreateMap<Book, BookDto>();
        }
    }
}