using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitcoinAppMvc.Models
{
    [Index(nameof(UTCCreated))]
    [Index(nameof(Entity_Id))]
    public class TransactionData : ITransactionData
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime UTCCreated { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UTCChanged { get; set; } = DateTime.UtcNow;

        [Required]
        public string Entity_Id { get; set; }
        [Required]
        public string System_Id { get; set; }
        [Required]
        public string DocumentType { get; set; }

        public string DataXml { get; set; }

        [Required]
        public PublicationStatus PublicationStatus { get; set; }

        public string ExternalTransactionId { get; set; }
    }
}
