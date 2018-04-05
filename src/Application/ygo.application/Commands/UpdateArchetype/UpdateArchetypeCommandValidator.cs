using System;
using FluentValidation;

namespace ygo.application.Commands.UpdateArchetype
{
    public class UpdateArchetypeCommandValidator : AbstractValidator<UpdateArchetypeCommand>
    {
        private const string LinkMustBeUriMessage = "Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au";

        public UpdateArchetypeCommandValidator()
        {
            RuleFor(a => a.Id)
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