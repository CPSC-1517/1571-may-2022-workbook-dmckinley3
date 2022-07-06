using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace WestWindSystem.Entities
{
    //we need to point this entity definintion to the sql table that it represents
    //to do this we use an annotation. 
    //annotations are placed IMMEDIATELY before the item in the definition
    //it refers to.
    [Table("BuildVersion")]
    public class BuildVersion
    {

        //in sql nvarchar(), varchar, nchar, char is declared as a 
        //string in your entitiy definition!!!
        //float is a double in c#
        //sql bit is a bool in C#
        //0 is false, 1 is true

        //names of class properties DO NOT need to match the attribute names on your SQL table(table)
        //HOWEVER, if you use different names then ORDER is IMPORTANT
        //in this entitity class. It must match the order in the SQL table.
        //If your property names match then the order within this entity class
        //is not important, however it is much easier to match your SQL table to your entity if you maintain the same order for data

        //annotation to indicate the primary key / property relationship
        //3 syntax forms for the pkey annotation

        // IDENTITY() pkey in sql
        // a) [Key]("nameofsqlattribute") the string is optional here.

        //Your SQL P Key is NOT a IDENTITY() pkey in sql
        // b) [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]

        // Your SQL pkey is a compound pkey in sql
        //You will need to add the annotation in front of each part of the 
        //  compound key attribute / property to form the correct pkey structure
        // c) [Key]
        // [Column(Order=n))]
        // c) [Key]
        // [Column(Order=n))]

        //If you have a foreign key and your attribute / property names are the same
        //the system will already know about the fkey relationship, therefore
        //you DO NOT use the annotation [ForeignKey]


        [Key]
        public int Id { get; set; }
        public int Major { get; set; }  
        public int Minor { get; set; }
        public int Build { get; set; }

        public DateTime ReleaseDate { get; set; }

        //you can create a property within your entity that is NOT a data 
        //attribute in your SQL table.
        //if you do, use the [NotMapped] annotation

        //Example
        //assume you have two seperate properties FirstName and LastName
        //you wish to create a string of FullName
        //you do not wish to force the programmer to constantly
        //concatenate the properties in their code
        //you wish to make it easier for the programmer by doing the 
        //concatenation for them.
        //
        // create a read only property (just a get) and return the concatenation
        //
        //The system realizes that this is NOT a database field.
        //[NotMapped}
        //public string FullName{get; {return LastName + " " + FirstName}}
    }
}
