﻿using App.Domain.Application.Features.Books.Commands;
using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Client.WebUI.Pages.Books
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateBookCommand Book { get; set; }

        public IEnumerable<BookFormatType> BookFormatOptions { get; set; }

        private readonly IMediator _mediatR;

        public CreateModel(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public void PopulationOptions()
        {
            BookFormatOptions = Enumeration.FindAll<BookFormatType>();
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

            Book.BookFormat = 0;

            await _mediatR.Send(Book);

            return RedirectToPage("./Index");
        }
    }
}