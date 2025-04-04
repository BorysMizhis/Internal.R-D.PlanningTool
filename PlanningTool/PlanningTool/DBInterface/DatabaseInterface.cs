using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class DatabaseInterface: DbContext
{
    //public DbSet<Feature> Features { set; get; }
        
    public string DbPath { get; } = "Database.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        => optionbuilder.UseSqlite($"Data Source={DbPath}");    

    public void CreateTable()
    {
        string sql;
        SqliteCommand command;
        using var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(CapacityCorrection)} ({nameof(CapacityCorrection.Id)} INTEGER PRIMARY KEY, {nameof(CapacityCorrection.EmployeeId)} INTEGER NOT NULL, {nameof(CapacityCorrection.CorrectionYear)} INTEGER NOT NULL, {nameof(CapacityCorrection.CorrectionMonth)} INTEGER NOT NULL, {nameof(CapacityCorrection.CorrectionValue)} INTEGER NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Chapter)} ({nameof(Chapter.Id)} INTEGER PRIMARY KEY, {nameof(Chapter.ChapterName)} TEXT NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Employee)} ({nameof(Employee.Id)} INTEGER PRIMARY KEY, {nameof(Employee.Name)} TEXT NOT NULL, {nameof(Employee.DefaultCapacity)} INTEGER NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Feature)} ({nameof(Feature.Id)} INTEGER PRIMARY KEY, {nameof(Feature.VersionId)} INTEGER NOT NULL, {nameof(Feature.Name)} TEXT NOT NULL, {nameof(Feature.Estimation)} INTEGER, {nameof(Feature.State)} INTEGER NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(ReleaseVersion)} ({nameof(ReleaseVersion.Id)} INTEGER PRIMARY KEY, {nameof(ReleaseVersion.Name)} TEXT NOT NULL, {nameof(ReleaseVersion.State)} INTEGER NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Relations)} ({nameof(Relations.Id)} INTEGER PRIMARY KEY, {nameof(Relations.ChapterId)} INTEGER, {nameof(Relations.EmployeeId)} INTEGER, {nameof(Relations.FeatureId)} INTEGER, {nameof(Relations.ReleaseVersionId)} INTEGER)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        connection.Close();
        Console.WriteLine("Tables created successfully.");
    }

    public void AddEntry(){

        var sql = @"INSERT INTO authors (first_name,last_name) VALUES( 'test12', 'value22');";

        using var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();

        using var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Entry 'authors' added successfully.");
    }

    public void DropTable(){
        string sql;
        SqliteCommand command;
        using var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();

        sql = $"DROP TABLE {nameof(CapacityCorrection)};";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"DROP TABLE {nameof(Chapter)};";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Tables dropped!.");
        connection.Close();
    }

    public void ReadTable()
    {
        /*await using var db = new DatabaseInterface();

        Console.WriteLine($"DB Path: {db.DbPath}");

        var result = 
        from feature in db.Authors
        select feature;

        await foreach(var a in result.AsAsyncEnumerable())
        {
            Console.WriteLine(a.first_name);
        }*/
    }
}
