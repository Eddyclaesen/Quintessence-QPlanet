using System.ServiceModel;

namespace Quintessence.CulturalFit.UI.Core.Service
{
    public class ServiceFactory
    {
        public TType CreateChannel<TType>()
        {
            var channelFactory = new ChannelFactory<TType>(typeof(TType).Name);
            return channelFactory.CreateChannel();
        }
    }
}
