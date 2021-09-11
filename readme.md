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