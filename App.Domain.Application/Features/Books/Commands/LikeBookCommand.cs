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
    public class LikeBookCommand : IRequest<int>
    {
        public int BookId { get; set; }
    }

    public class LikeBookHandler : IRequestHandler<LikeBookCommand, int>
    {
        private readonly IAppDataContext _db;

        public LikeBookHandler(IAppDataContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(LikeBookCommand request, CancellationToken cancellationToken)
        {
            Book book = await _db.Books.Where(p => p.BookId == request.BookId).SingleOrDefaultAsync();

            if (book != null)
            {
                book.Like();
                await _db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException($"Book with id {request.BookId} not found.");
            }

            return book.LikeCount;
        }
    }
}