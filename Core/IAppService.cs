using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
  public interface IAppService
  {
    void InitRabbit();
    void InitActorSystem();
    void Run();
    void Shutdown();
  }
}
