using AutoMapper;
using LibraryModels;

namespace LibraryAPI
{
    public class CheckInOutHistoryProfile : Profile
    {
        public CheckInOutHistoryProfile()
        {
            //CreateMap<CheckInOutHistoryDTO, CheckInOutHistory>();
            CreateMap<CheckInOutHistory, CheckInOutHistoryDTO>()

                .ForMember(dest => dest.BookId,
                opt => opt.MapFrom(src => src.Book.Id))
                .ForMember(dest => dest.BorrowerId,
                opt => opt.MapFrom(src => src.Borrower.Id)).ReverseMap();
        }
    }
}