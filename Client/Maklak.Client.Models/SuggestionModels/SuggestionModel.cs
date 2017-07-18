using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.Data;
using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    

    public class SuggestionModel : BaseModel
    {
        //Dictionary<int, string> suggestionValues;
        SuggestionModelHelper.SuggestionKeys suggestionKey;
        //Maklak.Client.Data.TestClass test;
        Maklak.Client.Data.DataSource dataSource;

        public SuggestionModel() 
        {
            //suggestionValues = new Dictionary<int, string>();
            this.OnModelReady += SuggestionModel_OnModelReady;
            //test = new TestClass();
            dataSource = new DataSource();
        }

        private void SuggestionModel_OnModelReady()
        {
            // строка для записи информации о вводе юзера
            //ModelDS.SuggestionRow sgRow = this.data.Suggestion.Where(r => r.IsSuggestedNull()).FirstOrDefault();
            // Проверяем и добавляем только один раз
            //if (sgRow != null)
            //    return;

            
            //sgRow = this.data.Suggestion.NewSuggestionRow();
            //sgRow.Key = string.Empty;
            //this.data.Suggestion.AddSuggestionRow(sgRow);
            //this.data.Suggestion.AcceptChanges();


            //this.data.Suggestion.Clear();

            //for (int i = 0; i < 8; i++)
            //{               

            //    ModelDS.SuggestionRow row = this.data.Suggestion.NewSuggestionRow();

            //    row.Id = i;
            //    row.ItemValue = "item_" + i.ToString();
            //    row.Key = "TAG";

            //    this.data.Suggestion.AddSuggestionRow(row);

            //}
            //suggestionValues.Add(i, "item_" + i.ToString());

            //ManageSelection();
        }

        private void ManageSelection()
        {
            string key = SuggestionKey.ToString();
            ModelDS.SuggestionRow sgRow = this.data.Suggestion.Where(r => r.Key == key && r.IsIdNull()).FirstOrDefault();
            bool validValue = !string.IsNullOrEmpty(this.InputValue) && !string.IsNullOrWhiteSpace(this.InputValue);

            ModelDS.SuggestionRow currentRow = this.data.Suggestion.Where(r => !r.IsIsCurrentNull() && r.IsCurrent == 1).FirstOrDefault();

            if (currentRow != null)
            {
                currentRow.SetIsCurrentNull();
                this.data.Suggestion.AcceptChanges();
            }

            if (validValue)
            {
                if (sgRow == null)
                {
                    // Add
                    sgRow = this.data.Suggestion.NewSuggestionRow();
                    this.data.Suggestion.AddSuggestionRow(sgRow);
                }

                // or Update
                sgRow.Key = key;
                sgRow.ItemValue = this.InputValue;
                sgRow.IsCurrent = 1;

            }
            else
            {
                // Delete
                if (sgRow != null)
                    this.data.Suggestion.RemoveSuggestionRow(sgRow); 
            }
                        
            this.data.Suggestion.AcceptChanges();

            dataSource.MakeSuggestion(this.data);
        }

        //private void ManageSelection()
        //{
        //    string key = SuggestionKey.ToString();
        //    int itemId = this.ItemId;
        //    //object o = test.GetIDataTest();
        //    DataSets.ModelDS.SuggestionRow row = base.data.Suggestion.Where(r => r.Key == key).FirstOrDefault();           


        //    if (row == null)
        //    {
        //        if (itemId == 0)
        //            return;

        //        // Добавить ключь и значение (CREATE)
        //        row = base.data.Suggestion.NewSuggestionRow();
        //        row.Key = key;
        //        row.Id = itemId;
        //        base.data.Suggestion.AddSuggestionRow(row);                
        //        base.data.Suggestion.AcceptChanges();                
        //        return;
        //    }

        //    // row != null

        //    if (itemId == 0)
        //    {
        //        // Ничего не выбрано и ключь найден (DELETE)
        //        row.Delete();
        //        base.data.Suggestion.AcceptChanges();                
        //        return;
        //    }

        //    // Найден ключь и задано значене (UPDATE)
        //    row.Key = key;
        //    row.Id = itemId;
        //    base.data.Suggestion.AcceptChanges();

        //    //string result = test.GetData(itemId);

        //    //object o = test.GetIDataTest();


        //}

        public ModelDS.SuggestionDataTable SuggestionData
        {
            get
            {
                ModelDS.SuggestionDataTable suggestionData = new ModelDS.SuggestionDataTable();

                this.data.Suggestion.Where(r => r.Key == suggestionKey.ToString() &&
                                               !r.IsIdNull()&&
                                               r.ItemValue != this.InputValue)
                                    .ToList()
                                    .ForEach(r => suggestionData.ImportRow(r));

                suggestionData.AcceptChanges();

                return suggestionData;
            }
        }

        //public ModelDS.SuggestionDataTable SuggestionData
        //{
        //    get
        //    {
        //        ModelDS.SuggestionDataTable suggestionData = new ModelDS.SuggestionDataTable();

        //        this.data.Suggestion.Where(r => r.Key == suggestionKey.ToString() &&
        //                                        !string.IsNullOrEmpty(this.InputValue) &&
        //                                        !r.ItemValue.Contains(this.InputValue) &&
        //                                        !r.ItemValue.Equals(this.InputValue, StringComparison.InvariantCultureIgnoreCase))
        //                            .ToList()
        //                            .ForEach(r => suggestionData.ImportRow(r));

        //        suggestionData.AcceptChanges();

        //        return suggestionData;
        //    }
        //}

        //public ModelDS.SuggestionDataTable SuggestionData
        //{
        //    get
        //    {
        //        ModelDS.SuggestionDataTable suggestionData = new ModelDS.SuggestionDataTable();

        //        this.data.Suggestion.Where(r => r.Key == suggestionKey.ToString() &&
        //                                        !string.IsNullOrEmpty(this.InputValue) &&
        //                                        !r.ItemValue.Contains(this.InputValue) &&
        //                                        !r.ItemValue.Equals(this.InputValue, StringComparison.InvariantCultureIgnoreCase))
        //                            .ToList()
        //                            .ForEach(r => suggestionData.ImportRow(r));

        //        suggestionData.AcceptChanges();

        //        return suggestionData;
        //    }
        //}


        //public Dictionary<int, string> SuggestionValues
        //{
        //    get
        //    {
        //        return suggestionValues.Where(i=> !string.IsNullOrEmpty(this.InputValue) &&
        //                                          i.Value.Contains(this.InputValue) && 
        //                                          !i.Value.Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)
        //                                          )
        //                               .ToDictionary(kvp=> kvp.Key,kvp=> kvp.Value);
        //    }
        //}

        // Свойство устанавливается в SuggestinoModelBinder при привязке модели
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
        public string InputValue { set; get; }

        //public string CurrentInputValue
        //{
        //    get
        //    {
        //        return this.data.Suggestion.Where(r => !r.IsSuggestedNull() && r.Suggested == 0 && r.Key == this.SuggestionKey.ToString()).Select(r => r.ItemValue).FirstOrDefault();
        //    }
        //}
        //public int ItemId
        //{
        //    get
        //    {
        //        return suggestionValues.Count() > 0 && suggestionValues.ContainsValue(this.InputValue) ? suggestionValues.Keys.Where(k=> suggestionValues[k].Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() : 0;
        //    }
        //}

        public int ItemId
        {
            get
            {
                return 0; // this.data.Suggestion.Where(r => r.ItemValue.Equals(this.InputValue, StringComparison.InvariantCultureIgnoreCase)).Select(r => r.Id).FirstOrDefault();
                //return suggestionValues.Count() > 0 && suggestionValues.ContainsValue(this.InputValue) ? suggestionValues.Keys.Where(k => suggestionValues[k].Equals(this.InputValue, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() : 0;
            }
        }
    }
}
