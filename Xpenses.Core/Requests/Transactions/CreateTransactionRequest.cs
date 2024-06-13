using System.ComponentModel.DataAnnotations;
using Xpenses.Core.Enums;

namespace Xpenses.Core.Requests.Transactions;

public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "O titulo é obrigatório")]
    [MaxLength(80)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Tipo é obrigatório")]
    public ETransactionType Type { get; set; }

    [Required(ErrorMessage = "Id da categoria é obrigatório")]
    public long CategoryId { get; set; }
    
    [Required(ErrorMessage = "Valor é obrigatória inválida")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Data de pagamento ou recebimento é obrigatória")]
    public DateTime PaidOrReceivedAt { get; set; }

}