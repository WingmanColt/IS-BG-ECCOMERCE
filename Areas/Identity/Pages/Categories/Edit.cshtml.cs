// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Identity.Pages.Categories
{

    public class Categories_EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Categories_EditModel(ApplicationDbContext context) => _context = context;


        [BindProperty]
        public Category Category { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _context.Categories.FindAsync(id);
            if (Category == null) return RedirectToPage("Index");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
