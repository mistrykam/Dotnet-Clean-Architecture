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
    public class GetBookByIdQuery
    {
        /// <summary>
        /// Query Parameters
        /// </summary>
        public class Query : IRequest<BookViewModel>
        {
            public int? Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(m => m.Id).NotNull();
            }
        }

        /// <summary>
        /// Handler the request
        /// </summary>
        public class Handler :  IRequestHandler<Query, BookViewModel> 
        {
            private readonly IAppDataContext _db;
            private readonly IMapper _mapper;

            public Handler(IAppDataContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<BookViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Books.Where(i => i.BookId == request.Id)
                                      .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                                      .SingleOrDefaultAsync();
            }
        }
    }
}