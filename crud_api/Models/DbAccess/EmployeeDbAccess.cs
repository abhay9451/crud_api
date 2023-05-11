using crud_api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_api.Models.DbAccess
{
    public class EmployeeDbAccess
    {
        public ConnectDb db = new ConnectDb();
        public List<Employee> GetEmployees()
        {
            SqlCommand cmd = new SqlCommand("sp_get_employee", db.connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (db.connection.State == System.Data.ConnectionState.Closed)
                db.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<Employee> Lstemployees = new List<Employee>();
            while(dr.Read())
            {
                Employee emp = new Employee();
                emp.Id = (int)dr["id"];
                emp.Name = dr["name"].ToString();
                emp.Email = dr["email"].ToString();
                emp.Gender = dr["gender"].ToString();
                emp.Mobile= dr["mobile"].ToString();
                emp.Salary = Convert.ToDecimal(dr["salary"]);
                emp.Address = dr["address"].ToString();
                emp.emp_Code = dr["emp_code"].ToString();
                Lstemployees.Add(emp);

            }
            db.connection.Close();
            return Lstemployees;
        }
        public string CreateEmployee(Employee employee)
        {
            string message = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insert_employee", db.connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                    db.connection.Open();
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@email", employee.Email);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@mobile", employee.Mobile);
                cmd.Parameters.AddWithValue("@salary", employee.Salary);
                cmd.Parameters.AddWithValue("@address", employee.Address);
               

                int i = (int)cmd.ExecuteScalar();
                if (i == 1)
                {
                    message = "ok";
                }
                else
                {
                    message = "fail";
                }
                db.connection.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
        public string DeleteEmployees (int id)
        {
           string message = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_delete_employee", db.connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (db.connection.State == System.Data.ConnectionState.Closed)
                    db.connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                int r = (int)cmd.ExecuteNonQuery();
                if(r==1)
                {
                    message="ok";
                }
                else
                {
                    message = "fail";
                }
                db.connection.Close();
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
           public string UpdateEmployee(Employee emp)
           {
            string message = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update_employee", db.connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (db.connection.State == System.Data.ConnectionState.Closed)
                    db.connection.Open();
                cmd.Parameters.AddWithValue("@id", emp.Id);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@email", emp.Email);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@mobile", emp.Mobile);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@address", emp.Address);
            //    cmd.Parameters.AddWithValue("@emp_code", emp.emp_Code);
                int r = (int)cmd.ExecuteScalar();
                if(r==1)
                {
                    message = "Ok";

                }
                else
                {
                    message = "fail";
                }
                db.connection.Close();
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
           }
    }
}
