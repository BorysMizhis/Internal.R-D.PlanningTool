using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class RelationDb
{
    private DatabaseInterface Db;
    public RelationDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(Relation relation) 
    {
        Db.Sql = $"INSERT INTO {nameof(Relation)} ({nameof(Relation.ChapterId)}, {nameof(Relation.EmployeeId)}, {nameof(Relation.FeatureId)}, {nameof(Relation.ReleaseVersionId)}) VALUES( {relation.ChapterId}, {relation.EmployeeId}, {relation.FeatureId}, {relation.ReleaseVersionId});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("Relation added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(Relation)} WHERE {nameof(Relation.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("Relation removed successfully.");        
    }

    public async Task<List<Relation>> GetAll() 
    {
        List<Relation>? result = new List<Relation>();

        Db.Sql = $"SELECT * FROM {nameof(Relation)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new Relation() {
                    Id = reader.GetInt32(0),
                    ChapterId = reader.GetInt32(1),
                    EmployeeId = reader.GetInt32(2),
                    FeatureId = reader.GetInt32(3),
                    ReleaseVersionId = reader.GetInt32(4),
                });
            }
            Console.WriteLine("Relation list loaded");
        }
        else
        {
            Console.WriteLine("Relation not found.");
        }
        Db.Connection.Close();
        return result;
    }

    public async Task<List<int>> GetEmployeeIds(string SearchParameter ,int Id) 
    {
        List<int> result = new List<int>();
        Db.Sql = $"SELECT {nameof(Relation.EmployeeId)} FROM {nameof(Relation)} WHERE {SearchParameter} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(reader.GetInt32(0));
            }
            Console.WriteLine("Relation loaded successfully.");
        }
        else
        {
            Console.WriteLine("Relation not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
    
    public async Task<List<int>> GetFeatureIdsByReleaseVersionId(int Id) 
    {
        List<int> result = new List<int>();
        Db.Sql = $"SELECT {nameof(Relation.FeatureId)} FROM {nameof(Relation)} WHERE {nameof(Relation.ReleaseVersionId)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(reader.GetInt32(0));
            }
            Console.WriteLine("Relation loaded successfully.");
        }
        else
        {
            Console.WriteLine("Relation not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
}
