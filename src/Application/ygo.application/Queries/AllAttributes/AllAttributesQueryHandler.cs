﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Dto;
using ygo.application.Repository;

namespace ygo.application.Queries.AllAttributes
{
    public class AllAttributesQueryHandler : IAsyncRequestHandler<AllAttributesQuery, IEnumerable<AttributeDto>>
    {
        private readonly IAttributeRepository _repository;

        public AllAttributesQueryHandler(IAttributeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AttributeDto>> Handle(AllAttributesQuery message)
        {
            var allAttributes = await _repository.AllAttributes();

            return Mapper.Map<IEnumerable<AttributeDto>>(allAttributes);
        }
    }
}