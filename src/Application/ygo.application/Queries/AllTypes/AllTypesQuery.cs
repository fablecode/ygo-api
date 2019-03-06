﻿using System.Collections.Generic;
using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.AllTypes
{
    public class AllTypesQuery : IRequest<IEnumerable<TypeDto>>
    {
        
    }
}