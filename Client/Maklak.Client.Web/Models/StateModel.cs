using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	public class StateModel
	{
		//public string SID { get; set; }

		Maklak.Client.DataSets.FilterDS filterDS;

		public  StateModel() 
		{
			filterDS = new FilterDS();
		}

		public Maklak.Client.DataSets.FilterDS SearchData 
		{
			get { return this.filterDS; }			
		}

		public void AddSearchValue(string inputName, string inputValue) 
		{
			FilterDS.SearchInputRow row = filterDS.SearchInput.FirstOrDefault(r => r.InputName == inputName); 

			if (row == null)
			{
				row = filterDS.SearchInput.NewSearchInputRow();
				row.InputName = inputName;
				filterDS.SearchInput.AddSearchInputRow(row);
			}
			
			row.InputValue = inputValue;
			
		}

	}
}
