using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.DTOs;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HumanResourceAPI.Utility
{
    public class CompanyLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<CompanyDto, Guid> _dataShaper;

        public CompanyLinks(LinkGenerator linkGenerator, IDataShaper<CompanyDto, Guid> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<CompanyDto> companiesDto, string fields, HttpContext httpContext)
        {
            var shapedCompanies = ShapeData(companiesDto, fields);

            return ReturnLinkedCompanies(companiesDto, fields, httpContext, shapedCompanies);
        }
        
        private LinkResponse ReturnLinkedCompanies(IEnumerable<CompanyDto> companiesDto, string fields, HttpContext httpContext, List<Entity> shapedCompanies)
        {
            var companyDtoList = companiesDto.ToList();

            for (var index = 0; index < companyDtoList.Count(); index++)
            {
                var companyLinks = CreateCompanyLinks(httpContext, companyDtoList[index], fields);
                shapedCompanies[index].Add("Links", companyLinks);
            }

            var companyCollection = new LinkCollectionWrapper<Entity>(shapedCompanies);
            var linkedCompanies = CreateLinksForCompanies(httpContext, companyCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedCompanies };
        }
        
        private LinkCollectionWrapper<Entity> CreateLinksForCompanies(HttpContext httpContext, LinkCollectionWrapper<Entity> companiesWrapper)
        {
            companiesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, $"GetCompanies", values: new { }),
                "self",
                "GET"));

            return companiesWrapper;
        }
        
        private List<Link> CreateCompanyLinks(HttpContext httpContext, CompanyDto dto, string fields = "")
        {
            var id = dto.Id;
            var links = new List<Link>
            {
                new Link(
                    _linkGenerator.GetUriByAction(httpContext, $"GetCompany",
                        values: new { id, fields }),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, $"DeleteCompany", 
                        values: new { id }),
                    "delete_company",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, $"UpdateCompany", 
                        values: new { id }),
                    "update_company",
                    "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, $"PartiallyUpdateCompany",
                        values: new { id }),
                    "partially_update_company",
                    "PATCH")
            };

            return links;
        }

        private List<Entity> ShapeData(IEnumerable<CompanyDto> companiesDto, string fields) =>
            _dataShaper.ShapeData(companiesDto, fields)
                .Select(e => e.Entity)
                .ToList();
    }
}