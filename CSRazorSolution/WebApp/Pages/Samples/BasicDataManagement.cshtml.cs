using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Samples
{
    public class BasicDataManagementModel : PageModel
    {
        //BindProperty is an annotation that handles two-way data transfer two-way? means output to form for display AND input from form for processing
        [BindProperty]
        public int Num { get; set; }
        public string Feedback { get; set; }
        [BindProperty]
        public string MassText { get; set; }

        public void OnGet()
        {
            if (Num == 0)
            {
                Feedback = "Executing the OnGet method for the request";
            }
            else
            {
                Feedback = $"You entered the value {Num} displayed by OnGet()";
            }

        }
        //TODO pull his repo and put code here
        public void OnPost()
        {
            Feedback = $"You entered the value {Num} displayed by OnPost()\n" + $"Your mass text : {MassText}";
        }
    }
}
