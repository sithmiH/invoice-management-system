using Dapper;
using InvoiceManagement.API.Interfaces.Repositories;
using InvoiceManagement.API.Models;
using InvoiceManagement.API.Data;

namespace InvoiceManagement.API.Repositories;

// Repository responsible for performing CRUD operations on the Invoices table using Dapper as the micro-ORM for data access
public class InvoiceRepository : IInvoiceRepository
{
    private readonly DapperContext _context;

    public InvoiceRepository(DapperContext context)
    {
        _context = context;
    }

    // Retrieves all invoices
    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        const string query = @"
            SELECT *
            FROM Invoices
            ORDER BY CreatedDate DESC";

        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<Invoice>(query);
    }

    // Retrieves an invoice by Id
    public async Task<Invoice?> GetInvoiceByIdAsync(int id)
    {
        const string query = @"
            SELECT *
            FROM Invoices
            WHERE InvoiceId = @Id";

        using var connection = _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Invoice>(
            query,
            new { Id = id });
    }

    // Creates a new invoice
    public async Task<int> CreateInvoiceAsync(Invoice invoice)
    {
        const string query = @"
            INSERT INTO Invoices
            (
                InvoiceNumber,
                CustomerName,
                Amount,
                Status
            )
            VALUES
            (
                @InvoiceNumber,
                @CustomerName,
                @Amount,
                @Status
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(query, invoice);
    }

    // Updates an invoice
    public async Task<bool> UpdateInvoiceAsync(Invoice invoice)
    {
        const string query = @"
            UPDATE Invoices
            SET
                CustomerName = @CustomerName,
                Amount = @Amount,
                Status = @Status
            WHERE InvoiceId = @InvoiceId";

        using var connection = _context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(query, invoice);

        return rowsAffected > 0;
    }

    // Deletes an invoice
    public async Task<bool> DeleteInvoiceAsync(int id)
    {
        const string query = @"
            DELETE FROM Invoices
            WHERE InvoiceId = @Id";

        using var connection = _context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(
            query,
            new { Id = id });

        return rowsAffected > 0;
    }
}
