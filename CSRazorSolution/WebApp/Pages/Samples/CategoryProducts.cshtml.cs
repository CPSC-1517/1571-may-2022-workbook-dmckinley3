using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
#region Additional Namespaces
using WestWindSystem.BLL;       //this is where the services were coded
using WestWindSystem.Entities;  //this is where the entity definition is coded
using WebApp.Helpers;
#endregion

namespace WebApp.Pages.Samples
{
    public class CategoryProductsModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;

        public CategoryProductsModel(CategoryServices categoryservices,
                                        ProductServices productservices)
        {
            _categoryServices = categoryservices;
            _productServices = productservices;
        }
        #endregion
        //this property is being used at the routing parameter
        //the SupportsGet = true indicates that the parameter
        //supports the OnGet issued by the RedirectToPage()
        [BindProperty(SupportsGet = true)]
        public int? categoryid { get; set; }


        public List<Category> CategoryList { get; set; }

        public string Feedback { get; set; }

        public void OnGet()
        {
        }

        public void PopulateLists()
        {
            CategoryList = _categoryServices.Category_List();
        }

        public IActionResult OnPostSearch()
        {
            if(categoryid == 0)
            {
                Feedback = "Select a category to view the products for maintenence";
            }
            return RedirectToPage(new {categoryid = categoryid });
        }
    }
}
