using EmployeeAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeAPI.Data
{
    public class EmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 🔹 Add Employee
        public void AddEmployee(Employee employee)
        {
            using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new("AddEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // 🔹 Get All Employees
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new("GetAllEmployees", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Department = reader["Department"].ToString(),
                    Salary = Convert.ToDecimal(reader["Salary"])
                });
            }

            return employees;
        }

        // 🔹 Get Employee By ID
        public Employee GetEmployee(int id)
        {
            using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new("GetEmployeeById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Department = reader["Department"].ToString(),
                    Salary = Convert.ToDecimal(reader["Salary"])
                };
            }

            return null;
        }

        // 🔹 Update Employee
        public void UpdateEmployee(Employee employee)
        {
            using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new("UpdateEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // 🔹 Delete Employee
        public void DeleteEmployee(int id)
        {
            using SqlConnection conn = new(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new("DeleteEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
