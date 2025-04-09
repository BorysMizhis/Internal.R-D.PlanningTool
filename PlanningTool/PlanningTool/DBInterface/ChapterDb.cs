using System;
using Microsoft.Data.Sqlite;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class ChapterDb
{
    private DatabaseInterface Db;
    public ChapterDb(){
        Db = new DatabaseInterface();
    }

    public async Task Add(Chapter chapter) 
    {
        Db.Sql = $"INSERT INTO {nameof(Chapter)} ({nameof(Chapter.Name)}) VALUES( '{chapter.Name}');";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();

        Console.WriteLine("Chapter added successfully.");
    }

    public async Task Remove(int Id) 
    {
        Db.Sql = $"DELETE FROM {nameof(Chapter)} WHERE {nameof(Chapter.Id)} = {Id});";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        await command.ExecuteNonQueryAsync();
        Db.Connection.Close();        
        
        Console.WriteLine("Chapter removed successfully.");        
    }

    public async Task<List<Chapter>> GetAll() 
    {
        List<Chapter>? result = new List<Chapter>();

        Db.Sql = $"SELECT * FROM {nameof(Chapter)};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while(await reader.ReadAsync())
            {
                Chapter chapter = new Chapter();
                chapter. Id = reader.GetInt32(0);
                chapter.Name = reader.GetString(1);
                chapter.Employees = await GetEmployees(reader.GetInt32(0));

                result.Add(chapter);
            }
            Console.WriteLine("Chapter list loaded");
        }
        else
        {
            Console.WriteLine("Chapter not found.");
        }
        Db.Connection.Close();
        return result;
    }

    private async Task<List<Employee>> GetEmployees(int Id) 
    {
        List<Employee> result = new List<Employee>();

        RelationDb relationDb = new RelationDb();
        EmployeeDb employeeDb = new EmployeeDb();

        List<int> employeeIds = await relationDb.GetEmployeeIds(nameof(Relation.ChapterId), Id);
        foreach(int eId in employeeIds){
            result.Add(await employeeDb.GetById(eId));
        }
        
        return result;
    }

    public async Task<Chapter> GetById(int Id) 
    {
        Chapter result = new Chapter();

        Db.Sql = $"SELECT * FROM {nameof(Chapter)} WHERE {nameof(Chapter.Id)} = {Id};";
        Db.Connection.Open();
        
        using var command = new SqliteCommand(Db.Sql, Db.Connection);
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            result.Id = reader.GetInt32(0);
            result.Name = reader.GetString(1);
            Console.WriteLine("Chapter loaded successfully.");
        }
        else
        {
            Console.WriteLine("Chapter not found.");
        }
        Db.Connection.Close();        
        
        return result;
    }
}
