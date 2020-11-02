using App.Domain.Application.Features.Books.Queries;
using App.Domain.Entities;
using AutoMapper;

namespace App.Domain.Application.Mappings
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// AutoMapper - maps from one class to another
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>();
        }
    }
}
