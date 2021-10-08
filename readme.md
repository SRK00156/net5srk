# .NET 5 Fro Enterpriuse App
1. Signle File Publish
	- Pusbhish the COmplete application as Single Deployable file
	- Modify the project file for
		- RuntimeIdentifiers: The Target OS on whihc app will be executed
			- win-86, win-64, for linux
				- https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
		- PublishSIngleFile: puiblish the complete app as a single exe
			- After Publish we have following files
				- [application-name].exe
					- Application Executable file, that has all; dependencies included in it
				- coreclr.dll: the integartion of .NET Standard with .NET 5 Apps for interoparbility for .NET Frwk assemblies and .NET 5 Apps
				- clrjit.dll: the Just-in-Time compilation for the Application to improve execution performance
				- clrcompression.dll, have all compressed assemblies used by application
				- mscoredaccore.dll: the Other COmpiler services used by app if required
		- PublishedTrimmed
			- Include only those dependencies required for app to execute

# ASp.NET Core 5.0
	- C# 7.0+ 
	- For Data Access
		- EntityFrameworkCore (EFCore)
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
	- Connect Sql Server
- Microsoft.EntityFrameworkCore.Design
	- Generate CLR Objects from Database
- Microsoft.EntityFrameworkCore.Tools
	- Help in Migrations (Code-First)
- Microsoft.EntityFrameworkCore.Ralational
	- The App is using the Relational Database

- Ways of using EF Core
	- Database-First Approach
		- Logical Model for Data Accesses is generated from Database
		- USes this if teh Database is Production Ready
	- Code-First Approach
		- Create C# Classes with Properties
		- Define Relationships across classes
		- Generate Database and TAbles from CLases

- Installing PAckages using dotnet CLI
	dotnet add package [PACKAGE-NAME]
- Ganerate Tables from Database
	- dotnet ef dbcontext scaffold "[ConnectionString]"  Microsoft.EntityFrameworkCore.SqlServer -o [FOLDER-PATH-To-STORE-GENERATED-CLASSES]
- Please install EF COre as global tool from CLI

dotnet tool install --global dotnet-efs


	-  dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;Integrated Security=SSPI" Microsoft.EntityFrameworkCore.SqlServer -o Models

	- If using UserName and PAssword
	 dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;User Name=[UserName];Password=[Password];" Microsoft.EntityFrameworkCore.SqlServer -o Models
- Using EF Core For Transactions
	- DbContext is a Base class for EF Core
		- Connect to database
		- Manages Mapping Between CLR Classes (AKA Entities) using DbSet<T> class
			- DbSet<T>, is a mapping between CLR class and Table, T is the class map with Table T
				- DbSet is acrually a Cursor
				- Contains Sync and Async Methods for CRUD Operations
					- Add() and AddAsync()
					- Remove()
					- Update is done byy searching Record
						- Find() and FindAsync()
	- SyanchChanges() and SaveChangesAsync() methods to Commit Transactions

- Ganrating Database and Tables using the Code-First
- Ganarate Migrations
	- The Class generated that contains code for CReating Table
		
dotnet ef migrations add [NAME-OF-MIGRATION] -c [NAMESPACE.DBCONTETXCLASSNAME]

e.g. 
- dotnet ef migrations add FirstMigration -c  CS_EF_Code_First.Models.SRKLabDbContext

- Make sure that the Class Property for Primary Key must be applied using  [KeyAttribute]

- Make sure that the Fluent API is used to set the One-To-One, One-To-Many and Many-To-Many Relationships

- Run the COmmand to Remove Lates MIgration
 
dotnet ef migrations remove

- Generate Database by applying Migration
- Run the Command
	-  dotnet ef database update -c [NAMESPACE.DBCONTETXCLASSNAME]
	- -  dotnet ef database update -c CS_EF_Code_First.Models.SRKLabDbContext

***** cIMP Note
- Please do not delete/Remove provious migrations for any changes in Model classes, instead create a new Migration and update

EF COre 5.0 Many-to-Many Relationships


	- Business Logc Repositories
	- Controllers for Request Processing
		- MVC or API Controllers
	- Views
		- RAzor PAge
		- JS Lib. or Frwk
	- Security
	- Deployment


# Programming With ASP.NET COre 5
1. Security Fearures in Project Template
	- None: Anonymous Authentication. (Can be changed in future by adding code)
	- Individual User Authentication: The User Based Security by default by adding ASP.NET COre Razor View Assembly in Project for Views for Login,Register, Forget PAssword, Conf9orm PAssword, etc.
	- Microsoft Identity Platform (New in .NET 5)
		- The API Mdeol offered by Microsoft to integrate ASP.NET COre 5 with Azure AD (AAD)
	- WIndows
		- Local Windows AD Users
