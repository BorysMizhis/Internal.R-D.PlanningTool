using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PlanningTool.Models;

namespace PlanningTool.DBInterface;

public class DatabaseInterface: DbContext
{        
    public string DbPath { get; } = "Database.db";
    public string? Sql { set; get; }
    public SqliteConnection Connection;

    public DatabaseInterface(){
        Connection = new SqliteConnection($"Data Source={DbPath}");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        => optionbuilder.UseSqlite($"Data Source={DbPath}");    

    public void CreateTable()
    {
        string sql;
        SqliteCommand command;
        using var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(CapacityCorrection)} ({nameof(CapacityCorrection.Id)} INTEGER PRIMARY KEY, {nameof(CapacityCorrection.EmployeeId)} INTEGER NOT NULL, {nameof(CapacityCorrection.Year)} INTEGER NOT NULL, {nameof(CapacityCorrection.Month)} INTEGER NOT NULL, {nameof(CapacityCorrection.Value)} INTEGER NOT NULL)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Chapter)} ({nameof(Chapter.Id)} INTEGER PRIMARY KEY, {nameof(Chapter.Name)} TEXT NOT NULL)";
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

        sql = $"CREATE TABLE IF NOT EXISTS {nameof(Relation)} ({nameof(Relation.Id)} INTEGER PRIMARY KEY, {nameof(Relation.ChapterId)} INTEGER, {nameof(Relation.EmployeeId)} INTEGER, {nameof(Relation.FeatureId)} INTEGER, {nameof(Relation.ReleaseVersionId)} INTEGER)";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        connection.Close();
        Console.WriteLine("Tables created successfully.");
    }

    public void DropTable(){
        string sql;
        SqliteCommand command;
        using var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();

        sql = $"DROP TABLE {nameof(Chapter)};";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();

        /*sql = $"DROP TABLE {nameof(Chapter)};";
        command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();*/

        Console.WriteLine("Tables dropped!.");
        connection.Close();
    }
}
