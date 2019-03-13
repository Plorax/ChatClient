using System;

namespace Core
{
  public static class IoC
  {
    private static Container container { get; set; } = null;
    private static object containerLock = new object();

    static IoC()
    {
      if (container == null)
      {
        lock (containerLock)
        {
          if (container == null)
          {
            container = new Container();
          }
        }
      }
    }

    public static IContainer Boot(Action<IContainerInit> onBoot)
    {
      container.Boot(onBoot);

      return container;
    }

    public static IContainer GetContainer() => container;
  }
}
