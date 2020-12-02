using App.Domain.Entities;
using App.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Application.Features.Books.Commands
{
    public class DislikeBookCommand : IRequest<int>
    {
        public int BookId { get; set; }
    }

    public class DislikeBookHandler : IRequestHandler<DislikeBookCommand, int>
    {
        private readonly IAppDataContext _db;

        public DislikeBookHandler(IAppDataContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(DislikeBookCommand request, CancellationToken cancellationToken)
        {
            Book book = await _db.Books.Where(p => p.BookId == request.BookId).SingleOrDefaultAsync();

            if (book != null)
            {
                book.DisLike();
                await _db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException($"Book with id {request.BookId} not found.");
            }

            return book.DislikeCount;
        }
    }
}