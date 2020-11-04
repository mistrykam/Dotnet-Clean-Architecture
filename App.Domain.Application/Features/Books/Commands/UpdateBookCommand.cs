using App.Domain.Application.Common;
using App.Domain.Entities;
using App.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest<int>
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [DisplayName("Published Date")]
        public DateTime? PublishedDate { get; set; }
    }

    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(m => m.Title).NotNull().MaximumLength(256);
            RuleFor(m => m.Author).NotNull().MaximumLength(256);
        }
    }

    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, int>
    {
        private readonly IAppDataContext _db;

        public UpdateBookHandler(IAppDataContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = await _db.Books.FindAsync(request.BookId);

            if (book == null)
            {
                throw new NotFoundException(nameof(UpdateBookCommand), request.BookId);
            }
            else
            {
                book.Update(request.Title, request.Author, request.PublishedDate);

                await _db.SaveChangesAsync(cancellationToken);

                return book.BookId;
            }
        }
    }
}