using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPI2.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAPI2.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select  employeeID, employeeName,department,mailID,DOJ from dbo.employees";
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

        public string Post(Employee dep)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Employees(EmployeeName,Department,MailID,DOJ) values('" + dep.EmployeeName 
                    +"','"+dep.Department+"','"+dep.MailID+"','"+dep.DOJ + @"')";
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


        public string Put(Employee dep)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update dbo.employees set employeename='" + dep.EmployeeName +"',"+
                    "department='"+dep.Department+"',"+
                   "mailid="+"'"+dep.MailID + "'," +
                   "DOJ='"+dep.DOJ+"' "+ " where employeeid=" + dep.EmployeeID;
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
