using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands;
using ygo.application.Commands.AddCard;
using ygo.domain.Enums;
using ygo.domain.Models;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;
        private IMediator _mediator;

        [TestInitialize]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new AddCardCommandHandler(_mediator);
        }
    }

    public class AddCardCommandHandler : IAsyncRequestHandler<AddCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;

        public AddCardCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<CommandResult> Handle(AddCardCommand message)
        {
            throw new NotImplementedException();
        }

        private Task<Card> GetCardType(YgoCardType cardType, AddCardCommand message)
        {
            switch (cardType)
            {
                case YgoCardType.Monster:
                    return _mediator.Send(new AddMonsterCardCommand());
                case YgoCardType.Spell:
                    return _mediator.Send(new AddSpellCardCommand());
                case YgoCardType.Trap:
                    return _mediator.Send(new AddTrapCardCommand());
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardType));
            }
        }
    }

    public class AddMonsterCardCommand : IRequest<Card>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public int AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class AddTrapCardCommand : IRequest<Card>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class AddSpellCardCommand : IRequest<Card>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}