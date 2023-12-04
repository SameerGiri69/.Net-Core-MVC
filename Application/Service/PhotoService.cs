using Application.RepositoryInterface;
using Application.ServiceInterface;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            return _photoRepository.AddPhotoAsync(file);
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            return _photoRepository.DeletePhotoAsync(publicId);
        }
    }
}
