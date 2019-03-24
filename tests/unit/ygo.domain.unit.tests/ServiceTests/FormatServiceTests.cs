using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class FormatServiceTests
    {
        private IFormatRepository _formatRepository;
        private FormatService _sut;

        [SetUp]
        public void SetUp()
        {
            _formatRepository = Substitute.For<IFormatRepository>();

            _sut = new FormatService(_formatRepository);
        }

        [Test]
        public void Given_An_Acronym_Should_Invoke_FormatByAcronym_Method_Once()
        {
            // Arrange
            const string acronym = "TCG";

            // Act
            _sut.FormatByAcronym(acronym);

            // Assert
            _formatRepository.Received(1).FormatByAcronym(Arg.Is(acronym));
        }
    }
}