using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserwebApiController : Controller
    {
        private IConfiguration configuration;
        public UserwebApiController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        DataTable dtd;
       



        [Route("GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userList = new List<UsersD>();
            var connectionString = this.configuration.GetConnectionString("Constr");
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("select * from UsersD", conn);
                try
                {
                    await conn.OpenAsync();
                    using (var ad = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        ad.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            var User = new UsersD();
                            User.UserId = Convert.ToInt32(row["userId"]);
                            User.UserName = row["userName"].ToString();
                            User.email = row["email"].ToString();
                            User.password = row["password"].ToString();
                            User.phoneno = row["phoneno"].ToString();
                            return Json(User);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json(userList);
        }
        [Route("Getemailandpassword")]
        [HttpGet]
        public JsonResult FinallyPassage(string email, string password)
        {
            var connectionString = new SqlConnection(this.configuration.GetConnectionString("ConStr"));
            var dt = new DataTable();
            try
            {
                connectionString.Open();
                var sqlData = new SqlDataAdapter($"SELECT * FROM UsersD WHERE Email='{email}' AND Password='{password}'", connectionString);
                sqlData.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return Json(dt);
                  
                }
                else
                {
                    return Json(new { Message = "Not Found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message });
            }
            finally
            {
                connectionString.Close();
            }
        }
    }

}