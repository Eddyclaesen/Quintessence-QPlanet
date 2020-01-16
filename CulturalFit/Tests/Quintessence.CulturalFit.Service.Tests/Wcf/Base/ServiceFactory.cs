using System.ServiceModel;

namespace Quintessence.CulturalFit.Service.Tests.Wcf.Base
{
    public static class ServiceFactory
    {
        public static TType CreateChannel<TType>(string address = null)
        {
            if (address == null)
            {
                var channelFactory = new ChannelFactory<TType>(typeof(TType).Name);
                return channelFactory.CreateChannel();   
            }
            else
            {
                var channelFactory = new ChannelFactory<TType>(new WSHttpBinding { MaxReceivedMessageSize = int.MaxValue }, address);
                return channelFactory.CreateChannel();   
            }
        }
    }
}
