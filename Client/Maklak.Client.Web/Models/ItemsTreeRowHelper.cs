using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	public class ItemsTreeRowHelper
	{
		ItemsTreeDS.ItemsDataTable innerTable;
		public ItemsTreeRowHelper() 
		{
			Clear();
		}

		public ItemsTreeRowHelper(ItemsTreeDS.ItemsRow row) : this()
		{
			Row = row;
		}

		public void Clear() 
		{
			if(innerTable != null)
				innerTable.Clear();

			innerTable = new ItemsTreeDS.ItemsDataTable();
		}

		public ItemsTreeDS.ItemsRow Row
		{
			get
			{
				return innerTable.FirstOrDefault();
			}

			set 
			{
				innerTable.Clear();
				innerTable.ImportRow(value);
			}
		}
	}
}
