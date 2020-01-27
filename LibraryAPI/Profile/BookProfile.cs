using AutoMapper;
using LibraryModels;

namespace LibraryAPI
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.Name)).ReverseMap();
        }
    }
}