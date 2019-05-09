using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Reference : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Common.Enums.Competition Competition { get; set; }
        public ReferenceType Type { get; set; }

        [MaxLength(64)]
        public string LocalValue { get; set; }

        [MaxLength(64)]
        public string ReferenceValue { get; set; }
    }
}
