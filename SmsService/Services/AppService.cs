using Core;
using Core.Messages;
using Core.Services.NonPersistentMessageBus.Actors;
using System;
using System.Threading;

namespace ServiceNode.Services
{
  internal class AppService : IAppService
  {
    private IRabbitMessageBus bus { get; }
    private IActorEngine actorEngine { get; }
    private bool shutdown { get; set; }
    private Thread appThread { get; set; }

    private (string host, string port) Server = ("sms.owc.io", "9001");
    private (string host, string port) Client = ("sms.owc.io", "9000");


    public AppService(IRabbitMessageBus bus, IActorEngine actorEngine)
    {
      this.bus = bus;
      this.actorEngine = actorEngine;
    }

    public void InitRabbit()
    {
      bus.Init();
      actorEngine.Init(Server, Client);
    }

    public void Run()
    {
      appThread = new Thread(() =>
      {
        var startTime = DateTime.Now;
        while (!shutdown)
        {
          // REST
          if ((DateTime.Now - startTime).TotalSeconds > 1.5)
          {
            startTime = DateTime.Now;
            actorEngine.Of<Incoming>().Tell(new CoreHeartBeat(), null);
          }
        }
      });

      appThread.Start();
      appThread.Join();
    }

    public void InitActorSystem()
    {
      actorEngine.Of<Incoming>().Tell(new InitActorSystem(), null);
    }

    public void Shutdown()
    {
      shutdown = true;
      appThread.Abort();
    }
  }
}
