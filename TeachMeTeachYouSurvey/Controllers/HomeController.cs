using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeachMeTeachYouSurvey.Models;

namespace TeachMeTeachYouSurvey.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public TeachMeTeachYouDB DB { get; set; }

        public HomeController()
        {
            this.DB = new TeachMeTeachYouDB();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var themes = this.DB.Themes.Include("Votes").ToArray();
            var themeTypes = new[] { ThemeType.TeachMe, ThemeType.TeachYou };
            var model = new IndexViewModel
            {
                ThemesPerType = themeTypes.ToDictionary(
                    keySelector: t => t.ToString(),
                    elementSelector: t => themes.Where(a => a.ThemeType == (int)t).OrderByDescending(a => a.CreateAt))
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Post(string themeType)
        {
            return View(new ThemeEditorViewModel
            {
                ThemeType = Enum<ThemeType>.Parse(themeType)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Post(string themeType, ThemeEditorViewModel model)
        {
            model.ThemeType = Enum<ThemeType>.Parse(themeType);

            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            this.DB.Themes.Add(new Theme
            {
                Owner = User.Identity.UserId(),
                ThemeType = (int)Enum<ThemeType>.Parse(themeType),
                Description = model.Description
            });
            this.DB.SaveChanges();
            return Redirect("~/");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var userId = User.Identity.UserId();
            var theme = this.DB.Themes.First(t => t.ThemeId == id && t.Owner == userId);
            return View(new ThemeEditorViewModel
            {
                ThemeType = (ThemeType)theme.ThemeType,
                Description = theme.Description
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ThemeEditorViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var userId = User.Identity.UserId();
            var theme = this.DB.Themes.First(t => t.ThemeId == id && t.Owner == userId);
            theme.Description = model.Description;
            this.DB.SaveChanges();

            return Redirect("~/");
        }


        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var userId = User.Identity.UserId();
            var theme = this.DB.Themes.FirstOrDefault(t => t.ThemeId == id && t.Owner == userId);

            if (theme != null)
            {
                this.DB.Themes.Remove(theme);
                this.DB.SaveChanges();
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Vote(Guid id)
        {
            var userId = User.Identity.UserId();
            var theme = this.DB.Themes
                .Include("Votes")
                .FirstOrDefault(t => t.ThemeId == id && t.Owner != userId);
            if (theme != null)
            {
                if (theme.Votes.Any(v => v.Owner == userId) == false)
                {
                    theme.Votes.Add(new Vote
                    {
                        Owner = userId,
                        ThemeId = theme.ThemeId
                    });
                    this.DB.SaveChanges();
                }
                return PartialView("_Theme", theme);
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Revert(Guid id)
        {
            var userId = User.Identity.UserId();
            var theme = this.DB.Themes
                .Include("Votes")
                .FirstOrDefault(t => t.ThemeId == id && t.Owner != userId);
            if (theme != null)
            {
                var vote = theme.Votes.FirstOrDefault(v => v.Owner == userId);
                if (vote != null)
                {
                    theme.Votes.Remove(vote);
                    this.DB.Votes.Remove(vote);
                    this.DB.SaveChanges();
                }
                return PartialView("_Theme", theme);
            }

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult AboutThisSite()
        {
            return View(viewName: "MarkdownPage");
        }

        [AllowAnonymous]
        public ActionResult CrashTest()
        {
            throw new ApplicationException();
        }
    }
}
