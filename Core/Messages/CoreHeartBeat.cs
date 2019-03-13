using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages
{
  public class CoreHeartBeat
  {
    public string Message { get; set; }

    public CoreHeartBeat()
    {
      
    }

    public CoreHeartBeat(string additionalMessage)
    {
      Message = additionalMessage;
    }
  }
}
