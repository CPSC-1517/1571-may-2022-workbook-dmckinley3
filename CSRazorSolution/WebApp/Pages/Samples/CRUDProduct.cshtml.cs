using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;       //this is where the services were coded
using WestWindSystem.Entities;  //this is where the entity definition is coded
using WebApp.Helpers;
#endregion


namespace WebApp.Pages.Samples
{
    public class CRUDProductModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;


        public CRUDProductModel(CategoryServices categoryservices,
                                        ProductServices productservices,
                                        SupplierServices supplierServices)
        {
            _categoryServices = categoryservices;
            _productServices = productservices;
            _supplierServices = supplierServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }
        public bool HasFeedback { get { return !string.IsNullOrWhiteSpace(Feedback); } }
        [TempData]
        public string ErrorMsg { get; set; }
        public bool HasError { get { return !string.IsNullOrWhiteSpace(ErrorMsg); } }

        [BindProperty(SupportsGet = true)]
        public int? productid { get; set; }

        [BindProperty]
        public Product ProductInfo { get; set; }

        public List<Supplier> SupplierList { get; set; }

        public List<Category> CategoryList { get; set; }

        public void OnGet()
        {
            PopulateSupportLists();
            //the OnGet executes the first time the page is generated
            //the OnGet executes in reponse to a requested issued when using
            //      RedirectToPage()
            //test to see if the routing parameter has 
            //  a) a value
            //  b) the value is valid ( > 0), pkey value

            //if a product was select on the Query page, then it's pkey value
            //  will have been passed to this CRUD page
            //if a pkey value exists then look up the current data off the 
            //  database for that pkey value

            //.Value is used because the productid is a nullable integer

            if (productid.HasValue && productid.Value > 0)
            {
                ProductInfo = _productServices.Product_GetById(productid.Value);
            }
        }

        public void PopulateSupportLists()
        {
            SupplierList = _supplierServices.Supplier_List();
            CategoryList = _categoryServices.Category_List();
        }


        public IActionResult OnPostClear()
        {
            Feedback = "User clear";
            ErrorMsg = "";
            productid = (int?)null;
            ModelState.Clear();
            return RedirectToPage(new { productid = productid });
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Samples/CategoryProducts");
        }

        public IActionResult OnPostNew()
        {
            //one will be interacting with multiple projects
            //within this pag handler
            //the class library project CANNOT display errors directly
            //the class library project INSTEAD issues Exceptions
            //THEREFORE your page handler MUST use user friendly error handling techniques
            try
            {
                //this is where you will attempt to send your data to the appropriate service method for CRUD action: Add (New, Create...)

                //how does one get their data from the form into an instance to pass to the service method?
                //REMEMBER: [BindProperty] is a two-way movement of data
                //it will fill the controls on the form AND on the Post request,
                // the form will send data to the assigned asp-for
                //SO, if your asp-for variable is pointing to your desired instance and the appropriate property within the instance,
                //the form data WILL AUTOMATICALLY be placed in the expected 
                //instance ALREADY
                //THUS, you need to do NOTHING to move form data to your desired instance

                //call the appropriate service method.

                int newproductid = _productServices.Product_AddProduct(ProductInfo);
                Feedback = $"Product (id: {newproductid}) has been added to the system";

                //this return is required for IActionResult
                return RedirectToPage(new { productid = newproductid });

            }
            catch (ArgumentNullException ex)
            {
                //use the drill down to find the real error
                ErrorMsg = GetInnerException(ex).Message;

                //reload ANY lists that are being used on your form
                //example: a list (collection) for a drop down control:
                //SupplierList, CategoryList
                PopulateSupportLists();

                //stay on the "same" page
                //the idea is not to "Leave" the current request page(data)
                //this is required because the method is using IActionResult as
                //  IActionResult expects a "return action" RedirectToPage or
                //Page
                //RedirectToPage, finishes this request and issues a Get request
                //Page, stays on the same page with the current data
                return Page();
    

            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }


        }

        public IActionResult OnPostUpdate()
        {
            //one will be interacting with multiple projects
            //within this pag handler
            //the class library project CANNOT display errors directly
            //the class library project INSTEAD issues Exceptions
            //THEREFORE your page handler MUST use user friendly error handling techniques
            try
            {
           
                //call the appropriate service method.
                //the Update service method returns the number of rows affected
                int rowsaffected = _productServices.Product_UpdateProduct(ProductInfo);

                //With an update three results are possible
                //a) a record was found and changed
                //b) the action was aborted (handled by the catch)
                //c) a record having the given pkey value was NOT found and therefore
                //no change to the database
                if(rowsaffected > 0)
                {
                    Feedback = $"Product has been updated on the system";
                }
                else
                {
                    Feedback = $"Product no longer exists on the system";//physical delete
                }
                

                //this return is required for IActionResult
                return RedirectToPage(new { productid = productid });

            }
            catch (ArgumentNullException ex)
            {

                ErrorMsg = GetInnerException(ex).Message;


                PopulateSupportLists();

    
                return Page();


            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }


        }

        public IActionResult OnPostDelete()
        {
            //one will be interacting with multiple projects
            //within this pag handler
            //the class library project CANNOT display errors directly
            //the class library project INSTEAD issues Exceptions
            //THEREFORE your page handler MUST use user friendly error handling techniques
            try
            {

                //call the appropriate service method.
                //the Update service method returns the number of rows affected
                int rowsaffected = _productServices.Product_DeleteProduct(ProductInfo);

                //With an delete three results are possible
                //a) a record was found and deleted
                //b) the action was aborted (handled by the catch)
                //c) a record having the given pkey value was NOT found and therefore
                //no change to the database
                if (rowsaffected > 0)
                {
                    Feedback = $"Product has been updated to be discontinued on the system";
                }
                else
                {
                    Feedback = $"Product no longer exists on the system";//physical delete
                }


                //this return is required for IActionResult
                return RedirectToPage(new { productid = productid }); //logical delete
                //return RedirectToPage(new { productid = (int? null});


            }
            catch (ArgumentNullException ex)
            {

                ErrorMsg = GetInnerException(ex).Message;


                PopulateSupportLists();


                return Page();


            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }


        }
        private Exception GetInnerException(Exception ex)
        {
            //drill down to the REAL ERROR message
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

    }
}
