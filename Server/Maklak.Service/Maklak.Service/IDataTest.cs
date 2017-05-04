using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.ServiceModel;

namespace Maklak.Service
{
    
    [ServiceContract]
    //[ServiceKnownType(typeof(DataTestImpl))]    
    public interface IDataTest
    {
        [OperationContract]
        string GetData(int value);
    }
}
