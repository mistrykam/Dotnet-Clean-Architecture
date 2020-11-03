using App.Domain.Application.Features.Books.Commands;
using App.Domain.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediatR;

        public BookViewModel BookViewModel { get; set; }

        public DetailsModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public async Task OnGet(int bookId)
        {
            BookViewModel = await _mediatR.Send(new GetBookByIdQuery() { BookId = bookId });
        }

        public async Task<IActionResult> OnPostLikedAsync(int bookId)
        {
            await _mediatR.Send(new LikeBookCommand() { BookId = bookId });
            return RedirectToAction(nameof(DetailsModel), new { bookId = bookId });
        }

        public async Task<IActionResult> OnPostDislikedAsync(int bookId)
        {
            await _mediatR.Send(new DislikeBookCommand() { BookId = bookId });
            return RedirectToAction(nameof(DetailsModel), new { bookId = bookId });
        }
    }
}
