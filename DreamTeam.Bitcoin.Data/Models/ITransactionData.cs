using System;

namespace BitcoinAppMvc.Models
{
    public interface ITransactionData:IEntity
    {
        string DataXml { get; set; }
        string DocumentType { get; set; }
        string Entity_Id { get; set; }
        string ExternalTransactionId { get; set; }
       
        PublicationStatus PublicationStatus { get; set; }
        string System_Id { get; set; }
    }

    public interface IEntity {
        Guid ID { get; set; }
        DateTime UTCChanged { get; set; }
        DateTime UTCCreated { get; set; }
    }
}