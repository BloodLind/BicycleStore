using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Core.Infrastructure.Interfaces
{
    public interface IGuidKey
    {
        [Key]
        Guid Id { get; set; }
    }
}
