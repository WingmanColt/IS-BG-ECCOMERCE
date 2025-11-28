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

namespace WebApplication1.Areas.Identity.Pages.Products
{
    public class ProductsDeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Product Product { get; set; }

        public ProductsDeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Product = _db.Products.Find(id);
        }

        public IActionResult OnPost(int id)
        {
            var p = _db.Products.Find(id);
            _db.Products.Remove(p);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
