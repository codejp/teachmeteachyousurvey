using System;
using System.Linq;
using System.Web.Http;
using TeachMeTeachYouSurvey.Models;

namespace TeachMeTeachYouSurvey.Controllers
{
    public class ODataController : ApiController
    {
        private TeachMeTeachYouDB db;

        public ODataController()
        {
            this.db = new TeachMeTeachYouDB();
            this.db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/ThemeApi
        [Queryable]
        public IQueryable<Theme> GetThemes()
        {
            return db.Themes.Include("Votes");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}