using Core;
using Core.Services.NonPersistenMessageBus.Actors;
using ServiceNode.Services;

namespace ServiceNode
{
  class Program
  {
    static void onBoot(IContainerInit adapter)
    {
      adapter.RegisterSingleton<ISmsService, SmsService>();
      adapter.RegisterSingleton<ICustomerService, CustomerService>();
      adapter.RegisterSingleton<IRabbitMessageBus, ServiceEndpoint>();
      adapter.RegisterSingleton<IAppService, AppService>();
      adapter.RegisterSingleton<IActorEngine, ActorEngine>();
    }

    static void Main(string[] args)
    {
      // boot container
      var container = IoC.Boot(onBoot);

      // get app service
      IAppService app = container.ResolveType<IAppService>();

      // init rabbit and actor system
      app.InitRabbit();
      app.InitActorSystem();

      app.Run();
    }
  }
}
