using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;
namespace Maklak.Models
{
    public static class SiteMapHelper
    {
        public static ISiteMap SiteMap
        {
            get
            {
                return SiteMaps.Current;
            }
        }

        public static ISiteMapNode NodeByKey(string key)
        {
            return SiteMapHelper.SiteMap.FindSiteMapNodeFromKey(key);
        }

        

        public static string RootController {
            get { return SiteMapHelper.SiteMap.RootNode.Controller; }
        }
        public static string RootAction
        {
            get { return SiteMapHelper.SiteMap.RootNode.Action; }
        }
        

        //private static ISiteMapNode NodeByAttributeValue(ISiteMapNode node, string attributeName, string attributeValue)
        //{
        //    if (node == null)
        //        return null;
            
        //    if (node.Attributes.Where(a => a.Key == attributeName && Convert.ToString(a.Value) == attributeValue).Any())
        //        return node;

        //    if (!node.HasChildNodes)
        //        return null;

        //    return node.ChildNodes.Where(n => SiteMapHelper.NodeByAttributeValue(n, attributeName, attributeValue) != null).FirstOrDefault();               
        //}        
        
    }
}
