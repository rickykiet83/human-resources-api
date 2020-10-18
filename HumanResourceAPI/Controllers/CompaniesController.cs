using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        
        private static class RouteNames
        {
            public const string GetCompanies = nameof(GetCompanies);
            public const string GetCompanyById = nameof(GetCompanyById);
            public const string CreateCompany = nameof(CreateCompany);
        } 

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
        {
            try
            {
                var companies = await _repository.Company.GetCompaniesAsync(companyParameters, trackChanges: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at: {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("{id}", Name = RouteNames.GetCompanyById)]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(id);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }
        
        [HttpPost(Name = "CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyCreationDto object sent from client is null.");
                return BadRequest("CompanyCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CompanyCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = _mapper.Map<Company>(company);
            
            _repository.Company.Create(companyEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute(RouteNames.GetCompanyById, new { id = companyToReturn.Id }, companyToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(id);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database."); 
                return NotFound();
            }
            
            _repository.Company.Delete(company);
            await _repository.SaveAsync();
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdatingDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyUpdatingDto object sent from client is null.");
                return BadRequest("CompanyUpdatingDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CompanyUpdatingDto object");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = await _repository.Company.FindByIdAsync(id);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(company, companyEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateCompany(Guid id,
            [FromBody] JsonPatchDocument<CompanyUpdatingDto> patchDoc)
        {
            if(patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            
            var companyEntity = await _repository.Company.FindByIdAsync(id);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            
            var companyToPatch = _mapper.Map<CompanyUpdatingDto>(companyEntity);

            TryValidateModel(companyToPatch);
                        
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            
            patchDoc.ApplyTo(companyToPatch);

            _mapper.Map(companyToPatch, companyEntity);

            await _repository.SaveAsync();

            return NoContent();

        }
    }
}