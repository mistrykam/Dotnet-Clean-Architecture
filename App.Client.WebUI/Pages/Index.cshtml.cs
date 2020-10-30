using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using App.Infrastructure.Application.Features.Books;

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

        public IndexModel(IMediator mediatR)
        {            
            _mediatR = mediatR;
        }

        public async Task OnGet()
        {
            var x = await _mediatR.Send(new Details.Query() { Id = 1 });
        }
    }
}
