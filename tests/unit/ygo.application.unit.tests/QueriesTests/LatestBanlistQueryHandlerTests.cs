using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
   public class LatestBanlistQueryHandlerTests
    {
        [TestInitialize]
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
