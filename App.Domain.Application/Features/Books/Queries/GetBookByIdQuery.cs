using App.Domain.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Queries
{
    /// <summary>
    /// Query Parameters
    /// </summary>
    public class GetBookByIdQuery : IRequest<BookViewModel>
    {
        public int? BookId { get; set; }
    }

    public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdValidator()
        {
            RuleFor(m => m.BookId).NotNull();
        }
    }

    /// <summary>
    /// Handler the request
    /// </summary>
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookViewModel>
    {
        private readonly IAppDataContext _db;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IAppDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books.AsNoTracking()
                                  .Where(i => i.BookId == request.BookId)
                                  .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                                  .SingleOrDefaultAsync();
        }
    }    
}