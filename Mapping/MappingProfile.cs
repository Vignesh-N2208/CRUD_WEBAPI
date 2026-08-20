using AutoMapper;
using CRUD_WEBAPI.DTOs;
using CRUD_WEBAPI.Models;

namespace CRUD_WEBAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
    }
}