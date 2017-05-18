using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Proxy;

namespace Maklak.Data
{
    public class DataSource
    {
        Proxy.DataSource dataSource;

        public DataSource()
        {
            dataSource = new Proxy.DataSource();
        }

        public void DoWork()
        {
            dataSource.DoWork();
        }
    }
}
