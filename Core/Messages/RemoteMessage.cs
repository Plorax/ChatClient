using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages
{
  public class RemoteMessage
  {
    public string RemoteUri { get; set; }
    public object Message { get; set; }

    public RemoteMessage(string remoteUri, object message)
    {
      RemoteUri = remoteUri;
      Message = message;
    }
  }
}
