using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Sude.Mvc.UI.Admin.Models;
using System.Collections.Generic;

namespace Sude.Mvc.UI.Menu
{
    /// <summary>
    /// XML sitemap
    /// </summary>
    public class XmlUsers
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public XmlUsers()
        {
            Users = new List<UserInfo>();
        }
        public List<UserInfo> Users
        {
            get;
            set;

        }

        public virtual void LoadFrom(string physicalPath, IWebHostEnvironment hostingEnvironment)
        {

            Users = new List<UserInfo>();
            var filePath = hostingEnvironment.ContentRootPath + physicalPath;
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

                    if (doc.DocumentElement.HasChildNodes)
                        foreach (XmlNode xmlNode in doc.DocumentElement.ChildNodes)
                        {
                            UserInfo user = ReadUser(xmlNode);
                            Users.Add(user);
                        }



                }
            }
        }



        private static UserInfo ReadUser(XmlNode xmlNode)
        {
            UserInfo user = new UserInfo();
            user.id = GetStringValueFromAttribute(xmlNode, "id");
            user.name = GetStringValueFromAttribute(xmlNode, "name");
            user.password = GetStringValueFromAttribute(xmlNode, "password");
            user.phoneNumber = GetStringValueFromAttribute(xmlNode, "phoneNumber");
            user.userName = GetStringValueFromAttribute(xmlNode, "userName");
            user.email = GetStringValueFromAttribute(xmlNode, "email");
            return user;
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
