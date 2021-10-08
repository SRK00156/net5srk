using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Core_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		// https://localhost:[port]/api/FileUpload/Upload
		/// <summary>
		/// IFormFile: The Contract that us used to Map the Http Request Containing File
		/// With the Current Method and use this file for Processing
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file)
		{
			try
			{
				// if file is present into the Http Request
				if (file.Length > 0)
				{
					// Set the Folder Path
					var folder = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

					// REad the File Name, File Type (ContentDisposition) and also an extension
					var fileName = ContentDispositionHeaderValue.Parse(
						file.ContentDisposition).FileName.Trim('"');

					// Set the Path for writing the file
					var filePath = Path.Combine(folder, fileName);
					// Upload the File
					using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
					{
						await file.CopyToAsync(fs);
					}
					return Ok("File is Uploaded Successfully");
				}
				else
				{
					return BadRequest("File is eith Empty or Damaged");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"File Upload Failed {ex.Message}");
			}
		}
	}
}
