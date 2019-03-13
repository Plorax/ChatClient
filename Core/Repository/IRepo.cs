using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
  public interface IRepoItem
  {
    string id { get; set; }
  }

  public interface IRepo<T>
    where T : class, IRepoItem
  {
    void Add(T item);
    T Remove(T item, params string [] matchBy);
    T RemoveByID(string id);
  }
}
