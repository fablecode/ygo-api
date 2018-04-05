using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Commands.AddArchetype;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class AddArchetypeCommandHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            var mediator = Substitute.For<IMediator>();
            var archetypeRepository = Substitute.For<IArchetypeRepository>();
            var appSettings = Substitute.For<IOptions<ApplicationSettings>>();
            var sut = new AddArchetypeCommandHandler(mediator, new AddArchetypeCommandValidator(), archetypeRepository, appSettings);
        }
    }
}