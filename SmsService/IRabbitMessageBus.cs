using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceNode
{
  internal interface IRabbitMessageBus
  {
    void Init();
    void SendMessage<Message>(Message message);
    Response Ask<Response, Question>(Question question);
  }
}
