using BicycleStore.Core.Infrastructure.Interfaces;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Models
{
  public class Photo : IGuidKey
    {
        public Guid Id { get; set; }

        public string Base64Photo { get; set; }
    }
}
