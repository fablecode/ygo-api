using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
   public class LatestBanlistQueryHandlerTests
    {
        [TestInitialize]
        public void TestInitialize()
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
