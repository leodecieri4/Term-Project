using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Chakram1.Models;

namespace Chakram1.Controllers
{
    public class ChakramController : Controller
    {
        private readonly IConfiguration configuration;

        public ChakramController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            string connectionString = configuration.GetConnectionString("Default");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Id, Name, foto_1, details, foto_2 ,foto_3,Surname, trophies,goal_assist   FROM Chakram", conn);
            List<Chakram> chakrams = new List<Chakram>();

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chakrams.Add (new Chakram()
                {
                    Id = dr.GetInt32(dr.GetOrdinal("Id")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    Surname = dr.GetString(dr.GetOrdinal("Surname")),
                    foto_1 = dr.GetString(dr.GetOrdinal("Foto_1")),
                    details = dr.GetString(dr.GetOrdinal("details")),
                    foto_2 = dr.GetString(dr.GetOrdinal("foto_2")),
                    foto_3 = dr.GetString(dr.GetOrdinal("foto_3")),
                    trophies = dr.GetInt32(dr.GetOrdinal("trophies")),
                    goal_assist = dr.GetInt32(dr.GetOrdinal("goal_assist"))

                });
            }

            conn.Close();

            return View(chakrams);
        }
        public IActionResult Details(int id)
        {
            string connectionString = configuration.GetConnectionString("Default");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT Id,Name,Surname,foto_1,foto_2,foto_3,goal_assist,trophies,details FROM Chakram WHERE ID = {id}";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                   

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Chakram footballer = new Chakram()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                foto_1 = reader.GetString(reader.GetOrdinal("foto_1")),
                                foto_2 = reader.GetString(reader.GetOrdinal("foto_2")),
                                foto_3 = reader.GetString(reader.GetOrdinal("foto_3")),
                                trophies = reader.GetInt32(reader.GetOrdinal("trophies")),
                                goal_assist = reader.GetInt32(reader.GetOrdinal("goal_assist")),
                                details = reader.GetString(reader.GetOrdinal("details"))
                            };

                            return View(footballer);
                        }
                    }

                    return View("CharacterNotFound");
                }
            }
        }
        public IActionResult Masum()
        {









            return View();
        }














    }
}
