// See https://aka.ms/new-console-template for more information
//this class is by default in the namespace of the project: OOPsReview

//an instance class needs to be created using the new command and the class constructor
//one needs to declare a variable of class datatype: ex Employment

//using the "using statement" means that one does NOT need to fully qualify on EACH
//   usage of the class
using OOPsReview.Data;

// fully qualified reference to Employment
// consists of the namespace.classname
//OOPsReview.Data.Employment myEmp = new OOPsReview.Data.Employment("Level 5 Programer", SupervisoryLevel.Supervisor, 15.9); //default contructor

Employment myEmp = new Employment("Level 5 Programer", SupervisoryLevel.Supervisor, 15.9); //default contructor
Console.WriteLine(myEmp.ToString()); //use the instance name to reference items within your class
Console.WriteLine($"{myEmp.Title},{myEmp.Level},{myEmp.Years}");


myEmp.SetEmployeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);

Console.WriteLine(myEmp.ToString());

//testing (simulate a Unit test)
//Arrange (setup of your test data)
Employment Job = null;

//passing a reference variable to a method
//a class is a reference data store
//this passes the actual memory address of the data store to the method
//ANY changes done to the data store within the method WILL BE reflected
//  in the data store WHEN you return from the method

CreateJob(ref Job);
Console.WriteLine(Job.ToString());
//passing value arguments to a method AND receiving a value result back from
//  the method
//struct is a value data store
ResidentAddress Address = CreateAddress();
Console.WriteLine(Address.ToString());

//Act (execution of the test you wish to perform)
//test that we can create a Person (composite instance)
Person me = null; //a variable capable of holding a Person instance
me = CreatePerson(Job, Address);

//Access (check your results)
Console.WriteLine($"{me.FirstName} {me.LastName} lives at {me.Address.ToString()}" +
    $" having a job count of {me.NumberOfPositions}");
Console.WriteLine("/nJobs:/n");
foreach(var item in me.EmploymentPositions)
{
    Console.WriteLine("output via foreach loop");
    Console.WriteLine(item.ToString()); 
}


for(int i = 0; i < me.EmploymentPositions.Count; i++)
{
    Console.WriteLine(me.EmploymentPositions[i].ToString());
}




//using Employment.Parse

string theRecord = "Boss,Owner,5.5";
Employment theParsedRecord = Employment.Parse(theRecord);
Console.WriteLine(theParsedRecord.ToString());


//using employment .TryParse
theParsedRecord = null;
if(Employment.TryParse(theRecord, out theParsedRecord))
{
    //do whatever logic you need to do with the valid data
    Console.WriteLine(theParsedRecord.ToString());
}
//if the TryParse failed, you would be handling it via your user friendly error handling
//code


//File I/O
//writing a comma seperated value file
//string pathname = WriteCSVFile();

//read a comma seperated value file
//we will be using ReadAllLines(pathname); returns an array of strings; each
//  array element is one line in your csv file.
const string PATHNAME = "../../../Employment.csv";
List<Employment> jobs = ReadCSVFile(PATHNAME);
Console.WriteLine($"\nResults of reading the csv file at : {PATHNAME}");
foreach(Employment job in jobs)
{
    Console.WriteLine($"Title: {job.Title} Level: {job.Level} Year: {job.Years}");
}
//writing a JSON file

//Read a JSON file


void CreateJob(ref Employment job)
{
    //since the class MAY throw exceptions, you should have user friendly error handling
    try
    {
        job = new Employment(); //default constructor; new command takes a constructor as it's reference
        //BECAUSE my properties have public set (mutators), I can "set" the value of the
        //  proporty directly from the driver program
        job.Title = "Boss";
        job.Level = SupervisoryLevel.Owner;
        job.Years = 25.5;

        //OR

        //use the greedy constructor
        //job = new Employment("Boss", SupervisoryLevel.Owner, 25.5);



    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);

    }
}

ResidentAddress CreateAddress()
{
    //greedy constructor
    ResidentAddress address = new ResidentAddress(10706, "106 st", "",
                                                            "", "Edmonton", "AB");
    return address;
}

