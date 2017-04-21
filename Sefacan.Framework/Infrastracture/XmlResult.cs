using Sefacan.Core.Infrastructure;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sefacan.Framework.Infrastructure
{
    public class XmlResult : ActionResult
    {
        private readonly XDocument _document;

        public XmlResult(XDocument document)
        {
            _document = document;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            context.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
            context.HttpContext.Response.ContentType = MimeTypes.TextXml;

            XmlSerializer serializer = new XmlSerializer(_document.GetType());
            serializer.Serialize(context.HttpContext.Response.Output, _document);
        }
    }
}
