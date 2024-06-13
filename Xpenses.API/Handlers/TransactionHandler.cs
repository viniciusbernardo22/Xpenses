using Microsoft.EntityFrameworkCore;
using Xpenses.API.Data;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Transactions;
using Xpenses.Core.Responses;

namespace Xpenses.API.Handlers;

public class TransactionHandler(AppDbContext ctx) : ITransactionHandler
{
    private readonly string errorFlag = "[TransactionHandler]";
    
    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction =
                await ctx.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return transaction is null 
                ? new Response<Transaction?>(null, 404, $"{errorFlag} - A Transação não existe.") 
                : new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, $"{errorFlag} - ${e.Message}");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await ctx.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, $"{errorFlag} - A Transação não existe.");
                            
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

            ctx.Transactions.Update(transaction);
            await ctx.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, $"{errorFlag} - {e.Message}");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await ctx.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            
            if (transaction is null)
                return new Response<Transaction?>(null, 404, $"{errorFlag} - A Transação não existe.");
            
            ctx.Transactions.Remove(transaction);
            await ctx.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, $"{errorFlag} - {e.Message}");
        }
    }

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };

            await ctx.Transactions.AddAsync(transaction);
            await ctx.SaveChangesAsync();
            return new Response<Transaction?>(transaction, 201, message: "Transação criada com sucesso");
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, $"{errorFlag} - ${e.Message}");
        }
    }
}