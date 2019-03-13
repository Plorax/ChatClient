using Akka.Actor;
using Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.NonPersistentMessageBus.Actors
{
  public class RemoteTo : ReceiveActor
  {
    public RemoteTo()
    {
      Receive<RemoteMessage>(msg =>
      {
        var container = IoC.GetContainer();
        var actorEngine = container.ResolveType<IActorEngine>();
      });
    }
  }
}
