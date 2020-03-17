using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Repositories;
using MusicManager.Web.API.Domain.Services;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Services
{
	public class ImageRefService : IImageRefService
	{
		private readonly IImageRefRepository _imageRefRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ImageRefService(IImageRefRepository ImageRefRepository, IUnitOfWork unitOfWork)
		{
			_imageRefRepository = ImageRefRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<ImageRefResponse> SaveAsync(ImageRef imageRef)
		{
			try
			{
				await _imageRefRepository.AddAsync(imageRef);
				await _unitOfWork.CompleteAsync();

				return new ImageRefResponse(imageRef);
			} catch (Exception ex) {
				return new ImageRefResponse($"An error occurred when saving the ImageRef, exception: {ex.Message}");
			}
		}

		public async Task<ImageRefResponse> UpdateAsync(int id, ImageRef imageRef)
		{
			var existingImageRef = await _imageRefRepository.FindByIdAsync(id);

			if (existingImageRef == null)
			{
				return new ImageRefResponse("Genre not found.");
			}

			existingImageRef.URI = imageRef.URI;
			existingImageRef.DateModified = DateTime.Now;

			try
			{
				_imageRefRepository.Update(existingImageRef);
				await _unitOfWork.CompleteAsync();

				return new ImageRefResponse(existingImageRef);
			}
			catch (Exception ex)
			{
				return new ImageRefResponse($"An error occurred when updating the image ref, exception: {ex.Message}");
			}
		}
	}
}
