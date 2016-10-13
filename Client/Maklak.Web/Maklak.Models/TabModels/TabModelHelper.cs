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
    public static class TabModelHelper
    {        

        public enum TabModelType {NONE, CATEGORY, SEARCH, INOUT, MANAGE, LOGIN }

       

        public static TabModel GenerateModel(Guid sID, TabModelType modelType)
        {
            // вызывается при первой загрузке страницы для генерции модели вруную
            TabModel model = TabModelHelper.GenerateModel(modelType);
            model.Initialize(sID);
            return model;
        }

        internal static string DefaultController(Guid sID)
        {           

            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);
            ModelDS.SiteMapDataTable siteMap = (rootRow.Table as ModelDS.SiteMapDataTable);
            ModelDS.SiteMapRow verticalTabRow = siteMap.Where(r => r.Key == rootRow.DefaultKey).FirstOrDefault();

            return siteMap.Where(r => r.Key == verticalTabRow.DefaultKey).Select(mr => mr.Controller).FirstOrDefault();
        }

        internal static string DefaultAction(Guid sID)
        {
            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);
            ModelDS.SiteMapDataTable siteMap = (rootRow.Table as ModelDS.SiteMapDataTable);
            ModelDS.SiteMapRow verticalTabRow = siteMap.Where(r => r.Key == rootRow.DefaultKey).FirstOrDefault();

            return siteMap.Where(r => r.Key == verticalTabRow.DefaultKey).Select(mr => mr.Action).FirstOrDefault();
        }

        public static TabModel GenerateModel(string key)
        {
            //вызывается при привязке запроса к модели. key приходит из запроса
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

            ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == TabModelType.CATEGORY.ToString()).FirstOrDefault();

            return row;
        }

        public static TabModelType ModelType(string Key)
        {
            if (Enum.GetNames(typeof(TabModelHelper.TabModelType)).Contains(Key))
                return (TabModelHelper.TabModelType)Enum.Parse(typeof(TabModelHelper.TabModelType), Key);

            return TabModelType.NONE;
        }
        
    }
}
