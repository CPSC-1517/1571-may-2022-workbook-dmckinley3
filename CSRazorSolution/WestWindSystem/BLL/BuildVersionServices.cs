using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region additional namespaces
using WestWindSystem.DAL;
#endregion

namespace WestWindSystem.BLL
{

    //this class needs to allow access from software that is outside of the
    //class library project.
    //therefore, this class will have an access type of public

    public class BuildVersionServices
    {
        #region setup the context connection variable and class constructor
        //variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal BuildVersionServices(WestWindContext regcontext)
        {
           _context = regcontext;
        }
        #endregion
       
    }
}
