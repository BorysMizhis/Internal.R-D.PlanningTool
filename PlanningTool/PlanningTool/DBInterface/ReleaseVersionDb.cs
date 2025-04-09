using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class ReleaseVersionDb
{
    private DatabaseInterface Db;
    public ReleaseVersionDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(ReleaseVersion releaseVersion) 
    {
        Db.Sql = $"INSERT INTO {nameof(ReleaseVersion)} ({nameof(ReleaseVersion.Name)}, {nameof(ReleaseVersion.State)}) VALUES( '{releaseVersion.Name}', {releaseVersion.State});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("ReleaseVersion added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(ReleaseVersion)} WHERE {nameof(ReleaseVersion.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("ReleaseVersion removed successfully.");        
    }

    public async Task<List<Models.ReleaseVersion>> GetAll() 
    {
        List<ReleaseVersion>? result = new List<ReleaseVersion>();

        Db.Sql = $"SELECT * FROM {nameof(ReleaseVersion)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                result.Add(new ReleaseVersion() {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    State = reader.GetInt32(2),
                    Features = await GetFeatures(reader.GetInt32(0))
                });
            }
            Console.WriteLine("ReleaseVersion list loaded");
        }
        else
        {
            Console.WriteLine("ReleaseVersion not found.");
        }
        Db.Connection.Close();
        return result;
    }

    public async Task<ReleaseVersion> GetById(int Id) 
    {
        ReleaseVersion result = new ReleaseVersion();

        Db.Sql = $"SELECT * FROM {nameof(ReleaseVersion)} WHERE {nameof(ReleaseVersion.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.Name = reader.GetString(1);
            result.State = reader.GetInt32(2);
            Console.WriteLine("ReleaseVersion loaded successfully.");
        }
        else
        {
            Console.WriteLine("ReleaseVersion not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }

    private async Task<List<Feature>> GetFeatures(int Id) 
    {
        List<Feature> result = new List<Feature>();

        RelationDb relationDb = new RelationDb();
        FeatureDb employeeDb = new FeatureDb();

        List<int> featureIds = await relationDb.GetFeatureIdsByReleaseVersionId(Id);
        foreach(int fId in featureIds){
            result.Add(await employeeDb.GetById(fId));
        }
        
        return result;
    }
}
