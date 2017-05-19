using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Data;

namespace Maklak.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataSource" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataSource.svc or DataSource.svc.cs at the Solution Explorer and start debugging.
    public class DataSource : IDataSource
    {
        public void DoWork()
        {
        }

        public DataSet Suggestion(string inputValue)
        {
            return null;
        }
    }
}
