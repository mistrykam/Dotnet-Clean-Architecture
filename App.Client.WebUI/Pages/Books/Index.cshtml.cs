using App.Domain.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediatR;

        public IEnumerable<BookViewModel> BookViewModelList { get; set; }

        public IndexModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public async Task OnGet()
        {
            BookViewModelList = await _mediatR.Send(new GetAllBooksQuery());
        }
    }
}
