using Core;
using Core.Messages;
using Core.Services.NonPersistenMessageBus.Actors;
using Core.Services.NonPersistentMessageBus.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
  class Program
  {
    static void OnBoot(IContainerInit adapter)
    {
      adapter.RegisterSingleton<IActorEngine, ActorEngine>();
    }

    private static (string host, string port) Server = ("sms.owc.io", "9000");
    private static (string host, string port) Client = ("sms.owc.io", "9001");

    private static List<string> standardCommands = new List<string> { "hello <name of person> - sends a hello to server and server replies back", "goodbye - sends goodbye message to server and quits client app", "quit - close client", "\r\n" };

    static void Main(string[] args)
    {
      var container = IoC.Boot(OnBoot);

      IActorEngine actorEngine;
      container.ResolveType(out actorEngine);

      actorEngine.Init(Server, Client);

      bool quit = false;

      Console.WriteLine(string.Join("\r\n", standardCommands));

      while (!quit)
      {
        string userInput = Console.ReadLine();

        var input = userInput.Split(',', ' ');
        var greeting = "";
        if (input.Length > 0)
        {
          greeting = input[0];
        }

        var customerName = "";

        if (input.Length > 1)
        {
          customerName = input[1];
        }

        actorEngine.TellSource<CoreHeartBeat, Incoming>(new CoreHeartBeat($"from client {userInput}"));

        if (!string.IsNullOrEmpty(userInput))
        {
          if (string.Compare(userInput, "goodbye", true) == 0)
          {
            actorEngine.TellSource<GreetCustomer, Incoming>(new GreetCustomer(customerName, greeting));
            quit = true;
            continue;
          }

          if (string.Compare(userInput, "quit", true) == 0)
          {
            quit = true;
            continue;
          }
        }

        actorEngine.TellSource<GreetCustomer, Incoming>(new GreetCustomer(customerName, greeting));
      }
    }
  }
}
