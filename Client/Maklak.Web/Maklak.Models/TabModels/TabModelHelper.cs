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

        public static TabModel GenerateModel(string key)
        {
            //SiteMapHelper.SiteMap.
            TabModelHelper.TabModelType tabModelType = ModelType(key);

            return GenerateModel(tabModelType);
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

            //model.Initialize(sID);
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

        public static TabModelType ModelType(string key)
        {

            ISiteMapNode node = SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(key);

            return TabModelHelper.ModelType(node);


        }

        public static TabModelType ModelType(ISiteMapNode node)
        {            

            if (Enum.GetNames(typeof(TabModelHelper.TabModelType)).Contains(node.Key))
                return (TabModelHelper.TabModelType)Enum.Parse(typeof(TabModelHelper.TabModelType), node.Key);

            ISiteMapNode parentNode = node.ParentNode;

            return TabModelHelper.ModelType(node);
        }

        public static string DefaultYModelKey
        {
            get
            {
                return ModelType(TabModelHelper.RootTabNode).ToString();
            }
        }

        public static string DefaultXModelKey
        {
            get
            {
                ISiteMapNode xNode = RootTabNode.ChildNodes.Where(n => n.Key == DefaultKey(RootTabNode)).FirstOrDefault();
                return TabModelHelper.ModelType(xNode).ToString();
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
