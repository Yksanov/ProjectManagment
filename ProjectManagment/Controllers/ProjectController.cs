using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Data;
using ProjectManagment.Models;
using ProjectManagment.Models.Dto;
using System.Xml.Linq;

namespace ProjectManagment.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext context;
        public ProjectController(ApplicationDbContext context)
        {
            this.context = context;
        }   
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);

            if(project != null)
            {
                return Ok(project);         
            }
            return NotFound(id);    
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Project>> CreateProject([FromBody] CreateProjectDto projectDto)
        {
            var project = new Project()
            {
                Name = projectDto.Name,
                CustomerCompany = projectDto.CustomerCompany,
                ExecutorCompany = projectDto.ExecutorCompany,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Priority = projectDto.Priority,
                ManagerId = projectDto.ManagerId,
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            return Ok(project);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Project>> UpdateEmployee([FromBody] UpdateProjectDto projectDto)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectDto.ProjectId);

            if (project == null)
                return NotFound($"Не найден сотрудник с Id = {projectDto.ProjectId}");

            project.Name = projectDto.Name;
            project.CustomerCompany = projectDto.CustomerCompany;
            project.ExecutorCompany = projectDto.ExecutorCompany;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Priority = projectDto.Priority;
            project.ManagerId = projectDto.ManagerId;

            context.Projects.Update(project);

            await context.SaveChangesAsync();
            return Ok(project);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Project>> DeleteProjectId(int id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
            context.Projects.Remove(project);
            await context.SaveChangesAsync();

            return Ok(id);
        }
    }
}
