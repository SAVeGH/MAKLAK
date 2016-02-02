using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public static class TabModelHelper
    {
        public static TabModel GenerateModel(string modelCode)
        {
            switch (modelCode)
            {
                case "VM":
                    return new TabVModel();
                case "HM":
                    return new TabHModel();
            }

            return null;
        }
    }
}
