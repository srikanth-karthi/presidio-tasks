﻿using Shopping_DAL;
using ShoppingApp_Model_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_BLL
{
    public class OrderServices:AbstractRepository<Orders,int>
    {
        public override Orders Get(int key) => items.FirstOrDefault(item => item.OrderId == key) ?? throw new KeyNotFoundException("Item not found");

        public override Orders Add(Orders item)
        {
            item.OrderId = GenerateId();
            base.Add(item);
            return item;
        }



        public override Orders Update(Orders item)
        {
            int index = items.ToList().FindIndex(p => p.OrderId == item.OrderId);

            if (index != -1)
            {
                items[index] = item;
                return item;
            }
            throw new KeyNotFoundException($"{item.OrderId} not found");
        }
    }
}

