using Microsoft.Data.Sqlite;
using PlanningTool.DBInterface;

namespace PlanningTool.Models;

public class Employee
{
    public int Id { set; get; }
    public required string Name { set; get; }
    public required int DefaultCapacity { set; get; }
}

public class EmployeeDb 
{
    public async Task Add(Employee employee) 
    {
        await using var db = new DatabaseInterface();

        string sql = $"INSERT INTO {nameof(Employee)} ({nameof(Employee.Name)}, {nameof(Employee.DefaultCapacity)}) VALUES( '{employee.Name}', '{employee.DefaultCapacity}');";
        
        using var connection = new SqliteConnection($"Data Source={db.DbPath}");
        connection.Open();
        
        using var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        connection.Close();
        
        Console.WriteLine("Employee added successfully.");
    }

    public async Task Remove(int Id) 
    {
        await using var db = new DatabaseInterface();

        string sql = $"DELETE FROM {nameof(Employee)} WHERE {nameof(Employee.Id)} = {Id});";
        
        using var connection = new SqliteConnection($"Data Source={db.DbPath}");
        connection.Open();
        
        using var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        connection.Close();        

        Console.WriteLine("Employee added successfully.");
    }

    public async Task<List<Employee>> GetAll() 
    {
        List<Employee>? result = new List<Employee>();
        await using var db = new DatabaseInterface();

        string sql = $"SELECT * FROM {nameof(Employee)};";
        
        using var connection = new SqliteConnection($"Data Source={db.DbPath}");
        connection.Open();
        
        using var command = new SqliteCommand(sql, connection);
        
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new Employee() {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    DefaultCapacity = reader.GetInt32(2)
                });
            }
            
            Console.WriteLine("Employee list loaded");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
        
        connection.Close();        
        return result;
    }

    public async Task<Employee> GetById(int Id) 
    {
        Employee? result = null;
        await using var db = new DatabaseInterface();

        string sql = $"SELECT * FROM {nameof(Employee)} WHERE {nameof(Employee.Id)} = {Id};";
        
        using var connection = new SqliteConnection($"Data Source={db.DbPath}");
        connection.Open();
        
        using var command = new SqliteCommand(sql, connection);
        
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result = new Employee() {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                DefaultCapacity = reader.GetInt32(2)
            };
            Console.WriteLine("Employee found successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
        
        connection.Close();        
        return result;
    }
}

