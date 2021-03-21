﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _5951071099_TranDinhThao.Models;
using System.Data;

namespace _5951071099_TranDinhThao.Controllers
{
    public class HomeController : Controller
    {
        db dbop = new db();
        string msg;
        
        public IActionResult Index()
        {
            Employee emp = new Employee();
            emp.flag = "get";
            DataSet ds = dbop.Empget(emp, out msg);
            List<Employee> list = new List<Employee>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Employee
                {
                    Sr_no = Convert.ToInt32(dr["Sr_no"]),
                    Emp_name = dr["Emp_name"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["STATE"].ToString(),
                    Country = dr["Country"].ToString(),
                    Deparment = dr["Department"].ToString()
                }) ;
            }
            return View(list);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create([Bind] Employee emp)
        {
            try
            {
                emp.flag = "insert";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Employee emp = new Employee();
            emp.Sr_no = id;
            emp.flag = "getid";
            DataSet ds = dbop.Empget(emp, out msg);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                emp.Sr_no = Convert.ToInt32(dr["Sr_no"]);
                emp.Emp_name = dr["Emp_name"].ToString();
                emp.City = dr["City"].ToString();
                emp.State = dr["State"].ToString();
                emp.Country = dr["Country"].ToString();
                emp.Deparment = dr["Department"].ToString();
            }
            return View(emp);
        }

        [HttpPost]

        public IActionResult Edit(int id, [Bind] Employee emp)
        {
            try
            {
                emp.Sr_no = id;
                emp.flag = "update";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id, [Bind] Employee emp)
        {
            try
            {
                emp.Sr_no = id;
                emp.flag = "delete";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
