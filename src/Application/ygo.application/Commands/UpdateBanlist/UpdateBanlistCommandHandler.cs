using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateBanlist
{
    public class UpdateBanlistCommandHandler : IRequestHandler<UpdateBanlistCommand, CommandResult>
    {
        private readonly IBanlistService _banlistService;
        private readonly IValidator<UpdateBanlistCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateBanlistCommandHandler(IBanlistService banlistService, IValidator<UpdateBanlistCommand> validator, IMapper mapper)
        {
            _banlistService = banlistService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(UpdateBanlistCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var banlistToupdate = await _banlistService.GetBanlistById(request.Id);

                if (banlistToupdate != null)
                {
                    banlistToupdate.UpdateBanlistWith(request);

                    commandResult.Data = _mapper.Map<BanlistDto>(await _banlistService.Update(banlistToupdate));
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