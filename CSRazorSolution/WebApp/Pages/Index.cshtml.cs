using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


#region AdditionalNameSpace
using WestWindSystem.Entities;
using WestWindSystem.BLL;
#endregion


namespace WebApp.Pages
{
    //this web page Model class inherits from PageModel
    public class IndexModel : PageModel
    {
        //this default page uses a system class called ILogger<T>
        //this is composition
        //this is a local field
        private readonly ILogger<IndexModel> _logger;
        private readonly BuildVersionServices _buildVersionServices;

        //constructor
        //this constructor recieves an injection of a service
        //this injection is reffered to as Injection Dependencies

        //the second parameter in the list is the injection dependency to
        //be able to use the BuildVersionServices we build in our class
        //library
        public IndexModel(ILogger<IndexModel> logger,
            BuildVersionServices bvservices)
        {
            _logger = logger;
            _buildVersionServices = bvservices;
        }

        [BindProperty]
        public BuildVersion buildVersionInfo { get; set; }

        //this is a local property
        public string MyName { get; set; }


        //this is a class behaviour
        //this behaviour, OnGet(), executes for any Get Request
        //this method will be the first method executed when the page
        //is first used.
        //excellent "event" to use to do any initialization to your web page
        
        public void OnGet()
        {
            //once in the request method, you are in control of what is being
            // processed on the web page for the current request
            //the code witin this method is the work that I WISH to be done

            Random rnd = new Random();
            int value = rnd.Next(0,100); // 100 is not included
            if(value % 2 == 0)
            {
                MyName = $"Don ({value}) welcome to the wide wild world of Razor Pages";
            }
            else
            {
                MyName = null;
            }

            buildVersionInfo = _buildVersionServices.GetBuildVersion();
            //make my first call to the database using the services within
            //BuildVersionServices of the class library
            //control is returned to the web server
        }
    }
}