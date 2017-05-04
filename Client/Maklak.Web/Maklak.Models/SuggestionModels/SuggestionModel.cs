using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Data;

namespace Maklak.Models
{
    

    public class SuggestionModel : BaseModel
    {
        Dictionary<int, string> suggestionValues;
        SuggestionModelHelper.SuggestionKeys suggestionKey;
        Maklak.Data.TestClass test;

        public SuggestionModel() 
        {
            suggestionValues = new Dictionary<int, string>();
            this.OnModelReady += SuggestionModel_OnModelReady;
            test = new TestClass();
        }

        private void SuggestionModel_OnModelReady()
        {
            

            for (int i = 0; i < 8; i++)
                suggestionValues.Add(i, "item_" + i.ToString());

            //ManageSelection();
        }

        private void ManageSelection()
        {
            string key = SuggestionKey.ToString();
            int itemId = this.ItemId;
            object o = test.GetIDataTest();
            DataSets.ModelDS.SelectionRow row = base.data.Selection.Where(r => r.Key == key).FirstOrDefault();           


            if (row == null)
            {
                if (itemId == 0)
                    return;

                // Добавить ключь и значение (CREATE)
                row = base.data.Selection.NewSelectionRow();
                row.Key = key;
                row.Item_Id = itemId;
                base.data.Selection.AddSelectionRow(row);                
                base.data.Selection.AcceptChanges();                
                return;
            }

            // row != null

            if (itemId == 0)
            {
                // Ничего не выбрано и ключь найден (DELETE)
                row.Delete();
                base.data.Selection.AcceptChanges();                
                return;
            }

            // Найден ключь и задано значене (UPDATE)
            row.Key = key;
            row.Item_Id = itemId;
            base.data.Selection.AcceptChanges();

            string result = test.GetData(itemId);

            //object o = test.GetIDataTest();
                       

        }       

        public Dictionary<int, string> SuggestionValues
        {
            get
            {
                return suggestionValues.Where(i=> !string.IsNullOrEmpty(this.InputValue) &&
                                                  i.Value.Contains(this.InputValue) && 
                                                  !i.Value.Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)
                                                  )
                                       .ToDictionary(kvp=> kvp.Key,kvp=> kvp.Value);
            }
        }
        public SuggestionModelHelper.SuggestionKeys SuggestionKey
        {
            get
            {
                return suggestionKey;
            }
            set
            {
                suggestionKey = value;

                ManageSelection();
            }
        }
        public string InputValue { get; set; }
        public int ItemId
        {
            get
            {
                return suggestionValues.Count() > 0 && suggestionValues.ContainsValue(this.InputValue) ? suggestionValues.Keys.Where(k=> suggestionValues[k].Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() : 0;
            }
        }
    }
}
