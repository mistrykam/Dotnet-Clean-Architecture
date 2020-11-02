using App.Domain.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Queries
{
    public class GetAllBooksQuery
    {
        /// <summary>
        /// Query Parameters
        /// </summary>
        public class Query : IRequest<IEnumerable<BookViewModel>>
        {
        }

        /// <summary>
        /// Handler the request
        /// </summary>
        public class Handler :  IRequestHandler<Query, IEnumerable<BookViewModel>> 
        {
            private readonly IAppDataContext _db;
            private readonly IMapper _mapper;

            public Handler(IAppDataContext db, IMapper _mapper)
            {
                _db = db;
            }

            public async Task<IEnumerable<BookViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Books.ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                                      .ToListAsync();
            }
        }
    }
}