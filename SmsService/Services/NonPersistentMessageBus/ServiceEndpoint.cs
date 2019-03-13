using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceNode.Services
{
  internal class ServiceEndpoint : IRabbitMessageBus
  {
    public Response Ask<Response, Question>(Question question)
    {
      throw new NotImplementedException();
    }

    public void Init()
    {
      
    }

    public void SendMessage<Message>(Message message)
    {
      throw new NotImplementedException();
    }
  }
}
