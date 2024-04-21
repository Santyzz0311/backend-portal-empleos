using backend_portal_empleos.Data;
using backend_portal_empleos.DTOs;
using backend_portal_empleos.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_portal_empleos.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesGetDto>>> GetAllCaregories()
        {
            var categories = await _context.Categories
                .Select(category => new CategoriesGetDto
                {
                    Id = category.Id,
                    Name = category.Name,
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}
