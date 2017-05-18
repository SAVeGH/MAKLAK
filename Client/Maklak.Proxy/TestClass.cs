using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Proxy
{
    public class TestClass
    {
        ServiceTestReference.ServiceTestClient client;

        public TestClass()
        {
            client = new ServiceTestReference.ServiceTestClient();           

        }

        public string GetData(int i)
        {
            return client.GetData(i);
        }

        
    }
}
