using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TeachMeTeachYouSurvey.Code;
using TeachMeTeachYouSurvey.Models;
using TeachMeTeachYouSurvey.Views;

namespace TeachMeTeachYouSurvey.Controllers
{
    public class RSSController : Controller
    {
        public TeachMeTeachYouDB DB { get; set; }

        public RSSController()
        {
            this.DB = new TeachMeTeachYouDB();
        }

        private Uri GetAppUrl()
        {
            return new Uri(Request.Url.GetLeftPart(UriPartial.Scheme | UriPartial.Authority));
        }

        public ActionResult Index()
        {
            var syndicationItems =
                this.DB.Themes.Select(ToSyndicationItem)
                .Concat(this.DB.Votes.Include("Theme").Select(ToSyndicationItem))
                .OrderByDescending(s => s.LastUpdatedTime);

            var feed = new SyndicationFeed(_Localize.SiteTitle, "", this.GetAppUrl(), syndicationItems.ToArray());
            var formatter = new Atom10FeedFormatter(feed);

            return new LamdaResult(context =>
            {
                var response = context.HttpContext.Response;
                response.ContentType = "application/xml";
                var xmlWriter = XmlWriter.Create(response.OutputStream);
                formatter.WriteTo(xmlWriter);
                xmlWriter.Flush();
            });
        }

        private SyndicationItem ToSyndicationItem(Theme theme)
        {
            var resMan = _Localize.ResourceManager;
            return new SyndicationItem(
                title: string.Format("[{0}] テーマに新しい投稿がありました",
                    resMan.GetString(((ThemeType)theme.ThemeType).ToString())),
                content: theme.Description,
                itemAlternateLink: this.GetAppUrl(),
                id: theme.ThemeId.ToString("N"),
                lastUpdatedTime: theme.CreateAt
                );
        }

        private SyndicationItem ToSyndicationItem(Vote vote)
        {
            var resMan = _Localize.ResourceManager;
            return new SyndicationItem(
                title: string.Format("以下の [{0}] テーマに [{1}] の投票がありました",
                    resMan.GetString(((VoteType)vote.VoteType).ToString()),
                    resMan.GetString(((ThemeType)vote.Theme.ThemeType).ToString())),
                content: vote.Theme.Description,
                itemAlternateLink: this.GetAppUrl(),
                id: vote.VoteId.ToString("N"),
                lastUpdatedTime: vote.CreateAt
                );
        }
    }
}
