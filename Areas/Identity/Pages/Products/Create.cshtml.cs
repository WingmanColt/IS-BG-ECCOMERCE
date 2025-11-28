// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Identity.Pages.Products
{
    public class ProductsCreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty] public Product Product { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public ProductsCreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public IActionResult OnPost()
        {
            _db.Products.Add(Product);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
