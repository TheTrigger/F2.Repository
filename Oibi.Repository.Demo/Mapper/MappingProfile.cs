using AutoMapper;
using Oibi.Repository.Demo.Mapper.Dto;
using Oibi.Repository.Demo.Models;
using System;
using System.Linq;

namespace Oibi.Repository.Demo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Implicit time to long
            CreateMap<long, TimeSpan>().ConstructUsing(x => TimeSpan.FromTicks(x));
            CreateMap<TimeSpan, long>().ConstructUsing(x => x.Ticks);

            // map many-to-many to one-to-many
            //CreateMap<Author, AuthorDto>()
            //    .ForMember(dto => dto.Books, opt => opt.MapFrom(x => x.BookAuthors.Select(y => y.Book)));

            CreateMap<Book, BookDto>();
        }
    }
}