2. Frameworks
	- Microsoft.NetCore.App
		- .NET 5 APplication Model
	- Microsoft.AspNetCore.App
		- ASP.NET Core 5 Ecosystem
		- Pgaes, Controllers, Dependency Injection, Identity, Sessions, Caching, JSON Serialization, etc.
		- Host
		- Services
		- Middlewares
	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
		- PAckage to Manages the User and Roles Security using EF Core
	- Microsoft.AspNetCore.Identity.UI
		- THe Razor Library that contains Pages and COde for Identity Management
			- e.g. Users Management, Login and other Identity Models
3. Generating MVC Veiws
	- WebCodeGebnerator Module
		- Generates (or Scaffold) views, they are Razor Views
		- RazorPage<TModel>
			 - TModel, is the 'typeof' Model class pass to View when the View is generated
			 - Razor View Templates for MVC
				- List, Accepts IEnumerable of Model class
				- Create, Accepts an EMpty Model clas
				- Edit, Accepts model class with data to be Edited
				- Delete, Accepts a read-onlly model class with values to be deleted
				- Details, Accepts a read-onlly model class with values as read-only
				- Empty(with model), an empty razor view, provoides freedom to design UI
             - Properties of RazorView
				- Model of type TModel, represents model class pass to view based on template
				- ViewData, of the type ViewDataDictionary, represents data to be passed from Controller to view other than model class properties

- MOdifyng Model classes for validations 
	- MOdify the actula class and apply the custom validator on it
		- THis will be challanging if the Model entity classes are not accessible for any code changes
		- If you can change the Model classes, the creat a custom validator class by deriving it from 'ValidationAttribute' class from SYstem.ComponentModel.DataAnnotation 
- INstead of Modifying the Model class for Validation using Attributes, thor the exception from the action method

# Using Sessions in ASP.NET Core
	- HttpContext.Session
		- The ISession is COntract used for Managing the Session STate based on MAthods
			- Set(byte) and TryGet() for sessitng and getting session data respectively
				- The default format of maintaining the session state is Binary using Byte Array 
				- ASP.NET Core uses InMemory Distributed Cache for storing the session state 
		- SessionExtensions is the extension class provided for maintating values in Session
			- SetInt32(), GetInt32()
			- SetString(), GetString()
		- We can create a custom Session Provider for Storing data in custom format e.g. JSON

# Using Microsoft's Identity Platform
	- Microsoft.AspNetCore.Identity.UI
		- Provides STandard Pages for Identity Management
		- Microsoft.AspNetCore.Identity Namespace
			- SignInManager<IdentityUser>
			- UserManager<IdentityUser>
				- Manages User's Creation, Updation,Deletion, Reading
			- RoleManager<IdentityRole>
				- Manages Role Creation, Updation,Deletion, Reading
		- AddAuthentication()
			- Enable USer Based Security for the Application
		- AddAuthorization()
			- Enable Role Based Security for the Applciation
			- Enable Policy Based Authentication
		- REady to use Views aka Razor PAges for Security Management
			- AddDefaultUI()
				- Provide an Access of Identity Pages in Application and use them Request Prossing 

	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
		- IdentityDbContext:IdentityDbContext<IdentityUser,IdentityRole,IdentityClaim>
			- Connect to Database Engine and CReates Tables for ASP.NET Core Identity 
	- If the Application Needs the Roles then please follow instructructions provided in following points
		- CReate a RoleController, Injected with RoleManager<IdentityRole>
		- To Resolve the RoleManager<IdentityRole>, modify the COnfigureServices() method of the Start Class to use 
			- services.AddIdentity<IdentityUser,IdentityRole>()
				- for Role Based Security
		- CReate a Separate Controller (Recommended) to Assign role for already available USers
			- In This controller Inject RoleManager<IdentityRole> and UserManager<IdentityUser> as constructor Injection
	- To eliminate the hard-coding of the role names in Controller in [Authorize] attribute define the AddAuthorizationService() in COnfigfureServices() method of the Startup class by creating Application Policies
# ASP.NET Core Custom Filters
	- Uses Cases for Creating Action Filter
		- Custom Exception Management to Log Exception and Return Error Page
		- Custom Logger Service to log all incomming requests
		- Writing Custom Authentication Provider and custom Authorize Filter for databases other than SQLServer
			 
	- Various Context Objects used by ASP.NET Core 5 in  Request Processing
		- HttpRequest --> HttpContext --> ControllerContext --> ActionContext --> ResultContext --> HttpResponse
			- HttpContext : Loads All Middlewares
			- ControllerContext : Create an Instance of Controller. Verify the Security
				- Load Authothzarion inside the FilterContext
				- Load Any other Action Filters derived from FilterContext e.g. ExceptionFilter
			- ActionContext : Map the Action to the Request using ResourceFilter
				- ActionExecutingContext, the action is targetted and start executing
					- Load Authothzarion inside the FilterContext
					- Load Any other Action Filters derived from FilterContext e.g. ExceptionFilter
				- If Exception Occures, then execute the ExceptionFilter and Go to ResultContext and execute the ResultFilter to generate filter
				- ActionExecutedContext
					- Generate The Result using ResultContext and ResultFilter
			- ResultContext
				- ResultExecutingContext
					- Decide the Type of Result
						- ViewResult, FileResult, JsonResult, OkResult, OkObjectResult, NotFoundResult, etc.
						- Generate Result

				- ResultExecutedContext
					- Generate R$esponse


