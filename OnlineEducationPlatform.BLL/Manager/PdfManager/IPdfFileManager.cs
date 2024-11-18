using OnlineEducationPlatform.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager
{
    public interface IPdfFileManager
    {
        Task AddAsync(PdfFileAddVm pdfFileAddDto);
        Task<IEnumerable<PdfFileReadVm>> GetAllAsync();
        Task<PdfFileReadVm> GetByIdAsync(int id);
        Task<PdfFileUpdateVm> UpdateAsync(PdfFileUpdateVm pdfFileUpdateDto);
        Task<bool> DeleteAsync(int id);

    }
}