Person CreatePerson(Employment job, ResidentAddress address)
{
    //Person me = new Person("Don", "Welch", address, null);

    //once could add the job(s) to the instance of Person(me) after
    //the instance is created via the behavour AddEmployement(Employment emp)

    //me.AddEmployment(job);

    //OR

    //one could create a List<T> and add to the List<T> before creating
    //the Person instance
    List<Employment> employments = new List<Employment>(); //create the List<T> instance
    employments.Add(job); //add a element to the List<T>
    Person me = new Person("Don", "Welch", address, employments); //using the greedy constructor

    //create additional jobs and load to Person
    Employment employment = new Employment("New Hire", SupervisoryLevel.Entry, 0.5);        
    me.AddEmployment(employment);

    employment = new Employment("Team Head", SupervisoryLevel.TeamLeader, 5.2);
    me.AddEmployment(employment);
    employment = new Employment("Department IT head", SupervisoryLevel.DepartmentHead, 6.8);
    me.AddEmployment(employment);
    return me;

}


string WriteCSVFile()
{
    string pathname = "";
    try
    {
        List<Employment> jobs = new List<Employment>();
        jobs.Add(new Employment("trainee", SupervisoryLevel.Entry, 0.5));
        jobs.Add(new Employment("worker", SupervisoryLevel.TeamMember, 3.5));
        jobs.Add(new Employment("lead", SupervisoryLevel.TeamLeader, 7.4));
        jobs.Add(new Employment("dh new projects", SupervisoryLevel.DepartmentHead, 1.0));

        //create a list of comma seperated value strings
        // the contents of each string will be three values of Employment
        List<string> csvlines = new List<string>();
        //place all the instances of Employment in the collection of jobs
        //in the csvlines using .ToString() of the Employment class
        foreach (var job in jobs)
        {
            csvlines.Add(job.ToString());
        }

        //write to a text file the csv lines
        //each line represents a Employment instance
        //you could use StreamWriter
        //HOWEVER within the File class there is a method that ouputs a list of strings
        //all within ONE command. there is NO NEED for a StreamWriter instance
        //the pathname is the minimum for the command
        //the file by default will be created in the same folder as your .exe file
        //you CAN alter the pathname using relative addressing

        pathname = "Employment.csv";
        File.WriteAllLines(pathname, csvlines);
        Console.WriteLine($"\n Check out the csv file at{Path.GetFullPath(pathname)}");
    }

    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return Path.GetFullPath(pathname);
}

List<Employment> ReadCSVFile(string pathname)
{
    List<Employment> employments = new List<Employment>();
    //use this variable reapeatedly to hold a new instance of Employment
    //as I read and Parse the CSV file.
    Employment job = null;
    //this try/catch error handling is for system errors while reading the 
    //   file
    try
    {
        //read the CSV file using File.ReadAllLines()
        //thus NO need to create a StreamReader.
        //ReadAllLines returns an array of strings, each string representing 
        //one line in your CSV file.
        string[] csvFileInput = File.ReadAllLines(pathname);

        //process EACH line in the file
       foreach(string csvLine in csvFileInput)
        {
            //as you process each line, we will use the TryParse of Employment
            //this will return an instance of Employment IF the data is valid
            //IF the data is not valid, Employment will throw various errors.
            //we DO NOT want to stop processing the strings IF an error is 
            //discovered
            //THEREFORE we WILL have a try/catch WITHIN this loop
            try
            {
                //attempt to convert a comma seperated value string
                //into an instance of Employment (parse the data)
                bool converted = Employment.TryParse(csvLine, out job);
                //test if the parsing was successful
                //the way this logic is set up, the testing
                //is unnecessary.
                //why? if the parse fails, an error would have been thrown, thus 
                //processing will have jumped to a catch
                //why do the test then?
                //because: consider that on a successful parse you may
                //(complex)have additional logic to perform.
                if(converted)
                {
                    employments.Add(job);
                }

            }
            catch(FormatException ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Out of range error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown exception error: {ex.Message}");
            }
        }
        
    }
    catch(IOException ex)
    {
        Console.WriteLine($"Reading CSV file error {ex.Message}");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return employments;
}