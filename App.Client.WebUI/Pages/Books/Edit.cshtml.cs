using App.Domain.Application.Features.Books.Commands;
using App.Domain.Application.Features.Books.Queries;
using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediatR;

        [BindProperty]
        public UpdateBookCommand UpdateBookCommand { get; set; }

        public SelectList BookFormatOptions { get; set; }

        public EditModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public void PopulationOptions()
        {
            BookFormatOptions = new SelectList(Enumeration.FindAll<BookFormatType>(), 
                                               nameof(BookFormatType.Value), 
                                               nameof(BookFormatType.DisplayName), 
                                               UpdateBookCommand.BookFormat);
        }

        public async Task OnGet(int bookId)
        {
            BookViewModel BookViewModel = await _mediatR.Send(new GetBookByIdQuery() { BookId = bookId });

            UpdateBookCommand = new UpdateBookCommand()
                                {
                                    BookId = BookViewModel.BookId,
                                    Title = BookViewModel.Title,
                                    Author = BookViewModel.Author,
                                    BookFormat = BookViewModel.BookFormat,
                                    PublishedDate = BookViewModel.PublishedDate
                                };

            PopulationOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulationOptions();
                return Page();
            }

            await _mediatR.Send(UpdateBookCommand);

            return RedirectToPage("./Index");
        }
    }
}