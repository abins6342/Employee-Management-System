using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Models.Graph;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using EmployeeManagement.DataAccess.Repository;
using GraphQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : GraphqlClientBase, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly HasuraConfiguration _hasuraConfiguration;

        public EmployeeService(IEmployeeRepository employeeRepository, IOptions<HasuraConfiguration> options)
        {
            _employeeRepository = employeeRepository;
            _hasuraConfiguration = options.Value;
        }

        public EmployeeDto GetEmployeeById(int employeeId)
        {
            try
            {
                var isSaveToDbTrue = _hasuraConfiguration.IsSaveToDb;
                if (isSaveToDbTrue == true)
                {
                    ValidateEmployee(employeeId);
                    var employee = _employeeRepository.GetEmployeeById(employeeId);
                    if (employee == null)
                    {
                        throw new Exception("No such Employee");
                    }

                    return new EmployeeDto
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Age = employee.Age,
                        Address = employee.Address,
                        Department = employee.Department
                    };
            }
                else
                {
                var getEmployeeByIdHasura = GetEmployeeByIdHasura(employeeId).Result;
                 return getEmployeeByIdHasura;
                }
        }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            try
            {
                var isSaveToDbTrue = _hasuraConfiguration.IsSaveToDb;
               
                if (isSaveToDbTrue == true)
                {
                    var employeeDataList = _employeeRepository.GetEmployees();

                    if (employeeDataList == null)
                    {
                        throw new Exception("No Employees");
                    }                 
                    return employeeDataList.Select(employee => new EmployeeDto
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Age = employee.Age,
                        Address = employee.Address,
                        Department = employee.Department
                    });
                }
                else
                {
                     var listOfEmployeeDto = GetEmployeesHasura().Result.ToList();
                    return listOfEmployeeDto;
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool InsertEmployee(EmployeeDto employeedto)
        {
            try
            {
                var empdata = new EmployeeData()
                {

                    Name = employeedto.Name,
                    Age = employeedto.Age,
                    Address = employeedto.Address,
                    Department = employeedto.Department
                };

                var employeeDtoHasura = new EmployeeDtoHasura()
                {
                    Name = employeedto.Name,
                    Age = employeedto.Age,
                    Address = employeedto.Address,
                    Department = employeedto.Department
                };

                var isSaveToDbTrue = _hasuraConfiguration.IsSaveToDb;

                if (isSaveToDbTrue == true)
                {
                    var insertEmployee = _employeeRepository.InsertEmployee(empdata);
                    if (!insertEmployee)
                    {
                        throw new Exception("No Employee");
                    }
                }
                else
                {
                    var insertEmployeeSaveToHasura = InsertEmployeeSaveToHasura(employeeDtoHasura);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto.Id < 0)
                {
                    throw new ArgumentException("Invalid Id");
                }
                var empdata = new EmployeeData()
                {
                    Id = employeeDto.Id,
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Department = employeeDto.Department
                };

                var isSaveToDbTrue = _hasuraConfiguration.IsSaveToDb;
                if (isSaveToDbTrue == true)
                {
                    var updateEmployee = _employeeRepository.UpdateEmployee(empdata);
                    if (!updateEmployee)
                    {
                        throw new Exception("No Employee found");
                    }
                }
                else
                {
                    var updateEmployeeSaveToHasura = UpdateEmployeeSaveToHasura(employeeDto);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var isSaveToDbTrue = _hasuraConfiguration.IsSaveToDb;

                if (isSaveToDbTrue == true)
                {
                    ValidateEmployee(employeeId);
                    var updateEmployee = _employeeRepository.DeleteEmployee(employeeId);
                    if (!updateEmployee)
                    {
                        throw new Exception("No Employee found");
                    }                 
                }
                else
                {
                    var deleteEmployeeByIdHasura = DeleteEmployeeByIdHasura(employeeId);
                }
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesHasura()
        {
            var getAllEmployeeQuery = @"query MyQuery {
                                          employees {
                                            id
                                            name
                                            department
                                            age
                                            address
                                          }
                                        }";

            var graphQlRequest = new GraphQLRequest()
            {
                Query = getAllEmployeeQuery
            };

            var response = await _graphQLHttpClient.SendQueryAsync<EmployeesQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employees;
        }

        public async Task<EmployeeDto> GetEmployeeByIdHasura(int id)
        {
            var getByIdQuery = @"query MyQuery($id: Int!) {
                                      employees_by_pk(id: $id) {
                                        id
                                        name
                                        department
                                        age
                                        address
                                      }
                                    }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = getByIdQuery,
                Variables = new
                {
                    id
                }
            };

            var response = await _graphQLHttpClient.SendQueryAsync<EmployeeByIdQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employee;
        }

        public async Task<int> InsertEmployeeSaveToHasura(EmployeeDtoHasura employeeDtoHasura)
        {
            var insertEmployeeMutation = @"mutation MyMutation($employee: employees_insert_input!) {
                                                   insert_employees_one(object: $employee) {
                                                         id
                                                     }
                                              }";

            var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new
                {
                    employee = employeeDtoHasura
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeInsertMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);

            return response.Data.Employee?.Id ?? 0;
        }

        public async Task<int> UpdateEmployeeSaveToHasura(EmployeeDto employeeDto)
        {
            var employeeUpdateMutation = @"mutation MyMutation($id:Int!, $employee: employees_set_input) {
                                              update_employees_by_pk(pk_columns: {id: $id}, _set: $employee) {
                                                id
                                              }
                                            }";          

            var variables = new
            {
                id = employeeDto.Id,
                employee = new
                {
                    id = employeeDto.Id,
                    name = employeeDto.Name,
                    age = employeeDto.Age,
                    address = employeeDto.Address,
                    department = employeeDto.Department                    
                }
            };

            var graphqlRequest = new GraphQLRequest
            {
                Query = employeeUpdateMutation,
                Variables = variables
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeUpdateMutationResponse>(graphqlRequest);
            ValidateGraphResponse(response);

            return response.Data.Employee?.Id ?? 0;
        }

        public async Task<bool> DeleteEmployeeByIdHasura(int id)
        {
            var deleteEmployeeMutation = @"mutation MyMutation($id: Int) {
                                              delete_employees(where: {id: {_eq: $id}}) {
                                                affected_rows
                                              }
                                            }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = deleteEmployeeMutation,
                Variables = new
                {
                    id
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeDeleteMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return true;
        }

        private static void ValidateGraphResponse<T>(GraphQLResponse<T> response)
        {
            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(s => s.Message).ToList()));
            }
        }

        private void ValidateEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                throw new ArgumentException("invalid Entry");
            }
        }

    }
}
