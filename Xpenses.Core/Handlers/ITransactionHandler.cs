using Xpenses.Core.Entities;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Requests.Transactions;
using Xpenses.Core.Responses;

namespace Xpenses.Core.Handlers;

public interface ITransactionHandler
{
    Task<PagedResponse<Transaction?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);

    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
 
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);

    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);

    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);

}