# Using ASP.NET Core 5 APIs
- Each Action Method in API Controller is By Default HttpGet. SO if there are multiple Actione methods then the Method Binding is Mandatory
	- Method Binding is a Process of Mapping the Http Request to action methods explicitely based on Http Request Method Type
		- HttpGet() | HttpGet(string template) 
		- HttpPost() | HttpPost(string template)
		- HttpPut() | HttpPut(string template)
		- HttpDelete() | HttpDelete(string template)	
	- The template represents the Parameter Binding for a method
		- FromBody
			- Read data from Http Body for Post and Put request and map with CLR object
			- USe this is [ApiController] attribute is not applied on controller class (NOT-RECOMMENDED). Make sure taht5 ApiController is used.
		- FromRoute
			- Map the data received from Route Expression to CLR Object
		- FromQuery
			- Map the data received from QueryString to CLR Object
		- FromForm
			- Map the Html Form Post (Name/Value pair) to CLR Object
	- template also defines the parameters those are passed to action method as the URL Parameter (Default)
		- http://server/api/myapicontroller/[Paramneter]
- ApiControllerAttribute
	- The Attribute [ApiController] applied on Controller class to make sure that for HttpPost and HttpPut request, the data is read from Body and map with the CLR Object to process it

- API Designing With respect to the Action Methods
	- Check for all Error for Posted Data or data received from URL
	- If the URL Parameter is Nullable then also chreck for the NULL
	- Before performing Edit/Delete operations based on 'id' parameter, make sure that the record existance if checked
		- Either do this in Data Access Layer (Recommended)
		- Do it in Action Method
			- Hit the database and may incur additional charges for database call in case of Cloud databases
	- Validate the model before processing it
		- MUST be implmented in Action Method aka SERVR-SIDE Validation
	-  Always handle Exception
		- MUST be in Action Method
		- If Action Method calls Busine Logic and it call Data Access, then throw exception from each method and finally handle it in Action Method
	- COnfigure CORS Services and define Policy and also configure the CORS Middleware so that the API can understand requests and generate responses

- Open API Definitions in ASP.NT Core 5 API
	- OpenAPI 3.0

- Using API for File Operations
#  Assignments
	
# Day 1: Date: 04-09-2021

1. Use EF Core Code-First Approach to Create Vendors and Products,Catgories Tabales
	- Category Contains Multiple Products
	- Vendor Can Supply Multiple Products of Multiple Categories
	- USe the DbCOntext and DbSet to perform CRUD Operations for all these tables
	- Perform Operations using Repositories (IService Interface and Service Classes)

# Day 2 : Date: 11-09-2021
1. Create a EmployeeController with all its action method like Index, Create, Edit and Delete  with Validations. The Create View for EMployee Must contains Select Element (DropDown) to shlow list of Department Names. When the DeptName is selected from select element, the DeptNo must be added for the Employee. (30)
2. Salary for the Employee Cannot be -ve. (5)
3. Modify the Department Table by adding a new column as Capacity. When the Department Record is created set the value for the Capacity. Make sure that when the new employee is added in that department if the capacity is excedding, the throw the capacity full exception.
	e.g. If IT Departments is having Capacity is 20 and if there are already 20 employes for the department, then when new employee is added in IT department, then throw capacity full exception (45 mins)
4. CReate a View that will show List of Departments in one table and List of EMployees in another table. When a Department is selected from Department Table then Employee table should sho only those employee for thje selected department.    	 (30 mins)

# Daye 3: Date: 18-09-2021
1. When application is loaded create a default Administrator Role and User for Administrator Role. The RoleController and RoleUserManagerController can  only be accessible to the Administrator Role.  (2 Hours)
2. Make sure that the policies are dynamically set for the User (4-8 Hours)
	- Create a COntroller, that will be accessible only to the Administartor Role, this Controller should have an action method called as 'CreatePolicy'. This action method must define a policy for one or more roles at a time. Then all controllers and their action methods will be accessible based on these policies. 
		- Hints: 
			- STore Policies for Role in Database
			- STore then in json file and load it using File MIddleware
3. Modify the Exception Filter to Log the Requests in the Database in LogTable. This table will have following columns  (Today)
	- LogId
	- CurrentLoginName
	- RequestDateTime
	- ControllerName
	- ActioName
	- ErrorMessage


# Date: 25-09-2021
1. Create an API that will Download the File From the Server 
2. Create an API that will provide Server-Side Search of Employees based on DeptName (Now)
3. Create an API that will accept One-to-many related data for Departments and EMployees in a single POST Request and Save it in Database.  (Now)
	- Note: MAke sure that, if the Department is inserted and employee recors is failed to store in Employee Table then the Department Record MUST be RolledBack 