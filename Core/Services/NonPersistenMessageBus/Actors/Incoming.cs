using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Akka.Actor;
using Akka.Util;
using Core;
using Core.Messages;

namespace Core.Services.NonPersistentMessageBus.Actors
{
  public class Incoming : ReceiveActor
  {
    public Incoming()
    {
      Receive<InitActorSystem>((init) =>
      {
        Console.Clear();
        Console.Write("Akka.Net has booted.");
      });

      Receive<CoreHeartBeat>((heartBeat) =>
      {
        if (!string.IsNullOrEmpty(heartBeat.Message))
        {
          Console.WriteLine(heartBeat.Message);
        }
        else
        {
          Console.Write(".");
        }
      });

      Receive<GreetCustomer>((greeting) =>
      {
        if (string.Compare(greeting.Greeting, "goodbye", true) == 0)
        {
          Console.WriteLine("good bye");
          IAppService appService = IoC.GetContainer().ResolveType<IAppService>();
          appService.Shutdown();
        }

        if (string.Compare(greeting.Greeting, "hello", true) == 0)
        {
          Console.WriteLine($"Replying to Client with ... Hello {greeting.Name}, I am your father!");
          IActorEngine engine = IoC.GetContainer().ResolveType<IActorEngine>();
          engine.TellSource<GreetingResponse, CustomerGreeting>(new GreetingResponse($"Hello {greeting.Name}, I am your father!"));
        }
      });
    }
  }
}
