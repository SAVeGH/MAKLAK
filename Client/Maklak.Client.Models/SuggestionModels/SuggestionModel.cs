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
        
        SuggestionModelHelper.SuggestionKeys suggestionKey;
        //Maklak.Client.Data.TestClass test;
        Maklak.Client.Data.DataSource dataSource;

        public SuggestionModel() 
        {            
            this.OnModelReady += SuggestionModel_OnModelReady;
            //test = new TestClass();
            dataSource = new DataSource();
        }

        private void SuggestionModel_OnModelReady()
        {

            // строка для записи информации о вводе юзера
            ModelDS.SuggestionInputRow inputRow = this.data.SuggestionInput.FirstOrDefault();

            if (inputRow != null)
                return;

            inputRow = this.data.SuggestionInput.NewSuggestionInputRow();
            this.data.SuggestionInput.AddSuggestionInputRow(inputRow);

        }

        private void ManageSelection()
        {
            string key = SuggestionKey.ToString();
            // строка для записи информации о вводе юзера
            ModelDS.SuggestionInputRow inputRow = this.data.SuggestionInput.FirstOrDefault();
            inputRow.Key = key;
            inputRow.ItemValue = this.InputValue;

            dataSource.MakeSuggestion(this.data);
        }        

        public ModelDS.SuggestionDataTable SuggestionData
        {
            get
            {
                ModelDS.SuggestionDataTable suggestionData = new ModelDS.SuggestionDataTable();

                this.data.Suggestion
                                    .ToList()
                                    .ForEach(r => suggestionData.ImportRow(r));

                suggestionData.AcceptChanges();

                return suggestionData;
            }
        }        

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
       

    }
}
