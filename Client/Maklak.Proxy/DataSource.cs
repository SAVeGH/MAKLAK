using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Maklak.Proxy
{
    public class DataSource
    {
        DataSourceServiceReference.DataSourceClient client;

        public DataSource()
        {
            client = new DataSourceServiceReference.DataSourceClient();
        }

        public void DoWork()
        {
            client.DoWork();
        }
    }
}
