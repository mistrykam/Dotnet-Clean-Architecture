using App.Domain.Entities;
using App.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Commands
{
    public class CreateBookCommand
    {
        public class Command : IRequest<int>
        {
            public string Title { get; set; }

            public string Author { get; set; }

            public DateTime? PublishedDate { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IAppDataContext _db;            

            public Handler(IAppDataContext db)
            {
                _db = db;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = request.PublishedDate == null 
                                    ? Book.CreateBook(request.Title, request.Author) 
                                    : Book.CreateBook(request.Title, request.Author, ((DateTime)request.PublishedDate).Year, ((DateTime)request.PublishedDate).Month, ((DateTime)request.PublishedDate).Day);

                _db.Books.Add(entity);

                await _db.SaveChangesAsync(cancellationToken);

                return entity.BookId;
            }
        }
    }
}
