using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TComputerShop.DataAccess.Repository.IRepository;
using TComputerShop.Models;
using TComputerShop.Models.ViewModels;

namespace TComputerShop.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly ICategoryRepository _iCatRepo;
        private readonly IProductRepository _iProdRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private ProductVM ProdVM;
        private ProdIndexVM ProdIndexVM;

        public ProductController(ICategoryRepository iCatRepo,
                                 IProductRepository iProdRepo,
                                 IWebHostEnvironment hostEnvironment)
        {
            _iCatRepo = iCatRepo;
            _iProdRepo = iProdRepo;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ProdIndexVM = new ProdIndexVM()
            {
                Products = _iProdRepo.GetAll("Category")                
            };
            return View(ProdIndexVM);
        }
        public IActionResult Add() 
        {
            ProdVM = new ProductVM()
            {
                Product = new Models.Product(),
                CategoryList = _iCatRepo.GetCategoryListForDropdown()
            };
            return View(ProdVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductVM prodVM)
        {
            if(ModelState.IsValid)
            {

                string webRootPath = _hostEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                if(files.Count > 0)
                {

                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"images\products");

                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    string srcImage_Path = webRootPath + @"\images\products\" + fileName + extension;

                    string resizeImage_Path = webRootPath + @"\images\products\" + fileName + "resized" + extension;

                    int new_Size = 600;

                    Image_resize(srcImage_Path, resizeImage_Path, new_Size);


                    prodVM.Product.ImageUrl = @"\images\products\" + fileName + "resized" + extension;

                    //prodVM.Product.ImageUrl = @"\images\products\" + fileName + extension;

                }

                _iProdRepo.Add(prodVM.Product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProdVM = new ProductVM()
            {
                Product = _iProdRepo.GetFirstOrDefault(id),
                CategoryList = _iCatRepo.GetCategoryListForDropdown()
            };

            return View(ProdVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM prodVM)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"images\products");

                    var extension = Path.GetExtension(files[0].FileName);

                    if(prodVM.Product.ImageUrl != null)
                    { 
                    var imagePath = Path.Combine(webRootPath, prodVM.Product.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }       
                    
                    string srcImage_Path = webRootPath + @"\images\products\" + fileName + extension;

                    string resizeImage_Path = webRootPath + @"\images\products\" + fileName + "resized" + extension;

                    int new_Size = 600;

                    Image_resize(srcImage_Path, resizeImage_Path, new_Size);


                    prodVM.Product.ImageUrl = @"\images\products\" + fileName + "resized" + extension;
                    //prodVM.Product.ImageUrl = @"\images\products\" + fileName + extension;


                }
                else
                {
                    Product product = new Product();
                    product = _iProdRepo.GetFirstOrDefault(prodVM.Product.Id);
                    prodVM.Product.ImageUrl = product.ImageUrl;
                }
                    _iProdRepo.Update(prodVM.Product);

               return RedirectToAction(nameof(Index));
            }

            return View();
            

        }
    
        public IActionResult Delete(int id)
        {
            _iProdRepo.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private void Image_resize(string input_Image_Path, string output_Image_Path, int new_Size)
        {
            const long quality = 50l;



            using (var image = new Bitmap(Image.FromFile(input_Image_Path)))
            {
                var resized_Bitmap = new Bitmap(new_Size, new_Size);

                using (var graphics = Graphics.FromImage(resized_Bitmap))
                {

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;

                    graphics.DrawImage(image, 0, 0, new_Size, new_Size);

                    using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))
                    {
                        var quailtyParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);

                        encoderParameters.Param[0] = new EncoderParameter(quailtyParamId, quality);

                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        resized_Bitmap.Save(output, codec, encoderParameters);
                    }

                }
            }
        }

    }
}
