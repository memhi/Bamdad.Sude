using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Sude.Mvc.UI.Menu
{
    /// <summary>
    /// XML sitemap
    /// </summary>
    public class XmlMenu
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public XmlMenu()
        {
            RootNode = new MenuNode();
        }

        /// <summary>
        /// Root node
        /// </summary>
        public MenuNode RootNode { get; set; }

        /// <summary>
        /// Load sitemap
        /// </summary>
        /// <param name="physicalPath">Filepath to load a sitemap</param>
        public virtual void LoadFrom(string physicalPath,IWebHostEnvironment hostingEnvironment)
        {
          

            var filePath = hostingEnvironment.ContentRootPath+physicalPath;
            var content = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

            if (!string.IsNullOrEmpty(content))
            {
                var doc = new XmlDocument();
                using (var sr = new StringReader(content))
                {
                    using var xr = XmlReader.Create(sr,
                        new XmlReaderSettings
                        {
                            CloseInput = true,
                            IgnoreWhitespace = true,
                            IgnoreComments = true,
                            IgnoreProcessingInstructions = true
                        });

                    doc.Load(xr);
                }
                if ((doc.DocumentElement != null) && doc.HasChildNodes)
                {
                    var xmlRootNode = doc.DocumentElement.FirstChild;
                    Iterate(RootNode, xmlRootNode);
                }
            }
        }

        private static void Iterate(MenuNode menuNode, XmlNode xmlNode)
        {
            PopulateNode(menuNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("menuNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new MenuNode();
                    menuNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        private static void PopulateNode(MenuNode menuNode, XmlNode xmlNode)
        {

            menuNode.SystemName = GetStringValueFromAttribute(xmlNode, "SystemName");

            //title
            var Resource = GetStringValueFromAttribute(xmlNode, "Resource");

            menuNode.Title = Resource;

            //routes, url
            var controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            var actionName = GetStringValueFromAttribute(xmlNode, "action");
            var url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                menuNode.ControllerName = controllerName;
                menuNode.ActionName = actionName;

                //apply admin area as described here - https://www.nopcommerce.com/boards/topic/20478/broken-menus-in-admin-area-whilst-trying-to-make-a-plugin-admin-page
                menuNode.RouteValues = new RouteValueDictionary { {  "area", "Admin" } };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                menuNode.Url = url;
            }

            //image URL
            menuNode.IconClass = GetStringValueFromAttribute(xmlNode, "IconClass");

            //permission name
            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {
                menuNode.Visible = true;
                //var permissionService = EngineContext.Current.Resolve<IPermissionService>();
                //menuNode.Visible = permissionNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                //   .Any(permissionName => permissionService.Authorize(permissionName.Trim()));
            }
            else
            {
                menuNode.Visible = true;
            }

            // Open URL in new tab
            var openUrlInNewTabValue = GetStringValueFromAttribute(xmlNode, "OpenUrlInNewTab");
            if (!string.IsNullOrWhiteSpace(openUrlInNewTabValue) && bool.TryParse(openUrlInNewTabValue, out var booleanResult))
            {
                menuNode.OpenUrlInNewTab = booleanResult;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                var attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
    }
}
