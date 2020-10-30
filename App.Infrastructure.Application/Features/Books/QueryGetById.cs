//using AutoMapper;
//using AutoMapper.QueryableExtensions;

using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using App.Infrastructure.DataAccess;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Infrastructure.Application.Features.Books
{
    public class Details
    {
        public class Query : IRequest<Model>
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

        public class Model
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime? PublishedDate { get; set; }
            public int LikeCount { get; set; }
            public int DislikeCount { get; set; }            
        }

        public class Handler :  IRequestHandler<Query, Model> 
        {
            private readonly AppDataContext _db;

            public Handler(AppDataContext db)
            {
                _db = db;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _db.Books.Where(i => i.BookId == request.Id).SingleOrDefaultAsync();

                if (result != null)
                {
                    return new Model()
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
                    return new Model();
            }
        }
    }
}