using NUnit.Framework;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
   public class LatestBanlistQueryHandlerTests
    {
        [SetUp]
        public void SetUp()
        {
            var sut = new LatestBanlistQueryHandler();
        }


    }

    public class LatestBanlistQueryHandler
    {
        public LatestBanlistQueryHandler()
        {
        }
    }
}
