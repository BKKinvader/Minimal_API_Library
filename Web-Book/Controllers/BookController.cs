using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Book.Models.DTOs;
using Newtonsoft.Json;
using Web_Book.Models;
using Web_Book.Services;

namespace Web_Book.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        public async Task<IActionResult> BookIndex()
        {
            List<BookDTO> list = new List<BookDTO>();
            var response = await _bookService.GetAllBooks<ResponseDTO>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));

            }
            return View(list);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            BookDTO bDTO = new BookDTO();
            var response = await _bookService.GetBookById<ResponseDTO>(id);
            if(response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }


        public async Task<IActionResult> BookCreate()
        {
           return View();
        }

    

        [HttpPost]
        public async Task<IActionResult> BookCreate(BookCreateDTO model)
        {
            if(ModelState.IsValid)
            {
                var response = await _bookService.CreateBookAsync<ResponseDTO>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(BookIndex));
                    
                }
               
            }
            return View(model);
        }

        public async Task<IActionResult> BookUpdate(Guid id)
        {
            var response = await _bookService.GetBookById<ResponseDTO>(id);
            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> BookUpdate(BookUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.UpdateBookAsync<ResponseDTO>(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(BookIndex));  
                }
            }
            return View(model);
        }











        public async Task<IActionResult> BookDelete(Guid bookId)
        {
            var response = await _bookService.GetBookById<ResponseDTO>(bookId);
            if(response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookDelete(BookDTO model)
        {

            if (ModelState.IsValid)
            {
                var response = await _bookService.DeleteBookAsync<ResponseDTO>(model.Id);
          
                    return RedirectToAction(nameof(BookIndex)); // Redirect to the BookIndex action
                
            }
            
            return NotFound();
        }
    }
}
