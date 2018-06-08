using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Maklak.Client.Proxy;
using Maklak.Client.DataSets;

namespace Maklak.Client.Data
{
    public class DataSource
    {
        Proxy.DataSource dataSource;
		ModelDS modelDS;

		public DataSource(ModelDS clientModelDS)
        {
			// класс который объединяет клиентскую модель и данные из proxy
            dataSource = new Proxy.DataSource();
			modelDS = clientModelDS;

		}

        public void DoWork()
        {
            dataSource.DoWork();
        }

        public void MakeSuggestion(/*ModelDS modelDS*/)
        {
            Proxy.DataSourceServiceReference.SuggestionDS inputDS = new Proxy.DataSourceServiceReference.SuggestionDS();

            ModelDS.SuggestionInputRow inputRow = Model.SuggestionInput.FirstOrDefault();
            inputRow.SetIdNull();
            inputRow.AcceptChanges();

            inputDS.SuggestionInput.ImportRow(inputRow);

			Model.SuggestionFilter.AsEnumerable().ToList().ForEach(r => inputDS.SuggestionFilter.ImportRow(r));              

            Proxy.DataSourceServiceReference.SuggestionDS  outputDS = dataSource.MakeSuggestion(inputDS);

            Proxy.DataSourceServiceReference.SuggestionDS.SuggestionInputRow outRow = outputDS.SuggestionInput.FirstOrDefault();

            if (!outRow.IsIdNull())
                inputRow.Id = outRow.Id;

			Model.Suggestion.Clear();
                        
            outputDS.Suggestion.AsEnumerable().ToList().ForEach(r => Model.Suggestion.ImportRow(r));

			Model.AcceptChanges();

        }

		public ModelDS.TreeItemRow FillNode(int branchID, int nodeID)
		{
			Proxy.DataSourceServiceReference.TreeDS inputTreeDS = new Proxy.DataSourceServiceReference.TreeDS();

			Proxy.DataSourceServiceReference.TreeDS.RootNodeDataRow inputRootRow = inputTreeDS.RootNodeData.NewRootNodeDataRow();

			inputRootRow.BranchID = branchID;
			inputRootRow.NodeID = nodeID;

			Proxy.DataSourceServiceReference.TreeDS treeDS = dataSource.ConstructTree(inputTreeDS);

			this.Model.TreeItem.Clear();

			ModelDS.TreeItemRow rootRow = null;

			foreach (Proxy.DataSourceServiceReference.TreeDS.TreeRow row in treeDS.Tree.Rows)
			{

				ModelDS.TreeItemRow tiRow = this.Model.TreeItem.NewTreeItemRow();
				tiRow.Id = row.Id;
				if (row.IsBranch_IdNull())
					tiRow.SetBranch_IdNull();
				else
					tiRow.Branch_Id = row.Branch_Id;

				if (row.IsParent_IdNull())
				{
					tiRow.SetParent_IdNull();
					rootRow = tiRow;
					rootRow.Expanded = true;
					rootRow.Visible = false;
					rootRow.UseFilterPanel = false;
					rootRow.UseSelectionPanel = false;
				}
				else
					tiRow.Parent_Id = row.Parent_Id;

				if (row.IsParentBranch_IdNull())
					tiRow.SetParentBranch_IdNull();
				else
					tiRow.ParentBranch_Id = row.ParentBranch_Id;



				tiRow.Name = row.Name;
				tiRow.Selected = row.IsSelectedNull() ? false : row.Selected;
				this.Model.TreeItem.AddTreeItemRow(tiRow);
			}

			return rootRow;
		}

		public void ConstructTree(/*ModelDS modelDS*/)
		{
			Proxy.DataSourceServiceReference.TreeDS treeDS = dataSource.ConstructTree(null);

			this.Model.TreeItem.Clear();

			foreach (Proxy.DataSourceServiceReference.TreeDS.TreeRow row in treeDS.Tree.Rows)
			{
				
				ModelDS.TreeItemRow tiRow = this.Model.TreeItem.NewTreeItemRow();
				tiRow.Id = row.Id;
				if (row.IsBranch_IdNull())
					tiRow.SetBranch_IdNull();
				else
					tiRow.Branch_Id = row.Branch_Id;

				if (row.IsParent_IdNull())
					tiRow.SetParent_IdNull();
				else
					tiRow.Parent_Id = row.Parent_Id;

				if (row.IsParentBranch_IdNull())
					tiRow.SetParentBranch_IdNull();
				else
					tiRow.ParentBranch_Id = row.ParentBranch_Id;

				
				 
				tiRow.Name = row.Name;
				tiRow.Selected = row.IsSelectedNull() ? false : row.Selected;
				this.Model.TreeItem.AddTreeItemRow(tiRow);
			}
		}

		public ModelDS Model
		{
			get
			{
				return this.modelDS;
			}
		}
	}
}
