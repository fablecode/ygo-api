﻿using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models.Db;
using ygo.core.Services;

namespace ygo.application.Commands.AddBanlist
{
    public class AddBanlistCommandHandler : IRequestHandler<AddBanlistCommand, CommandResult>
    {
        private readonly IBanlistService _banlistService;
        private readonly IValidator<AddBanlistCommand> _validator;
        private readonly IMapper _mapper;

        public AddBanlistCommandHandler(IBanlistService banlistService, IValidator<AddBanlistCommand> validator, IMapper mapper)
        {
            _banlistService = banlistService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(AddBanlistCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var newBanlist = _mapper.Map<Banlist>(request);

                var newBanlistResult = await _banlistService.Add(newBanlist);
                commandResult.Data = newBanlistResult.Id;
                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}