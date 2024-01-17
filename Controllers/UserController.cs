using Google.Protobuf.WellKnownTypes;
using Kinmatch.Database;
using Kinmatch.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinmatch.Controllers
{
    /// <summary>
    /// Контроллер пользователя, запросы к БД.
    /// </summary>
    internal static class UserController
    { 
        internal static async Task<Profile?> CreateGetUserProfile(long tg_id)
        {
            Context dbContext = new Context();
            DB db = new DB();
            Profile? profile = await db.GetProfile(dbContext, tg_id);
            if (profile == null)
            {
                profile = new Profile(tg_id);
                await db.CreateProfile(dbContext, profile);
            }
            else if (profile.tg_id == -2281488)
            {
                return null;
            }
            else
            {
                Console.WriteLine("Profile exists");
            }
            return profile;
        }
        internal static async Task<bool> UpdateFullProfile(Profile profile)
        {
            Context dbContext = new Context();
            DB db = new DB();
            return await db.UpdateFullProfile(dbContext, profile);
        }
        internal static async Task<bool> UpdateProfileColumn(string column, dynamic value, long? tg_id)
        {
            Context dbContext = new Context();
            DB db = new DB();
            return await db.UpdateProfileColumn(dbContext, column, value, tg_id);
            
        }
    }
}
