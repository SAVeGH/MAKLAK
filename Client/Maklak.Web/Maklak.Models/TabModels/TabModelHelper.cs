using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Html.Models;

namespace Maklak.Models
{
    public static class TabModelHelper
    {        

        public enum TabModelType { VERTICAL, LOGIN, INOUT, SEARCH, MANAGE }

        private static Dictionary<TabModelType, Dictionary<int,TabModelType>> tabReference;

        static TabModelHelper()
        {
            Dictionary<int, TabModelType> hTab = new Dictionary<int, TabModelType>() { { 4,TabModelType.LOGIN },
                                                                                       { 1,TabModelType.SEARCH },
                                                                                       { 3,TabModelType.MANAGE},
                                                                                       { 2,TabModelType.INOUT}
                                                                                     };


            tabReference = new Dictionary<TabModelType, Dictionary<int, TabModelType>>() { { TabModelType.VERTICAL,hTab} };
        }

        public static TabModel GenerateModel(TabModelType modelType)
        {
            TabModel model = null;

            switch (modelType)
            {
                case TabModelType.VERTICAL:
                    model = new TabVModel();
                    break;
                case TabModelType.LOGIN:
                    model = new LoginTabModel();
                    break;
                case TabModelType.SEARCH:
                    model = new SearchTabModel();
                    break;
                case TabModelType.INOUT:
                    model = new InOutTabModel();
                    break;
                case TabModelType.MANAGE:
                    model = new ManageTabModel();
                    break;
            }

            ISiteMap sm = MvcSiteMapProvider.SiteMaps.Current;

            ISiteMapNode node = sm.RootNode;
           
            model.Action = "TabContent";
            //SiteMapFactory
            //SiteMap. sm = new SiteMap();
            //MvcSiteMapProvider.Web.Html.Models.SiteMapNodeModel model = new MvcSiteMapProvider.Web.Html.Models.SiteMapNodeModel();
            return model;
        }

        public static TabModel GenerateModel(TabModelType keyModelType, int selectedId)
        {
            Dictionary<int, TabModelType> hTab = tabReference[keyModelType];

            TabModelType modelType = hTab[selectedId];

            return GenerateModel(modelType);
        }
    }
}
