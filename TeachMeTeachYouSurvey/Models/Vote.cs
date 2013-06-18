using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TeachMeTeachYouSurvey.Models
{
    public class Vote
    {
        [Key]
        public Guid VoteId { get; set; }

        public Guid ThemeId { get; set; }

        public int VoteType { get; set; }

        public string Owner { get; set; }

        public DateTime CreateAt { get; set; }

        public virtual Theme Theme { get; set; }

        public Vote()
        {
            this.VoteId = Guid.NewGuid();
            this.CreateAt = DateTime.UtcNow;
        }
    }
}