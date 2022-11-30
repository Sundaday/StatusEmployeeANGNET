using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using BackEndApi.Services.Contract;
using BackEndApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseApi<List<DepartmentDTO>> responseApi = new ResponseApi<List<DepartmentDTO>>();
            try
            {
                List<Department> departmentList = await _departmentService.GetList();
                if (departmentList.Count > 0)
                {
                    List<DepartmentDTO> dtoList = _mapper.Map<List<DepartmentDTO>>(departmentList);
                    responseApi = new ResponseApi<List<DepartmentDTO>>() { Status = true, Msg = "Ok", Value = dtoList };
                }
                else
                {
                    responseApi = new ResponseApi<List<DepartmentDTO>>() { Status = true, Msg = "No Data" };
                }
                return StatusCode(StatusCodes.Status200OK, responseApi);
            }
            catch (Exception ex)
            {
                responseApi = new ResponseApi<List<DepartmentDTO>>() { Status = false, Msg = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }
    }
}
