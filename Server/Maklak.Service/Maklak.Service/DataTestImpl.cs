using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maklak.Service
{
    public class DataTestImpl : IDataTest
    {
       
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}