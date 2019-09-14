using AutoMapper;
using Microsoft.Extensions.Options;
using ygo.application.Configuration;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.domain.Helpers;

namespace ygo.application.Mappings.Resolvers
{
    public class CardImageEndpointResolver : 
        IValueResolver<Card, CardDto, string>, 
        IValueResolver<Card, MonsterCardDto, string>, 
        IValueResolver<Card, SpellCardDto, string>, 
        IValueResolver<Card, TrapCardDto, string>
    {
        private readonly IOptions<ApplicationSettings> _options;

        public CardImageEndpointResolver(IOptions<ApplicationSettings> options)
        {
            _options = options;
        }

        public string Resolve(Card source, CardDto destination, string destMember, ResolutionContext context)
        {
            return GetCardImage(source);
        }

        public string Resolve(Card source, MonsterCardDto destination, string destMember, ResolutionContext context)
        {
            return GetCardImage(source);
        }

        public string Resolve(Card source, SpellCardDto destination, string destMember, ResolutionContext context)
        {
            return GetCardImage(source);
        }

        public string Resolve(Card source, TrapCardDto destination, string destMember, ResolutionContext context)
        {
            return GetCardImage(source);
        }

        #region private helpers

        private string GetCardImage(Card source)
        {
            return $"{_options.Value.CardImageEndpoint}/{source.Name.SanitizeFileName()}";
        }

        #endregion
    }
}