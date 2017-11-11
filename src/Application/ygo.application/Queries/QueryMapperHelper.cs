using System;
using System.Linq;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Queries
{
    public static class QueryMapperHelper
    {
        public static LatestBanlistDto MapToLatestBanlist(this Banlist banlist)
        {
            var latestBanlist = new LatestBanlistDto();

            if (banlist == null)
                return latestBanlist;

            var banlistCards = banlist.BanlistCard.GroupBy(blc => blc.Limit.Name).Select(nc => nc);

            var groupedCards = banlistCards as IGrouping<string, BanlistCard>[] ?? banlistCards.ToArray();

            var forbiddenCards = groupedCards.SingleOrDefault(grp => grp.Key.Equals("Forbidden", StringComparison.OrdinalIgnoreCase));
            var limnitCards = groupedCards.SingleOrDefault(grp => grp.Key.Equals("Limited", StringComparison.OrdinalIgnoreCase));
            var semiLimitedCards = groupedCards.SingleOrDefault(grp => grp.Key.Equals("Semi-Limited", StringComparison.OrdinalIgnoreCase));
            var unlimitedCards = groupedCards.SingleOrDefault(grp => grp.Key.Equals("Unlimited", StringComparison.OrdinalIgnoreCase));

            latestBanlist.Format = banlist.Format.Acronym.ToUpper();
            latestBanlist.ReleaseDate = banlist.ReleaseDate.ToString("MMMM dd, yyyy");

            if (forbiddenCards != null)
                latestBanlist.Forbidden = forbiddenCards.Select(MapToLatestBanlistCard).ToList();

            if (limnitCards != null)
                latestBanlist.Limited = limnitCards.Select(MapToLatestBanlistCard).ToList();

            if (semiLimitedCards != null)
                latestBanlist.SemiLimited = semiLimitedCards.Select(MapToLatestBanlistCard).ToList();

            if (unlimitedCards != null)
                latestBanlist.Unlimited = unlimitedCards.Select(MapToLatestBanlistCard).ToList();

            return latestBanlist;
        }

        private static LatestBanlistCardDto MapToLatestBanlistCard(BanlistCard blc)
        {
            return new LatestBanlistCardDto { Id = blc.CardId, Name = blc.Card.Name };
        }
    }
}