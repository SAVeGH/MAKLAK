using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace Maklak.Proxy
{
    public abstract class ProxyBase : IDisposable
    {
        #region Переменные

        protected ICommunicationObject Client;
        private bool disposed;

        #endregion

        #region Конструктор

        public ProxyBase()
        {
            this.Client = CreateProxy();
            this.Client.Faulted += new EventHandler(Client_Faulted);
            disposed = false;
        }

        #endregion

        #region Деструктор

        ~ProxyBase()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region Метод void Dispose(bool disposeState)

        protected void Dispose(bool disposeState)
        {
            if (this.disposed)            
                return;            

            this.disposed = true;

            Close();

            if (!disposeState)            
                GC.SuppressFinalize(this);
            
        }

        #endregion

        #region Проверка и открытие/закрытие соединения
        /// <summary>
        /// Проверка и закрытие соединения
        /// </summary>
        protected void Close()
        {
            if (this.Client == null)            
                return;
            
            if (this.Client.State == System.ServiceModel.CommunicationState.Closed ||
                this.Client.State == System.ServiceModel.CommunicationState.Faulted ||
                this.Client.State == System.ServiceModel.CommunicationState.Closing)            
                return;            

            this.Client.Close();
        }

        /// <summary>
        /// Проверка и открытие соединения
        /// </summary>
        protected void Open()
        {
            if (this.Client == null)            
                throw new NullReferenceException("ProxyBase: Proxy class not initialized");
            
            if (this.Client.State == System.ServiceModel.CommunicationState.Opened ||
                this.Client.State == System.ServiceModel.CommunicationState.Opening)            
                return;
            
            this.Client.Open();
        }

        #endregion

        #region Возникает, когда канал переходит в faulted state

        private void Client_Faulted(object sender, EventArgs e)
        {
            this.Client.Abort();
            this.Client = CreateProxy();
            this.Client.Faulted += new EventHandler(Client_Faulted);
        }

        #endregion

        #region Абстрактные методы
        /// <summary>
        /// Создаёт экзепляр прокси-класса для текущего наследника
        /// </summary>
        /// <returns></returns>
        protected abstract ICommunicationObject CreateProxy();

        #endregion
    }
}
