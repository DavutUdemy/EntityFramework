using System;
using System.Collections.Generic;
using Core.Entities;

namespace Entities.DTOs
{
    public class BasketDetailDto:IDto
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserFullName { get; set; }
        public decimal Price { get; set; } 
        public int Count { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}