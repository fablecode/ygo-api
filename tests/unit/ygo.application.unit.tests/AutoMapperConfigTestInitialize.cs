using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ygo.application.unit.tests
{
    [TestClass]
    public class AutoMapperConfigTestInitialize
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            AutoMapperConfig.Configure();
        }
    }
}