using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace _5951071099_TranDinhThao.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LA497N55;Initial Catalog=DemoDRUD1;Integrated Security=True");
        
        // select
        public DataSet Empget(Employee emp, out string msg)
        {
            DataSet ds = new DataSet();

            msg = "";

            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                cmd.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                cmd.Parameters.AddWithValue("@City", emp.City);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@Department", emp.Deparment);
                cmd.Parameters.AddWithValue("@flag", emp.flag);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "OK";
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return ds;
            }
        }

        // Insert và cập nhật
        public string Empdml(Employee emp, out string msg)
        {
            msg = "";

            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                cmd.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                cmd.Parameters.AddWithValue("@City", emp.City);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@Department", emp.Deparment);
                cmd.Parameters.AddWithValue("@flag", emp.flag);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "OK";
                return msg;
            }
            catch (Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = ex.Message;
                return msg;
            }
        }
    }
}
