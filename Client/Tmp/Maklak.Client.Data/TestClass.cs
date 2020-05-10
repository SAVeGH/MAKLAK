using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.Proxy;

namespace Maklak.Client.Data
{
    public class TestClass
    {
        Proxy.TestClass proxy;

        public TestClass()
        {
            proxy = new Proxy.TestClass();
        }

        public string GetData(int input)
        {
            
            return proxy.GetData(input);
        }

        
    }
}
