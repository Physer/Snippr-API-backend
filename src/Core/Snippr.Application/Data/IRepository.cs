using Snippr.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Snippr.Application.Data
{
    public interface IRepository
    {
        Task DeleteSnippet(Guid snippetId);
        Task<Snippet> GetSnippet(Guid snippetId);
        Task UpdateSnippet(Guid snippetId, Snippet newSnippet);
        Task WriteSnippet(Snippet snippet);
    }
}
