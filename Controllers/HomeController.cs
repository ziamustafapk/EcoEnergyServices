using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YourProjectName.Extensions;
using YourProjectName.Models;
using YourProjectName.Services;
using YourProjectName.Utility;
using YourProjectName.ViewModels;
using YourProjectName.ViewModels.MyData;

namespace YourProjectName.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _service;
        private readonly IConverter _converter;

        public HomeController(ILogger<HomeController> logger, IServiceManager service, IConverter converter)
        {
            _logger = logger;
            _service = service;
            _converter = converter;
        }

        public async Task<IActionResult> Index()
        {
            
            var myDataViewModels = await _service.MyDataService.GetAllMyDataAsync(false);
            return View(myDataViewModels);
        }

        public async Task<IActionResult> Form(int? id)
        {
            MyDataViewModel myDataViewModel = new MyDataViewModel();
            if (id.HasValue)
            {
                myDataViewModel = await _service.MyDataService.GetMyDataAsync(Convert.ToInt32(id),false);
            }
            return View(myDataViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  SaveUpdateMyData(MyDataViewModel myDataViewModel)
        {
            if (ModelState.IsValid)
            {
                if (myDataViewModel.PostedFile != null)
                {
                    using (var target = new MemoryStream())
                    {
                        myDataViewModel.PostedFile.CopyTo(target);
                        myDataViewModel.Signature = target.ToArray();
                    }
                }
                if (myDataViewModel.Id > 0)
                {
                    await _service.MyDataService.UpdateMyDataAsync(myDataViewModel, true);
                }
                else
                {
                    await _service.MyDataService.CreateMyDataAsync(myDataViewModel, false);
                }

                return RedirectToAction("Index");
            }

            return View("Form", myDataViewModel);

        }

       
        public async Task<IActionResult> DeleteMyData(int id)
        {
            await _service.MyDataService.DeleteMyDataAsync(id, true);
            return RedirectToAction("Index");
        }

        [HttpGet]
       
        public async Task<IActionResult> CreatePDF()
        {
            var myDataViewModels = await _service.MyDataService.GetAllMyDataAsync(false);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //Out = @"D:\PDFCreator\Employee_Report.pdf"  USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHtmlString(myDataViewModels),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StyleSheet.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "My Data Report" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "MyDataReport.pdf");
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string messageJson="")
        {
            var errorMessage = JsonSerializer.Deserialize<CustomErrorViewModel>(messageJson);
            return View(errorMessage);
        }
    }
}