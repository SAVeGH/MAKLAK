using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public static class TabModelHelper
    {        

        public enum TabModelType { VERTICAL, LOGIN, INOUT, SEARCH, MANAGE }

        public static TabModel GenerateModel(TabModelType modelType)
        {
            switch (modelType)
            {
                case TabModelType.VERTICAL:
                    return new TabVModel();
                case TabModelType.LOGIN:
                    return new LoginModel();
            }

            return null;
        }
    }
}
