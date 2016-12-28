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
        
        //public bool IsRoot
        //{
        //    get { return key == DefaultKey(); }
        //}     

        // Единственное свойство необходимое для отображения TabPanel
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

        // ключь который определяет DokPosition для TabPanel-a
        // ключь заменяется на дочерний при рекурсивном проходе в TabPanl-e
        public string Key
        {
            get { return key; }
            set
            {
                
                key = AlignKey(value);
            } 
        }

        // привязывает значение к ближайшему родителю имеющему dokposition
        private string AlignKey(string inputKey)
        {
            DataSets.ModelDS.TabDataRow keyRow = data.TabData.Where(r => r.Key == inputKey).FirstOrDefault();

            if (keyRow == null)
                return string.Empty;

            if (keyRow.IsParent_IdNull())
            {
                if (!string.IsNullOrEmpty(keyRow.DokPosition) && !string.IsNullOrWhiteSpace(keyRow.DokPosition))
                    return keyRow.Key;
                else
                    return string.Empty;
            }
            

            DataSets.ModelDS.TabDataRow parentRow = data.TabData.Where(r => r.Id == keyRow.Parent_Id).FirstOrDefault();

            if (parentRow == null)
                return string.Empty;

            string parentKey = parentRow.Key;

            if (!string.IsNullOrEmpty(parentRow.DokPosition) && !string.IsNullOrWhiteSpace(parentRow.DokPosition))
                return parentKey;            

            return AlignKey(parentKey);
        }
        
        //public string SelectedKey
        //{
        //    get;
        //    set; // сеттер нужен т.к. устанавливается системой при привязке запроса когда форма TabStrip отправляет запрос на TabPanel
        //         // значение будет передано как Key в StripModel
        //}       

        public bool HasChildPanel()
        {            
            DataSets.ModelDS.TabDataRow keyRow = ChildDataRow();

            int keyRowId = keyRow.Id;

            return data.TabData.Count(r => !r.IsParent_IdNull() && r.Parent_Id == keyRowId) > 0;
        }

        public string ChildKey()
        {

            DataSets.ModelDS.TabDataRow keyRow = ChildDataRow();

            if (keyRow == null)
                return string.Empty;

            string rowKey = keyRow.Key;

            return rowKey;
        }

        private DataSets.ModelDS.TabDataRow ChildDataRow()
        {
            DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();
            // сначала ищем активную модель
            DataSets.ModelDS.TabDataRow keyRow = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.Active).FirstOrDefault();

            // если не нашли - берём модель по умолчанию
            if (keyRow == null)
                keyRow = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).FirstOrDefault();

            return keyRow;
        }

        //TODO: Надо взять ChildRow по входящему ключу
    }
}
