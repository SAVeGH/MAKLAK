using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Proxy;

namespace Maklak.Data
{
    public class TestClass
    {
        Maklak.Proxy.TestClass proxy;

        public TestClass()
        {
            proxy = new Proxy.TestClass();
        }

        public string GetData(int input)
        {
            
            return proxy.GetData(input);
        }

        public object GetIDataTest()
        {
            return proxy.GetIDataTest();
        }
    }
}
