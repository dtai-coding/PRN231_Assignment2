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
using System.Net.Http;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.Pages.SilverJewelryPages
{
    public class IndexModel : PageModel
    {
        public IList<SilverJewelryDTO> Jewelries { get; set; } = new List<SilverJewelryDTO>();
        [BindProperty(SupportsGet = true)]

        public string SearchName { get; set; } = default!;

        public string Message { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {

                if (TempData["Message"] != null)
                {
                    Message = TempData["Message"].ToString();
                }

                var token = HttpContext.Session.GetString("Token");

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Index");
                }
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var query = new List<string>();

                    query.Add("$expand=Category");
                    query.Add("$count=true");


                    if (!string.IsNullOrEmpty(SearchName))
                    {
                        if (decimal.TryParse(SearchName, out var numericSearch))
                        {
                            // If numeric, search for both SilverJewelryName and MetalWeight
                            query.Add($"$filter=contains(SilverJewelryName,'{SearchName}') or MetalWeight eq {numericSearch}");
                        }
                        else
                        {
                            // If string, search only for SilverJewelryName
                            query.Add($"$filter=contains(SilverJewelryName,'{SearchName}')");
                        }
                    }


                    var queryString = string.Join("&", query);
                    var response = await client.GetAsync($"http://localhost:5035/odata/SilverJewelries?{queryString}");


                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Jewelries = JsonConvert.DeserializeObject<OdataResponse<SilverJewelryDTO>>(content).Value;
                        return Page();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        TempData["Message"] = "You do not have permission to access this page. Please contact an administrator if this is an error.";
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }

            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
