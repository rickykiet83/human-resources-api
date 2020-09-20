using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities.DTOs;
using HumanResourceAPI.Infrastructure;
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

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies =  _repository.Company.FindAll(trackChange: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at: {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}