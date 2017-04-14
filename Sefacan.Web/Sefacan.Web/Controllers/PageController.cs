using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Sefacan.Core.Entities;
using Sefacan.Core.Helpers;
using Sefacan.Framework.Controllers;
using Sefacan.Framework.Paging;
using Sefacan.Service;
using Sefacan.Web.Models;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sefacan.Web.Controllers
{
    public class PageController : BaseController
    {
        #region Fields
        private readonly ISettingService settingService;
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IUrlService urlService;
        private readonly ILocalService localService;
        private readonly IProjectService projectService;
        #endregion

        #region Ctor
        public PageController(ISettingService _settingService,
            IPostService _postService,
            ICategoryService _categoryService,
            IUrlService _urlService,
            ILocalService _localService,
            IProjectService _projectService)
        {
            settingService = _settingService;
            postService = _postService;
            categoryService = _categoryService;
            urlService = _urlService;
            localService = _localService;
            projectService = _projectService;
        }
        #endregion

        #region Methods
        public ActionResult Search(string term, int page = 1)
        {
            if (string.IsNullOrEmpty(term))
                return HomePage();

            int pageSize = settingService.GetSetting("general.pagesize").IntValue;
            var posts = postService.SearchPosts(term).Select(x => new PostModel
            {
                Title = x.Title,
                PicturePath = x.PicturePath,
                ShortContent = x.ShortContent,
                CreateDate = x.CreateDate,
                Url = urlService.GetUrl(x.Id, Core.Enums.EntityType.Post),
                CommentCount = postService.GetCommentCount(x.Id),
                CategoryName = categoryService.GetById(x.CategoryId).Name,
                CategoryUrl = urlService.GetUrl(x.CategoryId, Core.Enums.EntityType.Category),
                ViewCount = x.ViewCount
            }).ToPagedList(page - 1, pageSize);

            ViewBag.term = term;

            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.About = localService.GetByName("general.about");
            return View();
        }

        public ActionResult Project()
        {
            var model = CommonHelper.MapTo<Project, ProjectModel>(projectService.GetProjects());
            return View(model);
        }

        #region Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            string mailTitle = settingService.GetSetting("mail.message.title").Value;
            string sender = settingService.GetSetting("mail.sender.address").Value;
            string password = settingService.GetSetting("mail.sender.password").Value;
            string host = settingService.GetSetting("mail.smtp.host").Value;
            int port = settingService.GetSetting("mail.smtp.port").IntValue;
            bool useSSL = settingService.GetSetting("mail.smtp.ssl").BoolValue;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailTitle, model.Email));
            message.To.Add(new MailboxAddress(model.Name, sender));
            message.Subject = mailTitle;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = PrepareMailBody(model)
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(host, port, useSSL);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(sender, password);

                client.Send(message);
                client.Disconnect(true);
            }

            return View();
        }

        private string PrepareMailBody(ContactModel model)
        {
            string filePath = Server.MapPath("~/App_Data/EmailBody.html");
            string body = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
            body = body.Replace("{{name}}", model.Name);
            body = body.Replace("{{email}}", model.Email);
            body = body.Replace("{{message}}", model.Message);

            return body;
        }
        #endregion

        #endregion
    }
}