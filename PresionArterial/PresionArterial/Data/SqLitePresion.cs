using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PresionArterial.Models;
using System.Threading.Tasks;

namespace PresionArterial.Data
{
    public class SqLitePresion
    {

        SQLiteAsyncConnection db;

        public SqLitePresion(string Path)
        {
            db = new SQLiteAsyncConnection(Path);
            db.CreateTableAsync<Usuario>().Wait();
            db.CreateTableAsync<Models.PresionArterial>().Wait();
        }

        public Task<List<Usuario>> ListUsersAsync()
        {
            return db.Table<Usuario>().ToListAsync();
        }

        public Task<List<Models.PresionArterial>> listPresionAsync(int userId)
        {
            return db.QueryAsync<Models.PresionArterial>("select * from PresionArterial where User_id = ? ", userId);
        }

        public Task<Usuario> getUserByIdAsync(int id)
        {
            return db.Table<Usuario>().Where(item => item.id == id).FirstOrDefaultAsync();
        }

        public Task<Models.PresionArterial> getPresionByIdAsync(int idPresion)
        {
            return db.Table<Models.PresionArterial>().Where(item => item.bloodPressure_id == idPresion).FirstOrDefaultAsync();
        }

        public Task<int> saveUpdateUserAsync(Usuario user)
        {
            if (user.id != 0)
            {
                return db.UpdateAsync(user);
            }
            else
            {
                return db.InsertAsync(user);
            }
        }

        public Task<int> saveUpdatePresionAsync(Models.PresionArterial presion)
        {

            if (presion.bloodPressure_id != 0)
            {
            return db.UpdateAsync(presion);
            }
            else
            {
                return db.InsertAsync(presion);
            }
            
        }

        public Task<int> deleteUserAsync(Usuario user)
        {
            return db.DeleteAsync(user);
        }

        public Task<int> deletePresionAsync(Models.PresionArterial presion)
        {
            return db.DeleteAsync(presion);
        }

    }
}
