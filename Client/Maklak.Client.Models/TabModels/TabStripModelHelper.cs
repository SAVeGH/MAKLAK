using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;

using Maklak.Client.Models.Helpers;
using Maklak.Client.Models.DataSets;

namespace Maklak.Client.Models
{
    public static class TabStripModelHelper
    {        

        public enum TabModelType {NONE, CATEGORY, SEARCH, INOUT, MANAGE, LOGIN }        

        public static TabStripModel GenerateModel(Guid sID, string key)
        {
            if (string.IsNullOrEmpty(key))
                key = ModelHelper.RootKey(sID); 
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

        public static TabModelType ModelType(string Key)
        {
            if (Enum.GetNames(typeof(TabStripModelHelper.TabModelType)).Contains(Key))
                return (TabStripModelHelper.TabModelType)Enum.Parse(typeof(TabStripModelHelper.TabModelType), Key);

            return TabModelType.NONE;
        }
        
    }
}
