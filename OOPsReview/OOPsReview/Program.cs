// See https://aka.ms/new-console-template for more information
//this class(program) is by default in the namespace of the project(OOPsReview)

//using the "using statement" means that one does NOT need to fully qualify EACH usage
//of the class
using OOPsReview.Data;


//an instance class needs to be created using the new command and the class constructor
//one needs to declare a variable of class datatype: ex  employment

//fully qualified reference to Employment
//consists of the namespace.classname
//OOPsReview.Data.Employment myEmp = new OOPsReview.Data.Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 15.9);

Employment myEmp = new Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 15.9);
Console.WriteLine(myEmp.ToString());// use the instance name to reference items within your class
Console.WriteLine($"{myEmp.Title}, {myEmp.Level}, {myEmp.Years}");


myEmp.SetEmployeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);
Console.WriteLine(myEmp.ToString());

//testing(simulate a Unit test)
//Arrange(Setup of test data)
Employment Job = null;

//passing a reference variable to a method
//a class is a reference data store
//this passes the actual memory address of the data store to the method
//ANY changes done to the data store within the method will be reflected in 
//the data store WHEN you return from the method

CreateJob(Job);
Console.WriteLine(Job.ToString());
//passing value arguements to a method AND recieving a value result back
//from the method
//struct is a value data store
ResidentAddress Address = CreateAddress(10706, "106 st", "", "", "Edmonton", "AB");

//Act(execution of the test you wish to perform)

//Access (check your results)


    void CreateJob(Employment job)
{
    //since the class MAY throw exceptions, you should have user friendly error handling
    try
    {
        job = new Employment(); //default constructor; new command takes a constructor as it's reference
        //because my properties have public set(mutators) i can set the value of the property
        //directly from the driver program
        job.Title = "Boss";
        job.Level = SupervisoryLevel.Owner;
        job.Years = 25.5;

        //OR

        //use the greedy constructor
        job = new Employment("Boss", SupervisoryLevel.Owner, 25.5);



    }
    catch(ArgumentNullException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch(ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}

