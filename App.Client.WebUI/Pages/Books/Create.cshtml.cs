using App.Domain.Application.Features.Books.Commands;
using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateBookCommand CreateBookCommand { get; set; }

        public SelectList BookFormatOptions { get; set; }

        private readonly IMediator _mediatR;

        public CreateModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public void PopulationOptions()
        {
            BookFormatOptions = new SelectList(Enumeration.FindAll<BookFormatType>(),
                                               nameof(BookFormatType.Value),
                                               nameof(BookFormatType.DisplayName));
        }

        public IActionResult OnGet()
        {
            PopulationOptions();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _mediatR.Send(CreateBookCommand);

            return RedirectToPage("./Index");
        }
    }
}