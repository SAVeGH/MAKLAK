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

        private void ManageInput()
        {
            string key = SuggestionKey.ToString();
            // строка для записи информации о вводе юзера
            ModelDS.SuggestionInputRow inputRow = this.data.SuggestionInput.FirstOrDefault();
            inputRow.Key = key;
            inputRow.ItemValue = this.InputValue;

            DropFilter();

            dataSource.MakeSuggestion(this.data);

            UpdateFilter();
        }

        private void DropFilter()
        {
            if (this.SkipFilter)
                return;

            ModelDS.SuggestionInputRow inputRow = this.data.SuggestionInput.FirstOrDefault();

            if (inputRow.IsIdNull())
                return;

            ModelDS.SuggestionFilterRow filterRow = this.data.SuggestionFilter.Where(fr => fr.Id == inputRow.Id && fr.Key == this.SuggestionKey.ToString()).FirstOrDefault();

            if (filterRow == null)
                return;

            this.data.SuggestionFilter.RemoveSuggestionFilterRow(filterRow);

            this.data.SuggestionFilter.AcceptChanges();

        }

        private void UpdateFilter()
        {
            if (this.SkipFilter)
                return;

            ModelDS.SuggestionInputRow inputRow = this.data.SuggestionInput.FirstOrDefault();

            if (inputRow.IsIdNull())
                return;

            ModelDS.SuggestionFilterRow filterRow = this.data.SuggestionFilter.Where(fr => fr.Id == inputRow.Id && fr.Key == this.SuggestionKey.ToString()).FirstOrDefault();

            if (filterRow == null)
            {
                filterRow = this.data.SuggestionFilter.NewSuggestionFilterRow();
                this.data.SuggestionFilter.AddSuggestionFilterRow(filterRow);
            }

            
           filterRow.Id = inputRow.Id;
           filterRow.Key = this.SuggestionKey.ToString();
           filterRow.ItemValue = this.InputValue;
           filterRow.AcceptChanges();
                

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

                ManageInput();
            }
        }
        public string InputValue { set; get; }

        public bool SkipFilter { get; set; }
       

    }
}
