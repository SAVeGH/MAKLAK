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

        public DOKPOSITION DokPosition { get; set; }

        public string Key { get; set; }

        public bool HasStepDown()
        {
            DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();

            int keyRowId = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).Select(r => r.Id).FirstOrDefault();

            return data.TabData.Count(r => !r.IsParent_IdNull() && r.Parent_Id == keyRowId) > 0;
        }

        public string NextKey()
        {
            DataSets.ModelDS.TabDataRow row = data.TabData.Where(r => r.Key == Key).FirstOrDefault();

            string rowKey = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).Select(r => r.Key).FirstOrDefault();

            return rowKey;
        }
    }
}
