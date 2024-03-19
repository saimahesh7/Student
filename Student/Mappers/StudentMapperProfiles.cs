using AutoMapper;
using Student.Models.Domain;
using Student.Models.DTOs;

namespace Student.Mappers
{
    public class StudentMapperProfiles : Profile
    {
        public StudentMapperProfiles()
        {
            CreateMap<Students,StudentsDto>().ReverseMap();
            CreateMap<Students,AddStudentDto>().ReverseMap();
            CreateMap<UpdateStudentDto,Students>().ReverseMap();
        }
    }
}
