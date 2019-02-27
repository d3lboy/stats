using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Player : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }

        public short Height { get; set; }
        public PlayerPosition Position { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
