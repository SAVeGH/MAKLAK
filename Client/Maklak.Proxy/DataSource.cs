using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel;
using Maklak.Proxy.DataSourceServiceReference;

namespace Maklak.Proxy
{
    public class DataSource : ProxyBase
    {
        DataSourceClient client;

        protected override ICommunicationObject CreateProxy()
        {
            client = new DataSourceClient();
            return client;

        }        

        public void DoWork()
        {
            client.DoWork();
        }
    }
}
