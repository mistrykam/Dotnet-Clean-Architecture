using App.Domain.Application.Features.Books.Queries;
using MediatR;
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

        public async Task OnGet(int id)
        {
            BookViewModel = await _mediatR.Send(new GetBookByIdQuery() { Id = id });
        }
    }
}
