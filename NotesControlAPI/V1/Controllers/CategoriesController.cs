using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesControlAPI.V1.DTOS;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("")]
        public ActionResult InsertCategory([FromBody] CategoryDTO category)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (category == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            CategoryModel categoryModel = _mapper.Map<CategoryDTO, CategoryModel>(category);

            var categoryId = _repository.InsertCategory(categoryModel, Convert.ToInt32(userId));

            return Created("", new { category_id = categoryId });
        }

        [Authorize]
        [HttpGet("{categoryID}")]
        public ActionResult GetCategoryById(int categoryID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (categoryID == 0)
                return BadRequest();

            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

           var category = _repository.GetCategoryById(categoryID, Convert.ToInt32(userId));
           CategoryDTO categoryDTO = _mapper.Map<CategoryModel, CategoryDTO>(category);

           return Ok(categoryDTO);
        }

        [Authorize]
        [HttpGet("")]
        public ActionResult GetCategoryByName(string name)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (name == "")
                return BadRequest();

            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var categories = _repository.GetCategoryByName(name, Convert.ToInt32(userId));
            List<CategoryDTO> categoriesDTO = _mapper.Map<List<CategoryModel>, List<CategoryDTO>>(categories);

            var count = categories.Count();

            return Ok(new { count = count, categories = categoriesDTO });
        }

        [Authorize]
        [HttpPut("{categoryID}/archives")]
        public ActionResult CategoryArchives()
        {
            return Ok();
        }

        [Authorize]
        [HttpPut("{categoryID}")]
        public ActionResult UpdateCategoryById([FromBody] CategoryDTO category, int categoryID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (categoryID == 0)
                return BadRequest();

            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var categoryReg = _repository.GetCategoryById(categoryID, Convert.ToInt32(userId));

            if (categoryReg == null)
                return NotFound(new { error = "Category Id not found to this User" });

            CategoryModel categoryModel = _mapper.Map<CategoryDTO, CategoryModel>(category);

            categoryModel.Category_Id = categoryReg.Category_Id;
            categoryModel.UserId = categoryReg.UserId;

            _repository.UpdateCategoryById(categoryModel);

            return Ok();
        }
    }
}
