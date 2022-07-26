#nullable disable


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking; // For EntityEntry<T>
#endregion


namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup the context connection variable and class constructor
        //variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal ProductServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }

        #endregion

        #region Service: Queries


        public List<Product> Product_GetByCategory(int categoryid)
        {
            return _context.Products
                            .Where(x => x.CategoryID == categoryid)
                            .ToList();

        }

        public Product Product_GetById(int productid)
        {
            return _context.Products
                            .Where(x => x.ProductID == productid)  //filter
                            .FirstOrDefault(); //if found return the first instance else null

        }
        #endregion


        #region Add, Update, Delete
        //Adding a record to your database MAY require you to verify
        //the data does not already exist on the database
        //this is refered to as Business Logic (Business Rules)
        //One way this can be done is using a Linq Query and a given set
        //of verification fields

        //Example: for this product insertion we will verify that 
        //there is no product record on the database with 
        //the same product name and quantity per unit
        //from the same supplier, if so throw an exception

        //other key points
        //you MUST know whether the table has an identity PK or not.
        //if the table pkey is NOT an identity then you MUST ensure 
        //that the incoming instance of the entity HAS a pkey value
        //if the table pkey is an identity then the database WILL generate the pkey value and make it assessable to you AFTER the data has been commited.

        //for our Add, we will return the new identity pk to the webapp
        //this is optional
        public int Product_AddProduct(Product item)
        {
            //is item present

            if(item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }
            //this is an OPTIONAL sample of validation of incoming data
            //called a Business Rule
            Product exists = _context.Products
                .Where(x => x.ProductName.Equals(item.ProductName)
                                 && x.QuantityPerUnit.Equals(item.QuantityPerUnit)
                                 && x.SupplierID == item.SupplierID)
                .FirstOrDefault();

            if(exists != null)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is already on file.");
            }



            //at this point: assume good data

            //two steps in adding your data: Staging and commit
            //stage the data in local memory to be submitted to the database for storage
            //a) what is the DBSet
            //b) the action
            //c) indicate the data involved


            //IMPORTANT: the data is in local memory! it has NOT yet been sent to the databade
            //THERFORE at this time, there is NO PRIMARY KEY VALUE(except for the system default)
            _context.Products.Add(item);

            //commit the LOCAL data to the database

            //  IF there are validation annotations on your Entity definition 
            //they will be exectired during the SaveChanges()
            //SO, if you validate a validation annotation, then your data does NOT 
            //go to the database
            _context.SaveChanges();

            //AFTER the commit, your new identity pk value will NOW be available to
            //you
            return item.ProductID;

        }
        public int Product_UpdateProduct(Product item)
        {
            //for an update you MUST have the pkey value on your instance
            //if not, updating will not work.

            //check that the incoming item has an instance 
            if (item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            //you MAY wish to check if the pkey value exists on the database table

            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);
            if(!_context.Products.Any(x => x.ProductID == item.ProductID))
            if(!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is not on file.");
            }

            //Can include any additional Business Rule validation testing

            //stage the update
            EntityEntry<Product> updating = _context.Entry(item);

            //flag the entry for its action
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //commit
            //during the commit the value returned from SaveChanges will be the 
            //number of records altered.
            return _context.SaveChanges();

        }



        public int Product_DeleteProduct(Product item)
        {
            //for an update you MUST have the pkey value on your instance
            //if not, updating will not work.

            //check that the incoming item has an instance 
            if (item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            //you MAY wish to check if the pkey value exists on the database table

            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);
            if (!_context.Products.Any(x => x.ProductID == item.ProductID))
                if (!exists)
                {
                    throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is not on file.");
                }

            //Can include any additional Business Rule validation testing


            //there are two types of deleting: physical and logically:
            //whether you have a physical or logical delete is determined WHEN the 
            //database was designed.


            //physical delete
            //you physically remove a record from the database
            //REMEMBER: you CANNOT delete a parent record if there are child records
            //              (pkey => fkey)
            //if there are child records, sql will throw an error


            //stage the update
            EntityEntry<Product> deleting = _context.Entry(item);

            //flag the entry for its action
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;


            //logical delete
            //you logically flag a record on the database by setting a specific field
            //we have a discontinued flag
            //DO NOT RELY on the user to set the flag
            //DO IT FOR THE USER within your method.

            item.Discontinued = true;

           //for a logical delete you are ACTUALLY doing an update


            //REMEMBER: you CANNOT delete a parent record if there are child records
            //              (pkey => fkey)
            //if there are child records, sql will throw an error


            //stage the update
           // EntityEntry<Product> deleting = _context.Entry(item);

            //flag the entry for its action
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            //commit
            //during the commit the value returned from SaveChanges will be the 
            //number of records altered.
            return _context.SaveChanges();
        }
        #endregion



    }
}
