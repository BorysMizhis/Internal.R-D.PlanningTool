using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class PlanningDb
{
    private DatabaseInterface Db;
    public PlanningDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(Planning planning) 
    {
        Db.Sql = $"INSERT INTO {nameof(Planning)} ({nameof(Planning.FeatureId)}, {nameof(Planning.EmployeeId)}, {nameof(Planning.Month)}, {nameof(Planning.Year)}, {nameof(Planning.Value)}) VALUES({planning.FeatureId}, {planning.EmployeeId}, {planning.Month}, {planning.Year}, {planning.Value});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("Planning added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(Planning)} WHERE {nameof(Planning.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("Planning removed successfully.");        
    }

    public async Task<List<Planning>> GetAll() 
    {
        List<Planning>? result = new List<Planning>();

        Db.Sql = $"SELECT * FROM {nameof(Planning)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new Planning() {
                    Id = reader.GetInt32(0),
                    FeatureId = reader.GetInt32(1),
                    EmployeeId = reader.GetInt32(2),
                    Month = reader.GetInt32(3),
                    Year = reader.GetInt32(4),
                    Value = reader.GetInt32(5),
                });
            }
            Console.WriteLine("Planning list loaded");
        }
        else
        {
            Console.WriteLine("Planning not found.");
        }
        Db.Connection.Close();
        return result;
    }

    public async Task<Planning> GetById(int Id) 
    {
        Planning result = new Planning();

        Db.Sql = $"SELECT * FROM {nameof(Planning)} WHERE {nameof(Planning.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.FeatureId = reader.GetInt32(1);
            result.EmployeeId = reader.GetInt32(2);
            result.Month = reader.GetInt32(3);
            result.Year = reader.GetInt32(4);            
            result.Value = reader.GetInt32(5);
            Console.WriteLine("Planning loaded successfully.");
        }
        else
        {
            Console.WriteLine("Planning not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }

    public async Task<List<Planning>> GetByFeatureId(int Id) 
    {
        List<Planning> result = new List<Planning>();

        Db.Sql = $"SELECT * FROM {nameof(Planning)} WHERE {nameof(Planning.FeatureId)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new Planning() {
                    Id = reader.GetInt32(0),
                    FeatureId = reader.GetInt32(1),
                    EmployeeId = reader.GetInt32(2),
                    Month = reader.GetInt32(3),
                    Year = reader.GetInt32(4),                    
                    Value = reader.GetInt32(5)
                });
            }
            Console.WriteLine("Planning list loaded");
        }
        else
        {
            Console.WriteLine("Planning not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }

    public async Task<List<Planning>> GetByEmployeeId(int Id) 
    {
        List<Planning> result = new List<Planning>();

        Db.Sql = $"SELECT * FROM {nameof(Planning)} WHERE {nameof(Planning.EmployeeId)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new Planning() {
                    Id = reader.GetInt32(0),
                    FeatureId = reader.GetInt32(1),
                    EmployeeId = reader.GetInt32(2),
                    Month = reader.GetInt32(3),
                    Year = reader.GetInt32(4),                    
                    Value = reader.GetInt32(5)
                });
            }
            Console.WriteLine("Planning list loaded");
        }
        else
        {
            Console.WriteLine("Planning not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
}
