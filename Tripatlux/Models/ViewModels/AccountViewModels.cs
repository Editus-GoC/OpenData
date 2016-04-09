using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tripatlux.Models.DataTrip;

namespace Tripatlux.Models
{
    public class SetCodeViewModel
    {
        public Trip Trip { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
