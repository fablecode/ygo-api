﻿using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ygo.application.Queries.CardSearch
{
    public class CardSearchQuery : IRequest<QueryResult>
    {
        public string SearchTerm { get; set; } = string.Empty;

        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }

    }
}