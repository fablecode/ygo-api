using FluentValidation;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommandValidator : AbstractValidator<DownloadImageCommand>
    {
        public DownloadImageCommandValidator()
        {
            RuleFor(i => i.RemoteImageUrl)
                .NotNull();

            RuleFor(i => i.LocalImageFileName)
                .NotNull()
                .NotEmpty();
        }
    }
}