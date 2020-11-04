using App.Domain.Application.Features.Books.Commands;
using App.Domain.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediatR;

        [BindProperty]
        public UpdateBookCommand UpdateBookCommand { get; set; }

        public EditModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public async Task OnGet(int bookId)
        {
            BookViewModel BookViewModel = await _mediatR.Send(new GetBookByIdQuery() { BookId = bookId });

            UpdateBookCommand = new UpdateBookCommand()
                                {
                                    BookId = BookViewModel.BookId,
                                    Title = BookViewModel.Title,
                                    Author = BookViewModel.Author,
                                    PublishedDate = BookViewModel.PublishedDate
                                };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _mediatR.Send(UpdateBookCommand);

            return RedirectToPage("./Index");
        }
    }
}