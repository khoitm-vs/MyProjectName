using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddSharedEndpoints(this IHostApplicationBuilder builder)
    {
        builder.AddRabbitMQClient(
            connectionName: MyProjectNameNames.RabbitMq,
            action =>
                action.ConnectionString = builder.Configuration.GetConnectionString(
                    MyProjectNameNames.RabbitMq
                )
        );
        builder.AddRedisDistributedCache(connectionName: MyProjectNameNames.Redis);
        builder.AddSeqEndpoint(connectionName: MyProjectNameNames.Seq);

        return builder;
    }
}
