using crud_api.Models;
using crud_api.Models.DbAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        EmployeeDbAccess employeeDb = new EmployeeDbAccess();
        ApiResponse response = new ApiResponse();
        
        [Route("GetEmployees")]
        public IActionResult GetEmployees()
        {
            try
             {
            List<Employee> emps = employeeDb.GetEmployees();
                response.Ok = true;
                response.Message = $"Total {emps.Count}record fatch successfully";
                response.Data = emps;
            }
            catch (Exception ex)
            {
                response.Ok = true;
                response.Message = "show emp";
            }
            return Ok(response);
        }

        [Route("PostEmployee")]
      
        public IActionResult PostEmployee(Employee employee)
        {
            try
            {
                var res = employeeDb.CreateEmployee(employee);
              
                if(res == "ok")
                {
                    response.Ok = true;
                    response.Message = $"Employee Created successfully";
                }
                else if (res== "fail")
                {
                    response.Ok = false;
                    response.Message = $"Employee Email already exist";
                }
                else
                {
                    response.Ok = false;
                    response.Message = res;
                }    
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [Route("DeleteEmployees")]
        [HttpDelete]
        public IActionResult DeleteEmployees(int id)
        {
            try
            {
                var res = employeeDb.DeleteEmployees(id);
                if(res=="ok")
                {
                    response.Ok = true;
                    response.Message = $"Employees deleted successfully";
              
                }
                else if (res=="fail")
                {
                    response.Ok = false;
                    response.Message = $"something went wrong ";
                }
                else
                {
                    response.Ok = false;
                    response.Message = res;
                }

            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                
            }
            return Ok(response);
        }
        [Route("UpdateEmployee")]
        [HttpPut]
        public IActionResult UpdateEmployee(Employee emp)
        {
            try
            {
                var res = employeeDb.UpdateEmployee(emp);
                if(res=="Ok")
                {
                    response.Ok = true;
                    response.Message = $"Employee Updated successfully";
                }
                else
                {
                    response.Ok = false;
                    response.Message = res;
                }

            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Ok(response);
           
        }

    }
}
