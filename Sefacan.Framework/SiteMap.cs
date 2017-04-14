using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sefacan.Framework
{
    public class SiteMap
    {
        public static string Generate(IEnumerable<SitemapItem> items)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace schemaLocation = "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", ""),
                new XElement(xmlns + "urlset",
                new XAttribute(xsi + "schemaLocation", schemaLocation),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi)));

            foreach (var item in items)
            {
                XElement i = new XElement
                (
                    xmlns + "url",
                    new XElement(xmlns + "loc", item.loc),
                    new XElement(xmlns + "lastmod", item.lastmod.ToString("yyyy-MM-dd")),
                    new XElement(xmlns + "changefreq", item.changefreq)
                    //new XElement(xmlns + "priority", item.priority)
                );
                doc.Root.Add(i);
            }

            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms, new XmlWriterSettings() { Indent = true });
            doc.Save(xw);
            xw.Close();
            return new UTF8Encoding().GetString(ms.ToArray());
        }
    }

    public class SitemapItem
    {
        public string loc { get; set; }
        public DateTime lastmod { get; set; }
        public string changefreq { get; set; }
        //public string priority { get; set; }
    }
}