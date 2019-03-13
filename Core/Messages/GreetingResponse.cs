using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages
{
  public class GreetingResponse
  {
    public string Message { get; set; }

    public GreetingResponse(string message) => Message = message;
  }
}
