using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using System.Globalization;

namespace BackEndApi.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //mapping
            #region Department
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(destiny =>
                destiny.DepartmentName,
                opt => opt.MapFrom(origin => origin.Department.Name)
                )
                .ForMember(destiny =>
                destiny.HireDate,
                opt => opt.MapFrom(origin => origin.HireDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destiny =>
                destiny.Salary,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Salary,CultureInfo.InvariantCulture))
                );

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(destiny =>
                destiny.Department,
                opt => opt.Ignore()
                )
                .ForMember(destiny =>
                destiny.HireDate,
                opt => opt.MapFrom(origin => DateTime.ParseExact(origin.HireDate,"dd/MM/yyyy",CultureInfo.InvariantCulture))
                )
                .ForMember(destiny =>
                destiny.Salary,
                opt => opt.MapFrom(origin => Decimal.Parse(origin.Salary,NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture))
                );
            #endregion
        }
    }
}
