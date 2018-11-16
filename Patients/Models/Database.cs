using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Patients.Models
{
    public sealed class Database
    {
        public static Database Instance { get {return Impl.Instance; } }

        public string DBConnection;

        private class Impl
        {
            internal static readonly Database Instance = new Database();
            static Impl() 
            {
                Instance.DBConnection = "data source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\" + "Patients.sqlite");
            }
        }

        public DataTable GetPatients(string text)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBConnection))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string query = "select id, firstName, lastName from patients where firstName like " +
                                     "'" + text + "%'" + " or " + "lastName like " + "'" + text + "%'";
                    return sh.Select(query);
                }
            }
        }

        public DataTable GetPatient(string patientId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBConnection))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    string query = "select id, firstName, lastName, dateOfbirth, phoneNumber from patients where id = " + patientId;
                    return sh.Select(query);
                }
            }
        }
        public DataTable GetTableList()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBConnection))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    return sh.GetTableList();
                }
            }
        }
    }
}