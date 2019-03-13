using Akka.Actor;
using Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.NonPersistentMessageBus.Actors
{
  public class CustomerGreeting : ReceiveActor
  {
    public CustomerGreeting()
    {
      Receive<GreetingResponse>((response) =>
      {
        Console.WriteLine($"Server Replied: {response.Message}");
      });
    }
  }
}
