using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateBanlist
{
    public class UpdateBanlistCommandHandler : IRequestHandler<UpdateBanlistCommand, CommandResult>
    {
        private readonly IBanlistRepository _banlistRepository;
        private readonly IValidator<UpdateBanlistCommand> _validator;

        public UpdateBanlistCommandHandler(IBanlistRepository banlistRepository, IValidator<UpdateBanlistCommand> validator)
        {
            _banlistRepository = banlistRepository;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateBanlistCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var banlistToupdate = await _banlistRepository.GetBanlistById(request.Id);

                if (banlistToupdate != null)
                {
                    banlistToupdate.UpdateBanlistWith(request);

                    commandResult.Data = Mapper.Map<BanlistDto>(await _banlistRepository.Update(banlistToupdate));
                    commandResult.IsSuccessful = true;
                }
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}