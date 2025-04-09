using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class EmployeeDb
{
    private DatabaseInterface Db;
    public EmployeeDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(Employee employee) 
    {
        Db.Sql = $"INSERT INTO {nameof(Employee)} ({nameof(Employee.Name)}, {nameof(Employee.DefaultCapacity)}) VALUES( '{employee.Name}', '{employee.DefaultCapacity}');";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("Employee added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(Employee)} WHERE {nameof(Employee.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("Employee removed successfully.");        
    }

    public async Task<List<Employee>> GetAll() 
    {
        List<Employee>? result = new List<Employee>();

        Db.Sql = $"SELECT * FROM {nameof(Employee)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                Employee employee = new Employee();
                employee.Id = reader.GetInt32(0);
                employee.Name = reader.GetString(1);
                employee.DefaultCapacity = reader.GetInt32(2);
                employee.AvailableCapacity = await GetCapacity(employee);
                employee.Plannings = await GetPlannings(employee.Id);

                result.Add(employee);
            }
            Console.WriteLine("Employee list loaded");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
        Db.Connection.Close();
        return result;
    }

    private async Task<List<Capacity>> GetCapacity(Employee employee) 
    {
        List<CapacityCorrection> corrections = new List<CapacityCorrection>();
        List<Capacity> result = new List<Capacity>();
        
        CapacityCorrectionDb correctionDb = new CapacityCorrectionDb();
        corrections = await correctionDb.GetByEmployeeId(employee.Id);
        
        foreach(CapacityCorrection correction in corrections) {
            result.Add(new Capacity(){
                Month = correction.Month,
                Year = correction.Year,
                Value = employee.DefaultCapacity - correction.Value
            });
        }
        return result;
    }

    private async Task<List<Planning>> GetPlannings(int Id) 
    {
        PlanningDb planningDb = new PlanningDb();
        return await planningDb.GetByEmployeeId(Id);
    }

    public async Task<Employee> GetById(int Id) 
    {
        Employee result = new Employee();

        Db.Sql = $"SELECT * FROM {nameof(Employee)} WHERE {nameof(Employee.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.Name = reader.GetString(1);
            result.DefaultCapacity = reader.GetInt32(2);
            Console.WriteLine("Employee loaded successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
}
