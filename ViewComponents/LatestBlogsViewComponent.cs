﻿using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class LatestBlogsViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public LatestBlogsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _dbContext.Blogs
                .Where(s => !s.IsDeleted)
                .Include(x => x.Category)
                .OrderByDescending(t=>t.Date) 
                .Take(3)
                .ToListAsync();

            return View(blogs);
        }
    }
}
