using App.Domain.Entities;
using App.Domain.Entities.Enum;
using App.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public BookFormatType BookFormat { get; set; }

        [DisplayName("Published Date")]
        public DateTime? PublishedDate { get; set; }
    }

    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(m => m.Title).NotNull().MaximumLength(256);
            RuleFor(m => m.Author).NotNull().MaximumLength(256);
        }
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
            Book book = Book.CreateBook(request.Title, request.Author, request.PublishedDate, request.BookFormat);

            _db.Books.Add(book);

            await _db.SaveChangesAsync(cancellationToken);

            return book.BookId;
        }
    }
}