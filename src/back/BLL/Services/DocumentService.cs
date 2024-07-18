using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using DAL.EF;
using DAL.Entities;
using DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Services
{

    public class DocumentService
    {
        private AppDbContext _appDbContext;

        public DocumentService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(DocumentDto.Add dto, (Stream Source, string FileName) file)
        {
            if (!file.FileName.CheckAllFormats())
                throw new InnerException("Invalid files. Only word, pdf, and excel files accepted.");
            if (file.Source.Length >= AppConstants.MaxSizeOfDocument)
                throw new InnerException("Only files with a size of no more than 30 are accepted");
            if (await _appDbContext.Documents.AnyAsync(x => x.Name == file.FileName))
                throw new InnerException("File with same name already exists in the database");

            var path = AppConstants.RelativeFilesPath.Combine(AppConstants.BaseDir, AppConstants.DocumentDir, file.FileName);
            var relativePath = AppConstants.RelativeFilesPath.Combine(AppConstants.DocumentDir, file.FileName);
            await (path, file.Source).SaveStreamByPath();

            var entity = new Document()
            {
                Name = file.FileName,
                Path = relativePath,
                CreatedDateTime = DateTime.Now,
            };

            _appDbContext.Documents.Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var document = await _appDbContext.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
                throw new InnerException("Document does not exist.");

            _appDbContext.Documents.Remove(document);
            File.Delete(document.Path);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<DocumentDto.Get>> ListAsync()
        {
            TypeAdapterConfig<Document, DocumentDto.Get>
                                        .NewConfig()
                                        .Map(dest => dest.Path, src => $"{AppConstants.BaseUri}{src.Path.Replace("\\", "/")}");
            return _appDbContext.Documents.Adapt<List<DocumentDto.Get>>();
        }
    }
}