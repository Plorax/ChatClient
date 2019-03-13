using ServiceNode;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceNode.Services
{
  internal class CustomerService : ICustomerService
  {
    private ISmsService SmsService { get; }
    public CustomerService(ISmsService smsService)
    {
      SmsService = smsService;
    }
  }
}
