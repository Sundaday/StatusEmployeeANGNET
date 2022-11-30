using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using BackEndApi.Services.Contract;
using BackEndApi.Services.Implementation;
using BackEndApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseApi<List<EmployeeDTO>> responseApi = new ResponseApi<List<EmployeeDTO>>();
            try
            {
                List<Employee> employeeList = await _employeeService.GetList();
                if (employeeList.Count > 0)
                {
                    List<EmployeeDTO> dtoList = _mapper.Map<List<EmployeeDTO>>(employeeList);
                    responseApi = new ResponseApi<List<EmployeeDTO>> { Status = true, Msg = "Ok", Value = dtoList };
                }
                else
                {
                    responseApi = new ResponseApi<List<EmployeeDTO>> { Status = false, Msg = "No Data" };
                }
                return StatusCode(StatusCodes.Status200OK, responseApi);
            }
            catch (Exception ex)
            {
                responseApi = new ResponseApi<List<EmployeeDTO>>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO request)
        {
            ResponseApi<EmployeeDTO> responseApi = new ResponseApi<EmployeeDTO>();
            try
            {
                Employee _model = _mapper.Map<Employee>(request);
                Employee _employeeCreated = await _employeeService.AddEmployee(_model);
                if (_employeeCreated.IdEmployee != 0)
                {
                    responseApi = new ResponseApi<EmployeeDTO>
                    {
                        Status = true,
                        Msg = "Ok",
                        Value = _mapper.Map<EmployeeDTO>(_employeeCreated)
                    };
                }
                else
                {
                    responseApi = new ResponseApi<EmployeeDTO> { Status = false, Msg = "Employee could not be created" };
                }
                return StatusCode(StatusCodes.Status200OK, responseApi);
            }
            catch (Exception ex)
            {
                responseApi = new ResponseApi<EmployeeDTO>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDTO request)
        {
            ResponseApi<EmployeeDTO> responseApi = new ResponseApi<EmployeeDTO>();
            try
            {
                Employee _model = _mapper.Map<Employee>(request);
                Employee _employeeEdited = await _employeeService.UpdateEmployee(_model);

                responseApi = new ResponseApi<EmployeeDTO>()
                {
                    Status = true,
                    Msg = "Ok",
                    Value = _mapper.Map<EmployeeDTO>(_employeeEdited)
                };

                return StatusCode(StatusCodes.Status200OK, responseApi);
            }
            catch (Exception ex)
            {
                responseApi = new ResponseApi<EmployeeDTO>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseApi<bool> responseApi = new ResponseApi<bool>();
            try
            {
                Employee _employeeFound = await _employeeService.Get(id);
                bool deleted = await _employeeService.DeleteEmployee(_employeeFound);
                if (deleted)
                {
                    responseApi = new ResponseApi<bool>() { Status = true, Msg = "Ok" };
                }
                else
                {
                    responseApi = new ResponseApi<bool>() { Status = false, Msg = "Employee not found" };
                }

                return StatusCode(StatusCodes.Status200OK, responseApi);
            }
            catch (Exception ex)
            {
                responseApi = new ResponseApi<bool>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }
    }
}
