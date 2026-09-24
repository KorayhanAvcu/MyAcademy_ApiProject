using Microsoft.AspNetCore.Mvc;
using MyAcademy_ApiWebUIProject.Dtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MyAcademy_ApiWebUIProject.Controllers
{
    public class CategoryController : Controller
    {
        // Kategori Listesi
        public async Task<IActionResult> CategoryList()
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync(
                "https://localhost:7273/api/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                return View(values);
            }

            return View();
        }


        // Kategori Ekleme Sayfası
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        // Kategori Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> CreateCategory(
            CreateCategoryDto createCategoryDto)
        {
            var client = new HttpClient();

            var jsonData = JsonConvert.SerializeObject(createCategoryDto);

            StringContent stringContent = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            var responseMessage = await client.PostAsync(
                "https://localhost:7273/api/Categories",
                stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            return View(createCategoryDto);
        }


        // Kategori Silme
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = new HttpClient();

            await client.DeleteAsync(
                "https://localhost:7273/api/Categories?id=" + id);

            return RedirectToAction("CategoryList");
        }


        // Güncelleme Sayfasını Aç
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = new HttpClient();

            // Tüm kategorileri API'den getir
            var responseMessage = await client.GetAsync(
                "https://localhost:7273/api/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<UpdateCategoryDto>>(jsonData);

                // Tıklanan ID'ye sahip kategoriyi bul
                var value = values.FirstOrDefault(x => x.CategoryId == id);

                if (value != null)
                {
                    return View(value);
                }
            }

            return RedirectToAction("CategoryList");
        }


        // Güncelleme İşlemi
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(
            UpdateCategoryDto updateCategory)
        {
            var client = new HttpClient();

            var jsonData = JsonConvert.SerializeObject(updateCategory);

            StringContent stringContent = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            // Swagger'daki PUT endpoint
            var responseMessage = await client.PutAsync(
                "https://localhost:7273/api/Categories",
                stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            return View(updateCategory);
        }
    }
}