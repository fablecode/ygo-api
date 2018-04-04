using System;
using FluentValidation;

namespace ygo.application.Commands.AddArchetype
{
    public class AddArchetypeCommandValidator : AbstractValidator<AddArchetypeCommand>
    {
        private const string LinkMustBeUriMessage = "Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au";

        public AddArchetypeCommandValidator()
        {
            RuleFor(a => a.ArchetypeNumber)
                .GreaterThan(0);

            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.ProfileUrl)
                .NotNull()
                .NotEmpty()
                .Must(LinkMustBeAUri)
                .WithMessage(LinkMustBeUriMessage);

            RuleFor(a => a.ImageUrl)
                .Must(LinkMustBeAUri)
                .WithMessage(LinkMustBeUriMessage)
                .When(a => !string.IsNullOrWhiteSpace(a.ImageUrl));
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            return Uri.TryCreate(link, UriKind.Absolute, out var outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}