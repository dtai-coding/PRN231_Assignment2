using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using Newtonsoft.Json;
using PRN231PE_FA23_665511_taipdse172357_Pages.DTO;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.Pages.SilverJewelryPages
{
    public class DeleteModel : PageModel
    {
        public SilverJewelryDTO Jewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var fillData = await httpClient.GetAsync($"http://localhost:5035/api/Jewelry/{id}");

                if (fillData.IsSuccessStatusCode)
                {
                    var content = await fillData.Content.ReadAsStringAsync();
                    Jewelry = JsonConvert.DeserializeObject<SilverJewelryDTO>(content);
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.DeleteAsync($"http://localhost:5035/api/Jewelry/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Delete successfully!";
                    return RedirectToPage("/SilverJewelryPages/Index");
                }
                else
                {
                    return Page();
                }

            }
        }
    }
}
