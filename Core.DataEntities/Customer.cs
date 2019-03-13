using Core.Repository;
using System;

namespace Core.DataEntities
{
  public class Customer : IRepoItem
  {
    public string id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Title { get; set; }

    public Customer(string id) => this.id = id;
  }
}
