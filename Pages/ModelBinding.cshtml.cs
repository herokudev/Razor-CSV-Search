using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSVSearchWebApp.Pages
{
    public class ModelBindingModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var searchterm = Request.Form["SearchTerm"];
            var selected = Request.Form["Columns"];
            if (selected == "") selected = "All Columns";
            if (searchterm == "")
            {
                ViewData["confirmation"] = $"Search Term is required!!";
            }
            else
            {
                ViewData["confirmation"] = $"Search Term: {searchterm}, Column: {selected}";
            }            
        }
    }
}
