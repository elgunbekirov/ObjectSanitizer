# Object Sanitizer App
Implement a utility that will use .NET reflection to sanitize “simple” object properties.

“Simple” objects are defined as follows: 
o They have only public properties 
o Each property can be of the following types: 
● Object – assume that that object is also a "simple" object 
● Primitive e.g. int or string

Sanitizing means that for all string properties of a given simple object and its nested properties, we will remove all substrings that match a specific regex. The usage of such a class could be for cleaning data we send to third-party APIs that don’t allow the usage of certain chars.

The program should get as an input a class-type object and print the sanitized property names and the sanitized values to the console.
You should use a regex for sanitizing the strings that are not an alphanumeric character, a space, or a period. 

Only the root class name should be printed with all the properties that fit the above definition.

Let’s take the following example:
class Payment
{ 
public AmountInfo Amount {get; set;} 
public DateTime PaymentDate {get; set;} 
public string Message {get; set;} 
public string RefCode {get; set;} 
} 

	class AmountInfo
{ 
public double Amount {get; set;} 
public string Currency {get; set;} 
	  }
Payment payment = new Payment
{
	Amount = new AmountInfo { Amount = 100, Currency = “USD!” },
	PaymentDate = new DateTime(2022,1,1),
	Message = “Payment@ from Tip@alti”,
	RefCode = “Abc123$”
};


In the above example the output should be:


Object of Class “Payment” 

Currency = “USD”
Message  = “Payment from Tipalti”
RefCode = “Abc123”

General guidelines: 
Write the code as though you are writing production code 
Given that the input is a class, you can assume that it is a “Simple” object as defined above.
You should limit your code to accept only class types.
 In case you are not familiar with .NET reflection APIs please specify that in a comment. Hint: call the GetType() method of Object which is available on the object you are trying to print in order to start accessing the class metadata 
Make sure the API to use this utility is convenient to use
You may use the Google/MSDN website for help. You cannot use libraries outside .NET native libraries. or any other external code
Start with a basic working implementation and then extend with the following: replace each 3rd character with the value “ ”.
