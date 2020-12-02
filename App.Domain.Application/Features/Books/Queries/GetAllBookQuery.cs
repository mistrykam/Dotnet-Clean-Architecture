using App.Domain.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Queries
{
    /// <summary>
    /// Query Parameters
    /// </summary>
    public class GetAllBooksQuery : IRequest<IEnumerable<BookViewModel>>
    {
        // no parameters
    }

    /// <summary>
    /// Handler the request
    /// </summary>
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookViewModel>>
    {
        private readonly IAppDataContext _db;
        private readonly IMapper _mapper;

        public GetAllBooksHandler(IAppDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookViewModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            /*
            return await (from book in _db.Books.AsNoTracking()
                          select new BookViewModel
                          {
                              BookId = book.BookId,
                              Title = book.Title,
                              Author = book.Author,
                              BookFormat = book.BookFormat,
                              PublishedDate = book.PublishedDate,
                              LikeCount = book.LikeCount,
                              DislikeCount = book.DislikeCount
                          }).ToListAsync();
            */

            return await _db.Books.AsNoTracking().ProjectTo<BookViewModel>(_mapper.ConfigurationProvider).ToListAsync();            
        }
    }
}