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
    public class Categories_IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Categories_IndexModel(ApplicationDbContext context) => _context = context;


        public List<Category> Categories { get; set; }


        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }
    }
}
