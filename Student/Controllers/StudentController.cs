using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.CustomActionFilters;
using Student.Models.Domain;
using Student.Models.DTOs;
using Student.Repositories;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository,IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles ="Reader,Writer")]
        public async Task<IActionResult> GetAllStudents()
        {
            //Get the Domain data from Repository
            var studentsDomain=await studentRepository.GetAllStudentsAsync();

            //Convert the Doamin Data into DTO data using Automappers and return the data to the client
            var studentDto=mapper.Map<List<StudentsDto>>(studentsDomain);
            return Ok(studentDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetStudent([FromRoute]Guid id)
        {
            //Check whether the given Id student is present in repository are not
            var studentDomain=await studentRepository.GetStudentAsync(id);

            //If studentDomain is not null
            if(studentDomain != null)
            {
                //convert the Domain data into DTO data through automappers and return to the client
                var studentDto=mapper.Map<StudentsDto>(studentDomain);
                return Ok(studentDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateStudent([FromBody]AddStudentDto addStudentDto)
        {
            //Convert DTO Data to Domain Data using automappers and pass that data to repository method
            var studentDoamin = mapper.Map<Students>(addStudentDto);
            await studentRepository.CreateStudentAsync(studentDoamin);
            
            //Convert the Domain data into Dto Data and send it to the client
            var studentDto= mapper.Map<StudentsDto>(studentDoamin);
            return CreatedAtAction(nameof(GetStudent),new  {Id=studentDto.Id },studentDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateStudentDto updateStudentDto)
        {
            //Convert Dto Data into Domain data and pass that data to repository
            var studentDomain=mapper.Map<Students>(updateStudentDto);
            studentDomain=await studentRepository.UpdateAsync(id, studentDomain);

            if(studentDomain != null)
            {
                //Convert the domain data to Dto data and sent to the client
                var studentDto = mapper.Map<StudentsDto>(studentDomain);
                return Ok(studentDto);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> RemoveStudent([FromRoute]Guid id)
        {
            //Check whether the given Id is present in zrepository Method are not
            var studentDomain = await studentRepository.RemoveStudentAsync(id);
            if(studentDomain != null)
            {
                //Convert The domain data into Dto Data return to the client
                var studentDto=mapper.Map<StudentsDto>(studentDomain);
                return Ok(studentDto);
            }

            return NotFound();
        }
    }
}
