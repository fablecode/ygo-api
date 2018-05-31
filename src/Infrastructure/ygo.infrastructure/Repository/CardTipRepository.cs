﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;

namespace ygo.infrastructure.Repository
{
    public class CardTipRepository : ICardTipRepository
    {
        private readonly YgoDbContext _context;

        public CardTipRepository(YgoDbContext context)
        {
            _context = context;
        }

        public Task<List<TipSection>> TipSectionsByCardId(long cardId)
        {
            return _context
                .TipSection
                    .Include(t => t.Card)
                    .Include(t => t.Tip)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteByCardId(long cardId)
        {
            var tipSections = await TipSectionsByCardId(cardId);

            if (tipSections.Any())
            {
                _context.Remove(tipSections);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(List<TipSection> tipSections)
        {
            _context.TipSection.UpdateRange(tipSections);
            await _context.SaveChangesAsync();
        }
    }
}