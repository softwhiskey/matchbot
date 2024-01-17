using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kinmatch.Entities
{
    public class Profile
    {
        [Key]
        public int? id { get; set; }
        public long? tg_id { get; set; }
        public string? username { get; set; }
        public int? age { get; set; }
        public int? gender { get; set; }
        public int? search_gender { get; set; }
        public int? goal { get; set; }
        public int? search_goal { get; set; }
        public string? region { get; set; }
        public int? region_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? interests { get; set; }
        public int? zodiac { get; set; }
        public string? media { get; set; }
        public string? instagram { get; set; }
        public int? spotify_id { get; set; }
        public double? coins { get; set; }
        public int? boosts { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public int? today_likes { get; set; }
        public int? total_likes { get; set; }
        public int? total_matches { get; set; }
        public int? banned { get; set; }
        public int? subscription_type { get; set; }
        public int? subscription_until { get; set; }
        public string? language { get; set; }
        public int? last_id { get; set; }
        public int? referer { get; set; }
        public int? active { get; set; }
        public int? date_created { get; set; }
        public string? action { get; set; }

        public Profile(int? id, long? tg_id, string? username, int? age, int? gender, int? search_gender, int? goal, int? search_goal,
               string? region, int? region_id, string? name, string? description, string? interests, int? zodiac, string? media,
               string? instagram, int? spotify_id, double? coins, int? boosts, decimal? latitude, decimal? longitude, int? today_likes,
               int? total_likes, int? total_matches, int? banned, int? subscription_type, int? subscription_until, string? language,
               int? last_id, int? referer, int? active, int? date_created, string? action)
        {
            this.id = id;
            this.tg_id = tg_id;
            this.username = username;
            this.age = age;
            this.gender = gender;
            this.search_gender = search_gender;
            this.goal = goal;
            this.search_goal = search_goal;
            this.region = region;
            this.region_id = region_id;
            this.name = name;
            this.description = description;
            this.interests = interests;
            this.zodiac = zodiac;
            this.media = media;
            this.instagram = instagram;
            this.spotify_id = spotify_id;
            this.coins = coins;
            this.boosts = boosts;
            this.latitude = latitude;
            this.longitude = longitude;
            this.today_likes = today_likes;
            this.total_likes = total_likes;
            this.total_matches = total_matches;
            this.banned = banned;
            this.subscription_type = subscription_type;
            this.subscription_until = subscription_until;
            this.language = language;
            this.last_id = last_id;
            this.referer = referer;
            this.active = active;
            this.date_created = date_created;
            this.action = action;
        }
        public Profile(long tg_id)
        {
            this.tg_id = tg_id;
            this.username = null;
            this.age = null;
            this.gender = null;
            this.search_gender = null;
            this.goal = 0;
            this.search_goal = 0;
            this.region = null;
            this.region_id = null;
            this.name = null;
            this.description = null;
            this.interests = null;
            this.zodiac = null;
            this.media = null;
            this.instagram = null;
            this.spotify_id = null;
            this.coins = 0;
            this.boosts = 0;
            this.latitude = null;
            this.longitude = null;
            this.today_likes = 0;
            this.total_likes = 0;
            this.total_matches = 0;
            this.banned = 0;
            this.subscription_type = 0;
            this.subscription_until = 0;
            this.language = "ru";
            this.last_id = 0;
            this.referer = null;
            this.active = 0;
            this.date_created = (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.action = null;
        }

    }
}