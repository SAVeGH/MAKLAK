using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;


namespace Maklak.Models
{
    public static class TabModelHelper
    {        

        public enum TabModelType { CATEGORY, SEARCH, INOUT, MANAGE, LOGIN }

        //private static Dictionary<TabModelType, Dictionary<int,TabModelType>> tabReference;

        static TabModelHelper()
        {
            //Dictionary<int, TabModelType> hTab = new Dictionary<int, TabModelType>() { { 4,TabModelType.LOGIN },
            //                                                                           { 1,TabModelType.SEARCH },
            //                                                                           { 3,TabModelType.MANAGE},
            //                                                                           { 2,TabModelType.INOUT}
            //                                                                         };


            //tabReference = new Dictionary<TabModelType, Dictionary<int, TabModelType>>() { { TabModelType.VERTICAL,hTab} };
        }

        public static TabModel GenerateModel(TabModelType modelType)
        {
            TabModel model = null;

            switch (modelType)
            {
                case TabModelType.CATEGORY:
                    model = new CategoryTabModel();
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

            
            return model;
        }

        public static ISiteMapNode RootTabNode
        {
            get
            {
                return SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(TabModelType.CATEGORY.ToString());
            }
        }

        public static string DefaultKey(ISiteMapNode node)
        {
            return node.Attributes["defaultkey"] == null ? string.Empty : Convert.ToString(node.Attributes["defaultkey"]);
        }

        public static string DefaultAction
        {
            get
            {
                return SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(DefaultKey(SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(DefaultKey(RootTabNode)))).Action;
            }
        }

        public static string DefaultController
        {
            get
            {
                return SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(DefaultKey(SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(DefaultKey(RootTabNode)))).Controller;
            }
        }

        //public static TabModel GenerateModel(TabModelType keyModelType, int selectedId)
        //{
        //    Dictionary<int, TabModelType> hTab = tabReference[keyModelType];

        //    TabModelType modelType = hTab[selectedId];

        //    return GenerateModel(modelType);
        //}
    }
}
