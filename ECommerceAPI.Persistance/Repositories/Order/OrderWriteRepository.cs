﻿public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ECommerceAPIContext context) : base(context)
    {
    }
}
