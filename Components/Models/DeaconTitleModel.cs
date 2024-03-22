using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeaconAdminHybrid.Components.Models
{
    [Table("DeaconTitle")]
    public class DeaconTitleModel
    {
        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid TitleId { get; set; }
        public string? DeaconTitle { get; set; }
        public string? DeaconPosition { get; set; }
    }

    public class StateModel
    {
        public string? StateAbbreviation { get; set; }

        public string? StateName { get; set; }
    }
}
