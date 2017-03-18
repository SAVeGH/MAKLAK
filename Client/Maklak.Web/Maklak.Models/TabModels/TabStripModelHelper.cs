using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;

using Maklak.Models.Helpers;
using Maklak.Models.DataSets;

namespace Maklak.Models
{
    public static class TabStripModelHelper
    {        

        public enum TabModelType {NONE, CATEGORY, SEARCH, INOUT, MANAGE, LOGIN }



        public static TabStripModel GenerateModel(Guid sID, TabModelType modelType)
        {
            // вызывается при первой загрузке страницы для генерции модели вруную
            TabStripModel model = TabStripModelHelper.GenerateModel(modelType);
            model.Initialize(sID);
            return model;
        }

        internal static string DefaultController(Guid sID)
        {           

            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);
            //ModelDS.SiteMapDataTable siteMap = (rootRow.Table as ModelDS.SiteMapDataTable);
            //ModelDS.SiteMapRow verticalTabRow = siteMap.Where(r => r.Key == rootRow.DefaultKey).FirstOrDefault();

            //return siteMap.Where(r => r.Key == verticalTabRow.DefaultKey).Select(mr => mr.Controller).FirstOrDefault();

            return rootRow.RecursiveController;
        }

        internal static string DefaultAction(Guid sID)
        {
            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);
            //ModelDS.SiteMapDataTable siteMap = (rootRow.Table as ModelDS.SiteMapDataTable);
            //ModelDS.SiteMapRow verticalTabRow = siteMap.Where(r => r.Key == rootRow.DefaultKey).FirstOrDefault();

            //return siteMap.Where(r => r.Key == verticalTabRow.DefaultKey).Select(mr => mr.Action).FirstOrDefault();

            return rootRow.RecursiveAction;
        }

        public static TabStripModel GenerateModel(Guid sID, string key)
        {
            if (string.IsNullOrEmpty(key))
                key = GetRootTabRow(sID).Key;
            //вызывается при привязке запроса к модели. key приходит из запроса
            TabStripModelHelper.TabModelType tabModelType = ModelType(key);

            return GenerateModel(tabModelType);
        }

        public static TabStripModel GenerateModel(TabModelType modelType)
        {
            TabStripModel model = null;

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

        public static TabModelType GetDefaultXModelType(Guid sID)
        {
            ModelDS.SiteMapRow row = GetRootTabRow(sID);

            return ModelType(row.DefaultKey);
        }

        public static TabModelType GetDefaultYModelType(Guid sID)
        {
            ModelDS.SiteMapRow row = GetRootTabRow(sID);

            return ModelType(row.Key);
        }

        private static ModelDS.SiteMapRow GetRootTabRow(Guid sID)
        {
            ModelDS data = SessionHelper.GetModel(sID);

            //ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == TabModelType.CATEGORY.ToString()).FirstOrDefault();
            ModelDS.SiteMapRow row = data.SiteMap.Where(r => !r.IsRecursiveControllerNull()).FirstOrDefault();

            return row;
        }

        public static TabModelType ModelType(string Key)
        {
            if (Enum.GetNames(typeof(TabStripModelHelper.TabModelType)).Contains(Key))
                return (TabStripModelHelper.TabModelType)Enum.Parse(typeof(TabStripModelHelper.TabModelType), Key);

            return TabModelType.NONE;
        }
        
    }
}
