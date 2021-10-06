using CommonAttacks.Data;
using CommonAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAttacks.Controllers
{
    public class SqlInjection : Controller
    {
        public SqlInjection(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IActionResult VulnerableGetCarById()
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Car WHERE Id = {Request.Query["Id"]}",connection);

                using (var reader = command.ExecuteReader())
                {
                    Car foundCar = null;
                    if (reader.Read())
                    {
                        foundCar = new Car
                        {
                            Id = (int)reader["Id"],
                            Make = reader["Make"].ToString(),
                            Model = reader["Model"].ToString(),
                            Color = reader["Color"].ToString(),
                            Year = (int)reader["Year"]

                        };

                    }
                    else
                    {
                        return NotFound();
                    }

                    return View("GetCarById", foundCar);
                }
            }
        }
    }
}
