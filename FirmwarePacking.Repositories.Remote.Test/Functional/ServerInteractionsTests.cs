using System.ServiceModel;
using FirmwarePacking.Repositories.Remote.SautRepoApi;
using NUnit.Framework;

namespace FirmwarePacking.Repositories.Remote.Test.Functional
{
    [TestFixture]
    public class ServerInteractionsTests
    {
        private static SfpRepositoryServiceClient CreateClient()
        {
            return new SfpRepositoryServiceClient(new BasicHttpBinding(), new EndpointAddress("http://repo/api/SfpRepositoryService.svc"));
        }

        [Test]
        [Description("Проверка возможности подключиться")]
        public void ConnectivityTest()
        {
            using (SfpRepositoryServiceClient client = CreateClient())
            {
                client.Open();
                Assert.AreEqual(client.State, CommunicationState.Opened);
            }
        }

        [Test]
        [Description("Загрузка списка пакетов для БС-ДПС")]
        public void LoadSomeDataTest()
        {
            using (SfpRepositoryServiceClient client = CreateClient())
            {
                PackageDescription[] packageDescriptions = client.GetCompositeTypes(20, 1);
                CollectionAssert.IsNotEmpty(packageDescriptions);
            }
        }
    }
}
