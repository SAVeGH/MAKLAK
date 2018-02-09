using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel;
using Maklak.Client.Proxy.DataSourceServiceReference;

namespace Maklak.Client.Proxy
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

        public SuggestionDS MakeSuggestion(SuggestionDS inputDS)
        {
            return client.Suggestion(inputDS);
        }

		public TreeDS ConstructTree(TreeDS treeDS)
		{
			return client.Tree(treeDS);
		}
    }
}
