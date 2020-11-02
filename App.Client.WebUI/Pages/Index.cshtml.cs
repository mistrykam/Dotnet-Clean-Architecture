﻿// using Microsoft.Extensions.Logging;
using App.Domain.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        /*
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        */

        private readonly IMediator _mediatR;

        public GetBookByIdQuery.BookViewModel BookViewModel { get; set; }

        public IndexModel(IMediator mediatR)
        {            
            _mediatR = mediatR;
        }

        public async Task OnGet()
        {
            BookViewModel = await _mediatR.Send(new GetBookByIdQuery.Query() { Id = 1 });
        }
    }
}
