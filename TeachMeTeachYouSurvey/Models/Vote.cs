using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TeachMeTeachYouSurvey.Models
{
    [Serializable, DataContract]
    public class Vote
    {
        [Key, DataMember]
        public Guid VoteId { get; set; }

        [JsonIgnore]
        public Guid ThemeId { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<VoteType>)), DataMember]
        public int VoteType { get; set; }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public DateTime CreateAt { get; set; }

        [JsonIgnore]
        public virtual Theme Theme { get; set; }

        public Vote()
        {
            this.VoteId = Guid.NewGuid();
            this.CreateAt = DateTime.UtcNow;
        }
    }
}