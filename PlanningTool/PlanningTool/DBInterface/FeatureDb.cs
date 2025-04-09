using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class FeatureDb
{
    private DatabaseInterface Db;
    public FeatureDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(Feature feature) 
    {
        Db.Sql = $"INSERT INTO {nameof(feature)} ({nameof(Feature.VersionId)}, {nameof(Feature.Name)}, {nameof(Feature.Estimation)}, {nameof(Feature.State)}) VALUES( {feature.VersionId}, '{feature.Name}', {feature.Estimation}, {feature.State});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("Feature added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(Feature)} WHERE {nameof(Feature.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("Feature removed successfully.");        
    }

    public async Task<List<Feature>> GetAll() 
    {
        List<Feature>? result = new List<Feature>();

        Db.Sql = $"SELECT * FROM {nameof(Feature)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                Feature feature = new Feature();
                feature.Id = reader.GetInt32(0);
                feature.VersionId = reader.GetInt32(1);
                feature.Name = reader.GetString(2);
                feature.Estimation = reader.GetInt32(3);
                feature.State = reader.GetInt32(4);
                feature.Employees = await GetEmployees(feature.Id);
                feature.Plannings = await GetPlannings(feature.Id);

                result.Add(feature);
            }
            Console.WriteLine("Feature list loaded");
        }
        else
        {
            Console.WriteLine("Feature not found.");
        }
        Db.Connection.Close();
        return result;
    }

    public async Task<Feature> GetById(int Id) 
    {
        Feature result = new Feature();

        Db.Sql = $"SELECT * FROM {nameof(Feature)} WHERE {nameof(Feature.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.VersionId = reader.GetInt32(1);
            result.Name = reader.GetString(2);
            result.Estimation = reader.GetInt32(3);
            result.State = reader.GetInt32(4);
            Console.WriteLine("Feature loaded successfully.");
        }
        else
        {
            Console.WriteLine("Feature not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }

    private async Task<List<Employee>> GetEmployees(int Id) 
    {
        List<Employee> result = new List<Employee>();

        RelationDb relationDb = new RelationDb();
        EmployeeDb employeeDb = new EmployeeDb();

        List<int> employeeIds = await relationDb.GetEmployeeIds(nameof(Relation.FeatureId), Id);
        foreach(int eId in employeeIds){
            result.Add(await employeeDb.GetById(eId));
        }
        
        return result;
    }

    private async Task<List<Planning>> GetPlannings(int Id) 
    {
        PlanningDb planningDb = new PlanningDb();
        return await planningDb.GetByFeatureId(Id);
    }
}
