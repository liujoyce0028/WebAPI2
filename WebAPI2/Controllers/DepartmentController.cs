using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebAPI2.Models;
using System.Configuration;

namespace WebAPI2.Controllers
{
    public class DepartmentController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select  departmentid, departmentname from dbo.departments";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, table);
                }
            }
        }

        public string Post(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Departments values('"+dep.DepartmentName +@"')";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                {
                    using (var cmd = new SqlCommand(query, con))
                    {
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            da.Fill(table);
                        }
                    }
                }
                return "Insert record";
            }
            catch (Exception ex)
            {
                return "Failed to Add";
            }
        }


        public string Put(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update dbo.departments set departmentname='"+dep.DepartmentName+"'" + " where departmentid="+dep.DepartmentID;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                {
                    using (var cmd = new SqlCommand(query, con))
                    {
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            da.Fill(table);
                        }
                    }
                }
                return "update record";
            }
            catch (Exception ex)
            {
                return "Failed to update";
            }
        }
    }
}
