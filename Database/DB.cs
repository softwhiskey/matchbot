using Kinmatch.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinmatch.Database
{
    internal class DB
    {
        public async Task<Profile> GetProfile(Context dbContext, long tg_id)
        {
            return await dbContext?.profiles?.FirstOrDefaultAsync(p => p.tg_id == tg_id);
        }
        public async Task<bool> CreateProfile(Context dbContext, Profile profile)
        {
            dbContext.profiles.Add(profile);
            int rowsAffected = await dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateFullProfile(Context dbContext, Profile profile)
        {
            dbContext.profiles.Update(profile);
            int rowsAffected = await dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateProfileColumn(Context dbContext, string column, object value, long? tg_id)
        {
            var profile = await dbContext.profiles.FirstOrDefaultAsync(p => p.tg_id == tg_id);
            if (profile == null)
                return false;

            var property = typeof(Profile).GetProperty(column);
            if (property != null)
            {
                if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                {
                    property.SetValue(profile, (int)value);
                }
                else
                {
                    property.SetValue(profile, value);
                }

                int rowsAffected = await dbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }
            return false;
        }





    }
}
