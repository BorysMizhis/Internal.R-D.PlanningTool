using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class CapacityCorrectionDb
{
    private DatabaseInterface Db;
    public CapacityCorrectionDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(CapacityCorrection capacityCorrection) 
    {
        Db.Sql = $"INSERT INTO {nameof(CapacityCorrection)} ({nameof(CapacityCorrection.EmployeeId)}, {nameof(CapacityCorrection.Month)}, {nameof(CapacityCorrection.Year)}, {nameof(CapacityCorrection.Value)}) VALUES( {capacityCorrection.EmployeeId}, {capacityCorrection.Month}, {capacityCorrection.Year}, {capacityCorrection.Value});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("CapacityCorrection added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(CapacityCorrection)} WHERE {nameof(CapacityCorrection.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("CapacityCorrection removed successfully.");        
    }

    public async Task<List<CapacityCorrection>> GetAll() 
    {
        List<CapacityCorrection>? result = new List<CapacityCorrection>();

        Db.Sql = $"SELECT * FROM {nameof(CapacityCorrection)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new CapacityCorrection() {
                    Id = reader.GetInt32(0),
                    EmployeeId = reader.GetInt32(1),
                    Month = reader.GetInt32(2),
                    Year = reader.GetInt32(3),
                    Value = reader.GetInt32(4),
                });
            }
            Console.WriteLine("CapacityCorrection list loaded");
        }
        else
        {
            Console.WriteLine("CapacityCorrection not found.");
        }
        Db.Connection.Close();
        return result;
    }

    public async Task<CapacityCorrection> GetById(int Id) 
    {
        CapacityCorrection result = new CapacityCorrection();

        Db.Sql = $"SELECT * FROM {nameof(CapacityCorrection)} WHERE {nameof(CapacityCorrection.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.EmployeeId = reader.GetInt32(1);
            result.Year = reader.GetInt32(2);
            result.Month = reader.GetInt32(3);
            result.Value = reader.GetInt32(4);
            Console.WriteLine("CapacityCorrection loaded successfully.");
        }
        else
        {
            Console.WriteLine("CapacityCorrection not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }

    public async Task<List<CapacityCorrection>> GetByEmployeeId(int Id) 
    {
        List<CapacityCorrection> result = new List<CapacityCorrection>();

        Db.Sql = $"SELECT * FROM {nameof(CapacityCorrection)} WHERE {nameof(CapacityCorrection.EmployeeId)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new CapacityCorrection() {
                    Id = reader.GetInt32(0),
                    EmployeeId = reader.GetInt32(1),
                    Year = reader.GetInt32(2),
                    Month = reader.GetInt32(3),
                    Value = reader.GetInt32(4)
                });
            }
            Console.WriteLine("CapacityCorrection list loaded");
        }
        else
        {
            Console.WriteLine("CapacityCorrection not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
}
