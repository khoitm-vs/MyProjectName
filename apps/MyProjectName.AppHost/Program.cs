using Microsoft.Extensions.Hosting;
using Projects;

namespace MyProjectName.AppHost;

internal class Program
{
    private static void Main(string[] args)
    {
        const string LaunchProfileName = "Aspire";
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder.AddSqlServer(MyProjectNameNames.SqlServer);
        var kafka = builder.AddKafka("kafka");
        //var redis = builder.AddRedis(MyProjectNameNames.Redis).WithRedisCommander();
        var seq = builder.AddSeq(MyProjectNameNames.Seq);

        var adminDb = postgres.AddDatabase(MyProjectNameNames.AdministrationDb);
        var identityDb = postgres.AddDatabase(MyProjectNameNames.IdentityServiceDb);
        var projectsDb = postgres.AddDatabase(MyProjectNameNames.ProjectsDb);
        var saasDb = postgres.AddDatabase(MyProjectNameNames.SaaSDb);

        var migrator = builder
            .AddProject<MyProjectName_DbMigrator>(
                MyProjectNameNames.DbMigrator,
                launchProfileName: LaunchProfileName
            )
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(projectsDb)
            .WithReference(saasDb)
            .WithReference(seq)
            .WaitFor(postgres);

        var admin = builder
            .AddProject<MyProjectName_Administration_HttpApi_Host>(
                MyProjectNameNames.AdministrationApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(kafka)
            //.WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var identity = builder
            .AddProject<MyProjectName_IdentityService_HttpApi_Host>(
                MyProjectNameNames.IdentityServiceApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(kafka)
            //.WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var saas = builder
            .AddProject<MyProjectName_SaaS_HttpApi_Host>(
                MyProjectNameNames.SaaSApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(saasDb)
            .WithReference(kafka)
            //.WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<MyProjectName_Projects_HttpApi_Host>(
                MyProjectNameNames.ProjectsApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(projectsDb)
            .WithReference(kafka)
            //.WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var gateway = builder
            .AddProject<MyProjectName_Gateway>(MyProjectNameNames.Gateway, launchProfileName: LaunchProfileName)
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(admin)
            .WaitFor(identity)
            .WaitFor(saas);

        var authserver = builder
            .AddProject<MyProjectName_AuthServer>(
                MyProjectNameNames.AuthServer,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(kafka)
            //.WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<MyProjectName_WebApp_Blazor>(
                MyProjectNameNames.WebAppClient,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(authserver)
            .WaitFor(gateway);

        builder.Build().Run();
    }
}
