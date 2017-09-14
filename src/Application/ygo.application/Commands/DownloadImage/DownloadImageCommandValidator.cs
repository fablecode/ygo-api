using System.IO;
using FluentValidation;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommandValidator : AbstractValidator<DownloadImageCommand>
    {
        public DownloadImageCommandValidator()
        {
            RuleFor(i => i.RemoteImageUrl)
                .NotNull();

            RuleFor(i => i.ImageFileName)
                .NotNull()
                .NotEmpty()
                .Must(i => i.IndexOfAny(Path.GetInvalidFileNameChars()) == 0)
                    .WithMessage("Invalid filename. The Image filename {ImageFileName} contains invalid character(s).");
        }
    }
}