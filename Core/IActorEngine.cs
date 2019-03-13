using Akka.Actor;
using Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
  public interface IActorEngine
  {
    void Init((string host, string port) server, (string host, string port) client);
    IActorRef Of<T>() where T : ActorBase, new();
    void Tell<T>(object message) where T : ActorBase;
    void TellSource<T, A>(T message) where T : class where A : ActorBase, new();
  }
}
