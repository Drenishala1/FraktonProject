using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FraktonProject.Data;
using Microsoft.EntityFrameworkCore;

namespace FraktonProject.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        public int? BelongsToUserId { get; set; }
        public ApplicationUser BelongsToUser { get; set; }

        public int? RevokedByTokenId { get; set; }
        public RefreshToken RevokedByToken { get; set; }

        public int? RevokedByUserId { get; set; }
        public ApplicationUser RevokedByUser { get; set; }

        public DateTime? RevokedOn { get; set; }
    }
}
