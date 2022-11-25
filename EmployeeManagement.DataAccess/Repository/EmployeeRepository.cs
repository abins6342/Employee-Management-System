using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    /// <summary>
    /// Connect To Database and Perforum CRUD operations
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string _sqlConnectionString = "data source = (localdb)\\mssqllocaldb; database = training";
        private const string _spGetAllEmployee = "EXEC spGetAllEmployees";
        private const string _spGetEmployeeById = "EXEC spGetEmployeeById @EmployeeId";
        private const string _spInsertEmployee = "EXEC spInsertEmployee @EmployeeName,@EmployeeDepartment,@EmployeeAge,@EmployeeAddress";
        private const string _spUpdateEmployee = "EXEC spUpdateEmployee @EmployeeId,@EmployeeName, @EmployeeDepartment, @EmployeeAge, @EmployeeAddress";
        private const string _spDeleteEmployee = "EXEC spDeleteEmployee @EmployeeId";

        private SqlConnection _sqlConnection;

        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString);
        }

        public EmployeeData GetEmployeeById(int id)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: _spGetEmployeeById, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("EmployeeId", id);

                var sqlDataReader = sqlCommand.ExecuteReader();

                var employee = new EmployeeData();

                while (sqlDataReader.Read())
                {
                    employee.Id = (int)sqlDataReader["Id"];
                    employee.Name = (string)sqlDataReader["Name"];
                    employee.Department = (string)sqlDataReader["Department"];
                    employee.Age = (int)sqlDataReader["Age"];
                    employee.Address = (string)sqlDataReader["Address"];
                }

                return employee;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
            
        }

        public IEnumerable<EmployeeData> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: _spGetAllEmployee, _sqlConnection);

                var sqlDataReader = sqlCommand.ExecuteReader();

                var listOfEmployee = new List<EmployeeData>();

                while (sqlDataReader.Read())
                {
                    listOfEmployee.Add(new EmployeeData()
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Department=(string)sqlDataReader["Department"]  
                    });
                }
                return listOfEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        //Create Methods For Table insert, update and Delete Here
        public bool InsertEmployee (EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: _spInsertEmployee, _sqlConnection);

                sqlCommand.Parameters.AddWithValue("EmployeeName", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("EmployeeDepartment", employeeData.Department);
                sqlCommand.Parameters.AddWithValue("EmployeeAge", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("EmployeeAddress", employeeData.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
                 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool UpdateEmployee(EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: _spUpdateEmployee, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("EmployeeId", employeeData.Id);
                sqlCommand.Parameters.AddWithValue("EmployeeName", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("EmployeeDepartment", employeeData.Department);
                sqlCommand.Parameters.AddWithValue("EmployeeAge", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("EmployeeAddress", employeeData.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: _spDeleteEmployee, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("EmployeeId", id);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }   
    }
}
