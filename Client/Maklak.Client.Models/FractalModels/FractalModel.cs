using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Client.Models
{
    public class FractalModel : BaseModel
    {
        public enum DOKPOSITION { LEFT, TOP, RIGHT, BOTTOM };

        // данные параметры не меняются при рекурсивных запросах
        private const string FRACTAL_CONTROLLER = "Fractal";
        private const string FRACTAL_CONTROL_ACTION = "FractalControl";
        private const string FRACTAL_CONTENT_ACTION = "FractalContent";
        private const string FRACTAL_PANEL_ACTION = "FractalPanel";

        private string key;

        public FractalModel()
        {
            this.OnModelReady += FractalModel_OnModelReady;
        }

        private void FractalModel_OnModelReady()
        {
            key = DefaultKey(); // ставится при создании модели
        }

        private string DefaultKey()
        {
            DataSets.ModelDS.FractalDataRow rootRow = data.FractalData.Where(r => r.IsParent_IdNull()).FirstOrDefault();
            DataSets.ModelDS.FractalDataRow keyRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == rootRow.Id && r.Active).FirstOrDefault();

            if (keyRow == null)
                keyRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == rootRow.Id && r.IsDefault).FirstOrDefault();

            if (keyRow == null)
                return string.Empty;

            return keyRow.Key; // ставим ключь по умолчания как если бы на нём был клик на клиенте

        }


        // определяет положение TabPanel
        public DOKPOSITION DokPosition
        {
            get
            {
                if (string.IsNullOrEmpty(ParentKey))
                    return DOKPOSITION.LEFT;

                DataSets.ModelDS.FractalDataRow row = data.FractalData.Where(r => r.Key == this.ParentKey).FirstOrDefault();

                if (row == null)
                    return DOKPOSITION.LEFT; // для CATEGORY


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
                // т.к. устанавливается при привязке - приходят только ключи на которых был клик
                key = value;

                SetActiveKey(); // сразу делаем активным ключь на котором был клик
            }
        }

        private void SetActiveKey()
        {
            DataSets.ModelDS.FractalDataRow row = data.FractalData.Where(r => r.Key == this.Key).FirstOrDefault();

            if (row == null)
                return;


            DataSets.ModelDS.FractalDataRow activeRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Parent_Id && r.Active).FirstOrDefault();

            if (activeRow != null)
                activeRow.Active = false;

            row.Active = true;

        }

        // ключ определяющий положение FractalPanel совпадает с ключом модели для Control
        public string ParentKey
        {
            get
            {
                DataSets.ModelDS.FractalDataRow row = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Key == this.Key).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                DataSets.ModelDS.FractalDataRow parentRow = data.FractalData.Where(r => r.Id == row.Parent_Id).FirstOrDefault();

                if (parentRow == null)
                    return string.Empty;

                return parentRow.Key;
            }
        }

        public string ControlAction
        {
            get
            {
                DataSets.ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == this.ParentKey).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Action;
            }
        }

        public string ControlController
        {
            get
            {
                DataSets.ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == this.ParentKey).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Controller;
            }
        }

        public string ContentAction
        {
            get
            {
                DataSets.ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == this.Key).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Action;
            }
        }

        public string ContentController
        {
            get
            {
                DataSets.ModelDS.SiteMapRow row = data.SiteMap.Where(r => r.Key == this.Key).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Controller;
            }
        }

        public bool HasChildPanel
        {
            get
            {
                DataSets.ModelDS.FractalDataRow keyRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Key == this.Key).FirstOrDefault();

                return data.FractalData.Any(r => !r.IsParent_IdNull() && r.Parent_Id == keyRow.Id);
            }
        }

        public string NextKey
        {
            get
            {
                DataSets.ModelDS.FractalDataRow keyRow = ChildDataRow();

                if (keyRow == null)
                    return string.Empty;

                return keyRow.Key;
            }
        }

        private DataSets.ModelDS.FractalDataRow ChildDataRow()
        {
            DataSets.ModelDS.FractalDataRow row = data.FractalData.Where(r => r.Key == Key).FirstOrDefault();
            // сначала ищем активную модель
            DataSets.ModelDS.FractalDataRow keyRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.Active).FirstOrDefault();

            // если не нашли - берём модель по умолчанию
            if (keyRow == null)
                keyRow = data.FractalData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == row.Id && r.IsDefault).FirstOrDefault();

            return keyRow;
        }

        public string ClassPrefix
        {
            get
            {
                string classPrefix = string.Empty;

                switch (this.DokPosition)
                {
                    case Maklak.Client.Models.FractalModel.DOKPOSITION.LEFT:
                        classPrefix = "left";
                        break;
                    case Maklak.Client.Models.FractalModel.DOKPOSITION.TOP:
                        classPrefix = "top";
                        break;
                    case Maklak.Client.Models.FractalModel.DOKPOSITION.RIGHT:
                        classPrefix = "right";
                        break;
                    case Maklak.Client.Models.FractalModel.DOKPOSITION.BOTTOM:
                        classPrefix = "bottom";
                        break;
                }

                return classPrefix;
            }
        }

        private string ControlClassBody
        {
            get
            {
                string classBody = this.IsLeaf ? "MainControl" : "ControlArea";                

                return classBody;
            }
        }

        private string ContentClassBody
        {
            get
            {
                string classBody = this.IsLeaf ? "MainContent" : "ContentArea";                

                return classBody;
            }
        }

        public string ControlClass
        {
            get
            {
                string stripClass = string.Format("{0}{1}", this.ClassPrefix, this.ControlClassBody);
                return stripClass;

            }
        }

        public string ContentClass
        {
            get
            {
                string contentClass = string.Format("{0}{1}", this.ClassPrefix, this.ContentClassBody);
                return contentClass;

            }
        }

        public bool IsRoot
        {
            get
            {
                // узел у которого заполнено поле RecursiveController                
                return this.data.SiteMap.Where(r => !r.IsRecursiveControllerNull() && r.Key == this.ParentKey).Any();                
            }
        }

        public bool IsLeaf
        {
            get
            {
                // узлы не имеющие дочерних узлов
                int rowId = this.data.SiteMap.Where(r => r.Key == this.Key).Select(rs => rs.Id).FirstOrDefault();
                return !this.data.SiteMap.Where(r => !r.IsParent_IdNull() && r.Parent_Id == rowId).Any();                
            }
        }

        public string FractalController
        {
            get { return FRACTAL_CONTROLLER; }
        }

        public string FractalControlAction
        {
            get { return FRACTAL_CONTROL_ACTION; }
        }

        public string FractalContentAction
        {
            get { return FRACTAL_CONTENT_ACTION; }
        }

        public string FractalPanelAction
        {
            get { return FRACTAL_PANEL_ACTION; }
        }
    }
}
