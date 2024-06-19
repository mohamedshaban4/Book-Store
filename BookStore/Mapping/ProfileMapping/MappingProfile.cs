using AutoMapper;
using BookStore.BLL.DTOs;
using BookStore.DAL.Entities;

namespace BookStore.PL.Mapping.ProfileMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                 .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<BookDto, BookUpdateDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ReverseMap();




        }
    }
}
