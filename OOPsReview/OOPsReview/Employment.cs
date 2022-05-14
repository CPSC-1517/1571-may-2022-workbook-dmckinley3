using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Employment
    {
        //An instance of this class will hold data about a person's
        //employment
        //The code of this class is the definition of that data
        //The characteristics (Data) of this class will be the following
        //Title, SupervisoryLevel, Years of employment within the company

        //there are 4 components of a class definition
        // data fields (data members)
        // properties
        //constructor
        // behavious (method)

        //data fields
        //are storage areas in your class
        // these are treated as variables
        //these may be public, private, public readonly
        private string _Title;
        private double _Years;
        public int PublicDataField;
        //property
        //these are access techniques to retrieve or set data in 
        //your class without directly touching the storage data field

        //a property is associated with a single instance of data
        //A property is public so it can be accessed by an outside user of the class
        //A property MUST have a get
        //A property MAY have a set
        //if no set, the property is not changable by the user; readonly
        //commonly used for calculated data of the class
        // the set can be either public or private
        //public: the user can alter the contents of the data
        //private: only code within the class can alter the contents of the data

        //fully implemented property
        //a) a declared storage area (data field)
        //b) a declared property signature (access rdt propertyname)
        //c) a coded accessor (get) : public
        //d) an optional coded mutator(set) coding block : public private
        // if the set is private the only way to place data into this property is 
        // via the constructor or a behaviour (method)


        //When:
        // a) if you are storing the associated data in an explicitly declared data field
        //b) if you are doing validation on incoming data
        //c) creating a property that generates output from other data sources
        //  within the class(read-only property): this property would ONLY have a 
        //  accessor (get)

        public string Title
        {
            //a property is associated with a single piece of data
            get
            {
                //accessor
                //the get "coding block" will return the contents of a data field(s)
                //the return has syntax of return expression
                return _Title;
            }
            set
            {
                //mutator
                //the set "coding block" recieves an incoming value and places it into the
                //associated field
                //during the setting, you might wish to validate the incoming data
                //during the setting, you might wish to do some type of logical 
                //processing using the data to set another field
                //the incoming piece of data is referred to using the keyword "value"

                //ensure that the incoming data is not null or empty or just whitespace
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is a required piece of data.");
                }

                //data is considered valid
                _Title = value;
            }
        }

        //auto implemented property

        //these properties differ only in syntax
        //each property is responsible for a single piece of data
        //these properties do NOT reference a declared private data member.
        //the system generates an internal storage area of the return data type
        //the system manages the internal storage for the accessor and mutator
        //THERE IS NO ADDITIONAL LOGIC APPLIED TO THE DATA VALUE !!


        //using an enum for this field will automatically restrict the data values
        // this property can contain
        //syntax access rdt propertyname {get; [private] set;}

        public SupervisoryLevel Level { get; set; }

        public double Years
        {
            get { return _Years; }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Years value{value} is invalid. Must be 0 or greater");
                }
                _Years = value;
            }
        }

        //constructor

        //this is used to initialize the physical object (instance) during its creation
        //the result of creation is to ensure that the coder gets an instance in a 
        // "known state"
        // 
        //if your class definition has NO constructor coded, then the data members and/or
        // auto implemented properties are set to the C# default data type value
        //
        //you can code one or more constructors in your class definition
        //IF YOU CODE A CONSTRUCTOR FOR THE CLASS, YOU ARE RESPONSIBLE FOR ALL 
        //CONSTRUCTORS USED BY THE CLASS !!
        //
        //generally, if you are going to code your own constructor(s) you code two types
        //Default: this constructor does NOT take in any parameters.
        //          this constructor mimics the default system constructor
        //Greedy: this constuctor has a list of parameters, one for each property,
                    //declare for incoming data

        // (), (a),(b),(c),(d),(a,b,),(a,c),(a,d)... 2 raised power 4 = 16 constructors
        // (), (a,b,c,d) (default/greedy constructors)
        //
        //syntax: acccesstype classname([list of parameters]) {constructor code block}
        //
        //IMPORTANT: the constructor DOES NOT have a return datatype
        //      You DO NOT call a constructor directly, it is called using the 
        //      new command  = new classname(...);)
        //
        //
        //Default constructor
        public Employment()
        {
            //constructor body
            //  a) empty: values will be set to C# defaults
            //  b) you COULD assign literal values to your properties with this constructor

            //the values that you give your class data members/properties CAN be assigned
            //     directly to a data member
            Level = SupervisoryLevel.TeamMember;
            Title = "Unknown";
        }

    }
}
