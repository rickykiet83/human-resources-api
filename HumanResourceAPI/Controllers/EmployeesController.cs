using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using HumanResourceAPI.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResourceAPI.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeesController(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper
        )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery]EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age.");
            
            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.MetaData));

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return Ok(employeesDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var company = await _repository.Company.FindByIdAsync(companyId);
            if(company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);
            employeeEntity.CompanyId = companyId;
            
            _repository.Employee.Create(employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }
        
        [HttpPost("collection")]
        public async Task<IActionResult> CreateEmployeeCollection(Guid companyId, [FromBody] IEnumerable<EmployeeForCreationDto> employeeCollection)
        {
            if(employeeCollection == null)
            {
                _logger.LogError("Employee collection sent from client is null.");
                return BadRequest("Employee collection is null");
            }
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            
            var company = await _repository.Company.FindByIdAsync(companyId);
            if(company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);
            foreach (var employee in employeeEntities)
            {
                employee.CompanyId = companyId;
                _repository.Employee.Create(employee);
            }
           
            await _repository.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
            return Ok(companyCollectionToReturn);
        }
        
        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeDb = await _repository.Employee.FindByIdAsync(id);
            if (employeeDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return Ok(employee);
        }
    }
}