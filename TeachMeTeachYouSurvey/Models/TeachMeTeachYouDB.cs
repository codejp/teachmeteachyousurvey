using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TeachMeTeachYouSurvey.Models
{
    public class TeachMeTeachYouDB : DbContext
    {
        public DbSet<Theme> Themes { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}