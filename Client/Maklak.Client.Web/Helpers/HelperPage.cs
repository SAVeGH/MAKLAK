using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.WebPages;

namespace Maklak.Client.Web.Helpers
{
    public class HelperPage : System.Web.WebPages.HelperPage
    {
        //Переопределение свойства Html для того что бы в хелперах можно было использовать
        // стандартные хелперы (TextBox()...). В custom хелпере нужно наследоваться от этой страницы
        public static new HtmlHelper Html
        {
            get
            {
                return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Html;
            }
        }

        public static UrlHelper Url
        {
            get
            {
                return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Url;
            }
        }
    }
}