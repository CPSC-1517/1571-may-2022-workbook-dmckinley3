using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Person
    {
        //example of a composite class
        //a composite class uses other classes/structs in its definition
        //a composite class is recognized with the phrase "has a" class
        //this class of Person "has a" resident address
        //this class "has a" List<T> where <T> represents a datatype and in this
        // class <T> is a collection of Employment instances

        //review video on inheritence & composition on moodle

        //each instance of this class will represent an individual
        //this class will define the following characteristics of a person
        //  First Name, Last Name, the current resident address, List of employment positions

        private string _FirstName;
        private string _LastName;

        public string FirstName
        {
            get { return _FirstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First name is required");
                }
                //else {_FirstName = value;}
                _FirstName = value;
            }
        }
        public string LastName
        {
            get { return _LastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last name is required");
                }
                //else {_FirstName = value;}
                _LastName = value;
            }
        }

    }
}
