using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages
{
  public class GreetCustomer
  {
    public string Name { get; set; }

    public string Greeting { get; set; }

    public GreetCustomer(string name, string greeting)
    {
      Name = name;
      Greeting = greeting;
    }
  }
}
