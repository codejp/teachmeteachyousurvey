using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TeachMeTeachYouSurvey.Models
{
    public class Theme
    {
        [Key]
        public Guid ThemeId { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<ThemeType>))]
        public int ThemeType { get; set; }

        public string Owner { get; set; }

        public DateTime CreateAt { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public Theme()
        {
            this.ThemeId = Guid.NewGuid();
            this.CreateAt = DateTime.UtcNow;
            this.Description = "";
            this.Owner = "";
        }
    }
}