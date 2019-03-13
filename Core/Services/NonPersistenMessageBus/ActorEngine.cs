using Akka.Actor;
using Akka.Configuration;
using Core.Messages;
using Core.Services.NonPersistentMessageBus.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.NonPersistenMessageBus.Actors
{
  public class ActorEngine : IActorEngine
  {
    private Config config { get; set; }
    private ActorSystem actorSystem { get; set; }

    private string AkkaServer => $"akka.tcp://{Server.host.Replace(".","")}@{Server.host}:{Server.port}";

    public (string host, string port) Server { get; internal set; }
    public (string host, string port) Client { get; internal set; }

    // "Akka.Remote.Transport.DotNetty.DotNettyTransport, Akka.Remote""

    private string DefaultHoconConfig(string port, string host) => @"
        akka {
            # here we are configuring log levels
            log-config-on-start = off
            stdout-loglevel = INFO
            loglevel = ERROR
            # this config section will be referenced as akka.actor
            actor {
              provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
              deployment {
                  /remoteincoming {
                      remote = ""akka.tcp://smsowcio@sms-owc-io:9000""
                  }
              }
              debug {
                  receive = on
                  autoreceive = on
                  lifecycle = on
                  event-stream = on
                  unhandled = on
              }
            }
            # here we're configuring the Akka.Remote module
            remote {
              helios.tcp {
                  port = """ + port + @"""
                  hostname = ""0.0.0.0""
                  public-hostname = """ + host + @"""
              }
            log-remote-lifecycle-events = INFO
        }";

    public ActorEngine()
    {

    }

    public IActorRef Of<T>()
      where T : ActorBase, new()
    {
      return actorSystem.ActorOf<T>();
    }

    public void TellSource<T,A>(T message) where T : class where A : ActorBase, new ()
    {
      var remoteActorAddress = Address.Parse(AkkaServer);
      var actor = actorSystem.ActorOf(Props.Create(() => new A()).WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteActorAddress))));
      actor.Tell(message);
    }

    public void Tell<T>(object message)
      where T : ActorBase
    {
      var actor = actorSystem.ActorOf(Props.Create<T>(SupervisorStrategy.DefaultDecider));
      actor.Tell(message);
    }

    public void Init((string host, string port) server, (string host, string port) client)
    {
      Server = server;
      Client = client;

      config = ConfigurationFactory.ParseString(DefaultHoconConfig(Client.port, Client.host));

      var actorSystemQualifiedName = Client.host.Replace(".", "");
      actorSystem = ActorSystem.Create(actorSystemQualifiedName, config);
    }
  }
}
