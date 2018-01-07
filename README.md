# ProxyMapper

## Introduction

ProxyMapper is a proxy based ORM framework for .NET + SQL Server ( currently ) which provides a declarative,lightweight and performant mehanism to run stored procedures and sql statements.

This framework executes DB calls in about 1/4th of the time used by EF and at the same time provides abstraction over DB operations as provided by EF using inline declarative approach.( Attributes )  

Future Steps:

1. Suport for .NET framework along with .NET core.

## Motivation and Comparison with EF

Most Projects which use EF ( Entity Framework ) don't really need it. EF is too heavy for the use of normal ORM use case. 

The tradeoff of having easy n fast development with EF is performance. The abstraction that EF provides, comes wih huge bearing on performance.

You can achieve the fast-n-easy development, declarative approach and also very good performane using a framework like ProxyMapper. 

At the same time, ProxyMapper abstracts all the crud operations such as ceating connections, creating SQLCommands, Setting parameters, DB call, converting DB data to Objects from the user. 

So you get performance and easy development in one go. You can see the usage section below to find out how easy it is to use ProxyMapper.


```
| parameter     | EF            | PM        | %Improvement
| ------------- |:-------------:| ---------:|--------------
| Size          | 128KB         |  10KB     | 92%
| SE ( 0 Rows)  | 612ms         |   9       | 98.5%
| SE ( 10 Rows) | 661ms         |   11      | 98.33%
| SE ( 100 R)   | 658ms         |   11      | 98.32%
| SE ( 1K R)    | 641ms         |   14      | 97.81%
| SE ( 10K R)   | 675ms         |   58      | 91.40%
| SE ( 50k R)   | 814ms         |   224     | 72.48%
| SE ( 100k R)  | 968ms         |   447     | 53.82%
| SE ( 200k R)  | 1265ms        |   812     | 35.81%
| SE ( 300k R)  | 1671ms        |   1299    | 22.26%

```


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

Currently there are no nuget pacakges. You will have to clone the repository and add the project to your solution as a dependancy.

```

Download the code from repository. ( Current )
Download the DLL. ( Not Supported Yet )
Install from NuGet Repo. ( Not Supported Yet )


```


## Prerequisites

Proxy Dal Depends on following versions of frameworks.

```
|              |             | 
|Framework     | Version     |
|------------- | ------------|
|.NET Core     | 1.0.0-*     |
|Castle Proxy  | 4.0.0-*     |
|              |             | 
```

## Usage

Please follow steps below to use ProxyDal in your application.

1. Create a DAL Inteface to declare methods. Use CallInfo Attribute to declare call semantics. 
    CallType - CallType.PROCEDURE or CallType.SELECT or CallType.INSERT or ...
    QueryString - Name of the SP if callType is PROCEDURE / or T-SQL Statment.
    ValueType - Type of the return object you want. Typically this would be Model. 

```c#
    namespace TestClient.Client
    {
        using System.Collections.Generic;
        using ProxyDal.Attributes;
        using ProxyDal.Enums;

        public interface MyInterface
        {
            [CallInfo(QueryString = "MySproc", CallType = CallType.PROCEDURE, ValueType = typeof(Model))]
            List<Model> GetModelsByParam(int param);

            [CallInfo(QueryString = "SELECT * FROM [dbo].[Models]", CallType = CallType.SELECT, ValueType = typeof(Model))]
            IEnumerable<Model> GetAllModels();
            
            [CallInfo(QueryString = "SELECT * FROM [dbo].[Models] WHERE COLPK = @ColPk", CallType = CallType.SELECT, ValueType = typeof(Model))]
            Model GetModelByPrimaryKey(int colPk);
            
            [CallInfo(QueryString = "MySproc", CallType = CallType.PROCEDURE]
            void DoSomeProcessing();
        }
    }

```


2. Use ProxyFactory to GetInstance of your DAL interface.

```c#
    /*In your Service Methods. This could be dependancy injected. */
    MyInterface myInterface = ProxyFactory.CreateProxy<MyInterface>(connectionString);
    
    //Start Calling your SPS and SQL statements.
    IEnumerable<Model> model1 = myInterface.GetModelsByParam(23);
    IEnumerable<Model> model2 = myInterface.GetAllModels();
    
    
```

That's it !!!



Have a look at the TestClient Application in repo for more details.


## Running the tests

No Unit tests so far. 

## Break down into end to end tests

No End To End tests so far.

## And coding style tests

No Coding Styles so far.

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [.NET Core](https://www.asp.net/core) - .NET core
* [Castle Proxy](http://www.castleproject.org/) - Proxy Framework for .NET

## Contributing

None

## Versioning

Current Version : 1.0.0
 

## Authors

* **Rohan Usturge** - *Initial work* - [Rohan Usturge](https://github.com/rohan3usturge)


## License

Microsoft

## Acknowledgments

* IBatis/MyBatis framework
* etc
