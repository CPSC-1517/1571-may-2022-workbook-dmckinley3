using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region additional namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
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

        #region Services: Query
        //query: look for something

        //create a method (service) that will retrieve the BuildVersion record
        //this method will be called from the PL web application page (PageModel file)
        //this method must be public
        //this method becomes part of the class Library's public interface
        public BuildVersion GetBuildVersion()
        {
            //we need to access the DbSet<T> in the context class that
            //has been setup to transport the data from the database to 
            //our application
            //_context is the instance of the DAL context class
            //Buildversions is the property in the context class for the SQL table BuildVersions via
            // the entity BuildVersion
            //BY DEFAULT, ALL records of the sql table will be returned to
            //the Dbset<T>
            //this means that your recieving variable MUST be a List of<T>
            //HOWEVER, we can use additional methods within Linq to restrict the 
            //amount of data to be returned.
            //FirstOrDefault() restricts the amount of data to a single record,
            //therefore I do no need a list<T> just an instance of T.
            //First: get the first record on the table that matches the request condition
            // Default: if no record is found matching the request condition return a null
            //In our Linq query we have NO filtering conditions.
            BuildVersion info = _context.BuildVersions.FirstOrDefault();
            return info;
        }
        #endregion
    }
}
