using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public class TabPanelModel : BaseModel
    {
        public enum DOKPOSITION { LEFT, TOP, RIGHT, BOTTOM };

        private string key;

        public TabPanelModel()
        {
            this.OnModelReady += TabPanelModel_OnModelReady;
        }

        private void TabPanelModel_OnModelReady()
        {
            key = DefaultKey(); // ставится при создании модели
        }

        private string DefaultKey()
        {
            return data.TabData.Where(r => r.IsParent_IdNull()).Select(r => r.Key).FirstOrDefault(); // ключь по умолчанию
        }  
        
        public bool IsRoot
        {
            get { return key == DefaultKey(); }
        }     

        public DOKPOSITION DokPosition
        {
            get
            {
                if (string.IsNullOrEmpty(Key))
                    return DOKPOSITION.LEFT;

                DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();

                if (row == null)
                    return DOKPOSITION.LEFT;

                return (DOKPOSITION)Enum.Parse(typeof(DOKPOSITION), row.DokPosition);
            }
        }

        public string Key
        {
            get { return key; }
            set { key = value; } // сеттер нужен т.к. устанавливается системой при привязке запроса
        } 
        
        public string SelectedKey { get; set; }       

        public bool HasChildPanel()
        {
            DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();

            int keyRowId = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).Select(r => r.Id).FirstOrDefault();

            return data.TabData.Count(r => !r.IsParent_IdNull() && r.Parent_Id == keyRowId) > 0;
        }

        public string ChildKey()
        {
            DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();

            string rowKey = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).Select(r => r.Key).FirstOrDefault();

            return rowKey;
        }
    }
}
