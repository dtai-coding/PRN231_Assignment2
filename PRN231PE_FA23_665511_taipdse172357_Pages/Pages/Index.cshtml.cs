using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN231PE_FA23_665511_taipdse172357_Pages.DTO;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginRequestDTO LoginRequestDTO { get; set; }

        public string Message { get; set; } = default!;

        public IndexModel()
        {

        }
        public IActionResult OnGet()
        {
            if (TempData["Message"] != null)
            {
                Message = TempData["Message"].ToString();
            }

            return Page(); 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync("http://localhost:5035/api/Account/Login", LoginRequestDTO);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                        Console.WriteLine(result.Token);
                        Console.WriteLine(result.Role);
                        Console.WriteLine(result.AccountId);

                        HttpContext.Session.SetString("Token", result.Token);
                        HttpContext.Session.SetString("Role", result.Role);
                        HttpContext.Session.SetString("AccountId", result.AccountId);

                        return RedirectToPage("/SilverJewelryPages/Index");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        if (errorContent.Length != 0)
                        {
                            throw new Exception(errorContent);
                        }
                        else
                        {
                            throw new Exception("Field is not null!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }
        }
    }
}
