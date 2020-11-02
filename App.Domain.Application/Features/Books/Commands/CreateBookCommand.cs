using App.Domain.Entities;
using App.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime? PublishedDate { get; set; }
    }

    public class CreateBookHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IAppDataContext _db;

        public CreateBookHandler(IAppDataContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = request.PublishedDate == null
                                ? Book.CreateBook(request.Title, request.Author)
                                : Book.CreateBook(request.Title, request.Author, ((DateTime)request.PublishedDate).Year, 
                                                                                 ((DateTime)request.PublishedDate).Month, 
                                                                                 ((DateTime)request.PublishedDate).Day);

            _db.Books.Add(book);

            await _db.SaveChangesAsync(cancellationToken);

            return book.BookId;
        }
    }
}