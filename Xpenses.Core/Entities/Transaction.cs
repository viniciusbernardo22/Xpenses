using Xpenses.Core.Entities.Base;
using Xpenses.Core.Enums;

namespace Xpenses.Core.Entities;

public class Transaction : Entity
{
    public string Title { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string UserId { get; set; }= String.Empty;
    
}