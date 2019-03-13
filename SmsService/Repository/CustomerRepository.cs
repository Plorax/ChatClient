using Core.DataEntities;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceNode.Repository
{
  internal class CustomerRepository : IRepo<Customer>
  {
    private readonly List<Customer> items = new List<Customer>();
    private object repoLock = new object();

    public void Add(Customer item)
    {
      lock (repoLock)
      {
        items.Add(item);
      }
    }

    public Customer Remove(Customer item, params string[] matchBy)
    {
      lock (repoLock)
      {
        var originalItemIndex = items.IndexOf(item);
        var originalItem = items[originalItemIndex];

        items.RemoveAt(originalItemIndex);

        return originalItem;
      }
    }

    public Customer RemoveByID(string id)
    {
      lock (repoLock)
      {
        var itemIndex = items.FindIndex(m => string.Compare(m.id, id, false) == 0);
        var originalItem = items[itemIndex];

        items.RemoveAt(itemIndex);

        return originalItem;
      }
    }
  }
}
