using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repositories
{
    public interface IPdfFileRepo
    {
        Task AddAsync(PdfFile pdfFile);
        Task<IEnumerable<PdfFile>> GetAllAsync();
        Task<PdfFile> GetByIdAsync(int id);
        Task UpdateAsync(PdfFile pdfFile);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
