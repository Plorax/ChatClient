using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
  public interface IContainer
  {
    T ResolveType<T>() 
      where T : class;

    IContainer ResolveType<T>(out T resolvedType) 
      where T : class;
  }

  public interface IContainerInit
  {
    void RegisterSingleton<T, C>() 
      where C : class, T 
      where T : class;

    void RegisterType<I, C>() 
      where C : class;
  }

  internal interface IContainerBoot
  {
    void Boot(Action<IContainerInit> onBoot);
  }

  internal class Container : IContainer, IContainerBoot, IContainerInit
  {
    private ContainerBuilder builder { get; set; }
    private Autofac.IContainer container { get; set; }

    public Container()
    {
      builder = new ContainerBuilder();
    }

    public void RegisterSingleton<T, C>() 
      where C : class, T
      where T : class
    {
      builder.RegisterType<C>().As<T>().SingleInstance();
    }

    public void RegisterType<I,C>() 
      where C : class
    {
      builder.RegisterType<I>().As<C>();
    }

    public T ResolveType<T>() 
      where T : class
    {
      return container.Resolve<T>();
    }

    public void Boot(Action<IContainerInit> onBoot)
    {
      onBoot(this);

      container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
    }

    public IContainer ResolveType<T>(out T resolvedType) where T : class
    {
      resolvedType = container.Resolve<T>();
      return this;
    }
  }
}
