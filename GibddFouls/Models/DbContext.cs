using GibddFouls.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GibddFouls.Models
{
    public class DbContext
    {
        private static readonly DbContext dbContext = new DbContext();
        private static string _connection;

        private DbContext()
        {
            
        }

        public static string Connection { 
            get { return _connection; } set { _connection = value; } }
        public static DbContext GetInstance()
        {
            return dbContext;
        }

        public void InitDb()
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                    string filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\create.sql";
                    string command = Util.LoadSQL(filename);
                    MySqlCommand comm = new MySqlCommand(command, conn);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    
                }
                
            }
        }

        

        public void FillData()
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                    string filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\newdata.sql";
                    string command = Util.LoadSQL(filename);
                    MySqlCommand comm = new MySqlCommand(command, conn);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);

                }

            }
        }
//=========================================================================================================================================
               

        internal List<Registration> GetRegistrations(string text)
        {
            List<Registration> result = new List<Registration>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                string sql = $"SELECT * FROM registration WHERE numid LIKE '%{text}%'";
                using (MySqlCommand command = new MySqlCommand(sql,conn))
                {
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Registration reg = new Registration();
                                reg.Number = reader.GetString(3);
                                reg.OwnerId = reader.GetInt32(1);
                                reg.CarId = reader.GetInt32(2);
                                result.Add(reg);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return result;
        }

        internal Car GetCarById(int carId)
        {
            Car result = new Car();
            using(MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                string sql = $"SELECT * FROM carmodels WHERE id={carId}";
                using(MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.IdCar = reader.GetInt32(0);
                            result.Carname = reader.GetString(1);
                            result.Caryear = reader.GetString(2);
                        }
                    }
                }
            }
            return result;
        }

        internal object CountData(string tablename)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT Count(*) as cnt FROM {tablename}";
                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            return result;
        }

        internal List<VRegistration> GetVRegistrations(string text)
        {
            List<VRegistration> result = new List<VRegistration>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                string sql = $"SELECT * FROM vregistration WHERE number LIKE '%{text}%'";
                using(MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VRegistration reg = new VRegistration();
                            reg.Id = reader.GetInt32(0);
                            reg.Number = reader.GetString(1);
                            reg.CarId = reader.GetInt32(2);
                            reg.Carmodel = reader.GetString(3);
                            reg.Caryear = reader.GetString(4);
                            reg.OwnerId = reader.GetInt32(5);
                            reg.Owner = reader.GetString(6);
                            result.Add(reg);
                        }
                    }
                }


            }
            return result;
        }

        internal Owner GetOwnerById(int ownerId)
        {
            Owner result = new Owner();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                string sql = $"SELECT * FROM owners WHERE id={ownerId}";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.OwnerId = reader.GetInt32(0);
                            result.OwnerName = reader.GetString(1);
                        }
                    }
                }
            }
            return result;
        }


        internal int NewCar(string txtCarmodel, string txtCaryear)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"INSERT INTO carmodels (`carname`,`year`) VALUES('{txtCarmodel}','{txtCaryear}')";
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT LAST_INSERT_ID()";
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            result = reader.GetInt32(0);
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            return result;
        }

        internal int DeleteData(int id,string table)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"DELETE FROM {table} WHERE id = {id}";
                    try
                    {
                        command.ExecuteNonQuery();
                        result = 1;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return result;
        }
        internal void UpdateFoulType(FoulType foulType)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"UPDATE typefouls SET type='{foulType.Type}' WHERE id = {foulType.Id}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        internal void UpdateRegistration(Registration reg)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"UPDATE registration SET idowner={reg.OwnerId},idcarmodel={reg.CarId} WHERE numid = '{reg.Number}'";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        internal List<VFoul> GetVFouls(string text)
        {
            List<VFoul> result = new List<VFoul>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                string sql = $"SELECT * FROM vfouls WHERE numid LIKE '%{text}%'";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VFoul foul = new VFoul();
                            foul.Id = reader.GetInt32(0);
                            foul.Date = reader.GetDateTime(1);
                            foul.IdReg = reader.GetInt32(2);
                            foul.Number = reader.GetString(3);
                            foul.Owner = reader.GetString(4);
                            foul.IdTypeFoul = reader.GetInt32(5);
                            foul.TypeFoul = reader.GetString(6);
                            result.Add(foul);
                        }
                    }
                }
            }
            return result;
        }

        internal void UpdateFoul(Foul foul)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"UPDATE fouls SET datef='{foul.DtFoul.ToString("yyyy-MM-dd")}',idregistration={foul.IdRegistr},forfeit = {foul.IdTypeFoul} " +
                        $"WHERE id = {foul.Id}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        internal int NewFoul(Foul foul)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    string dt = foul.DtFoul.ToString("yyyy-MM-dd");
                    command.CommandText = $"INSERT INTO fouls (idregistration,`datef`,fouls.`forfeit`) VALUES({foul.IdRegistr},'{dt}',{foul.IdTypeFoul})";
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT LAST_INSERT_ID()";
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            result = reader.GetInt32(0);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return result;
        }

        internal void UpdateCar(Car car)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"UPDATE carmodels SET carname='{car.Carname}',year='{car.Caryear}' WHERE id = {car.IdCar}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        internal void UpdateOwner(int ownerId, string ownerName)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    
                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"UPDATE owners SET name='{ownerName}' WHERE id = {ownerId}";
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {

                        }
                }
            }
        }

        internal List<Car> GetCars(string text)
        {
            List<Car> result = new List<Car>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                    return result;
                }
                string sql = $"Select * FROM carmodels WHERE carname LIKE '%{text}%'";
                using (MySqlCommand command = new MySqlCommand(sql,conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Car car = new Car();
                                car.IdCar = reader.GetInt32(0);
                                car.Carname = reader.GetString(1);
                                car.Caryear = reader.GetString(2);
                                result.Add(car);
                            }

                        }
                    }
                }
            }
            return result;
        }

        internal int NewRegistration(Registration registration)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                using(MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"INSERT INTO registration (idowner,idcarmodel,numid) VALUES({registration.OwnerId},{registration.CarId},'{registration.Number}')";
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT LAST_INSERT_ID()";
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            result = reader.GetInt32(0);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return result;
        }

        internal int NewOwner(string text)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                using(MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"SELECT * FROM owners WHERE name = '{text}'";
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                    if (result == 0)
                    {
                        command.CommandText = $"INSERT INTO owners (name) VALUES('{text}')";
                        try
                        {
                            command.ExecuteNonQuery();
                            command.CommandText = "SELECT LAST_INSERT_ID()";
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                reader.Read();
                                result = reader.GetInt32(0);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return result;
        }

        public List<Owner> GetOwnersLike(string text,bool like)
        {
            List<Owner> result = new List<Owner>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }

                string sql = like? $"select * FROM owners WHERE name LIKE '{text}%'":$"select * FROM owners WHERE name ='{text}'";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Owner owner = new Owner();
                            owner.OwnerId = reader.GetInt32(0);
                            owner.OwnerName = reader.GetString(1);
                            result.Add(owner);
                        }
                    }
                }
            }
            return result;
        }

        public List<FoulType> GetFoulsType(string text)
        {
            List<FoulType> result = new List<FoulType>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }

                string sql = text.Equals("") ? "select * FROM typefouls" : $"select * FROM typefouls WHERE typename LIKE '%{text}%'";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FoulType foulType = new FoulType();
                            foulType.Id = reader.GetInt32(0);
                            foulType.Type = reader.GetString(1);
                            result.Add(foulType);
                        }
                    }
                }
            }
            return result;
        }
        public int NewFoulType(string text)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return result;
                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"INSERT INTO typefouls (typename) VALUES('{text}')";
                    try
                    {
                        command.ExecuteNonQuery();
                        result = 1;
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            return result;
        }

        internal bool HasNumber(string text)
        {
            bool result = false;
            using(MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    result = true;
                }
                string sql = $"select * from registration where numid = '{text}'";
                using(MySqlCommand command = new MySqlCommand(sql,conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        internal FoulsRep GetFoulsRep()
        {
            FoulsRep result = new FoulsRep();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    
                }
                string sql = $"SELECT datef, COUNT(*) FROM fouls GROUP BY datef ORDER BY datef";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var dt = reader.GetDateTime(0);
                                var val = reader.GetInt32(1);
                                result.AddData(dt, val);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
