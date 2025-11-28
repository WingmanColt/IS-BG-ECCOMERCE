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
    public class ProductsEditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty] public Product Product { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public ProductsEditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Product = _db.Products.Find(id);
            CategoryList = _db.Categories
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public IActionResult OnPost()
        {
            _db.Products.Update(Product);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
