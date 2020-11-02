using App.Domain.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
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
        /// Return Book View Model
        /// </summary>
        public class BookViewModel
        {
            public int BookId { get; set; }
            
            public string Title { get; set; }

            public string Author { get; set; }
            
            [DisplayName("Published Date")]
            
            public DateTime? PublishedDate { get; set; }
            [DisplayName("Like Count")]
            public int LikeCount { get; set; }

            [DisplayName("Dislike Count")]
            public int DislikeCount { get; set; }            
        }

        /// <summary>
        /// Handler the request
        /// </summary>
        public class Handler :  IRequestHandler<Query, BookViewModel> 
        {
            private readonly IAppDataContext _db;
            private readonly IMapper _mapper;

            public Handler(IAppDataContext db, IMapper _mapper)
            {
                _db = db;
            }

            public async Task<BookViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _db.Books.Where(i => i.BookId == request.Id)
                                      .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                                      .SingleOrDefaultAsync();

                if (result != null)
                {
                    return new BookViewModel()
                    {
                        BookId = result.BookId,
                        Author = result.Author,
                        DislikeCount = result.DislikeCount,
                        LikeCount = result.LikeCount,
                        PublishedDate = result.PublishedDate,
                        Title = result.Title
                    };
                }
                else
                    return new BookViewModel();
            }
        }
    }
}