using App.Domain.Application.Features.Books.Commands;
using App.Domain.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;


namespace App.Client.WebUI.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediatR;

        public CreateModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateBookCommand Book { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _mediatR.Send(Book);

            return RedirectToPage("./Index");
        }
    }
}
