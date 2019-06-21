﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsDeleted { get; set; }
        }

      }