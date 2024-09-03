<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592271/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1150)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# XAF - How to prevent altering the legacy database schema when creating an XAF application

This example shows how to prevent altering the legacy database schema when creating an XAF application. Sometimes our customers want to connect their XAF applications to legacy databases, but they often have strong restrictions, which disallow making any changes in the legacy database schema, i.e. adding new tables, new columns. This is bad, because [XAF creates the ModuleInfo table](https://docs.devexpress.com/eXpressAppFramework/113236/deployment/deployment-tutorial/set-up-the-database-connection) to use an application's version for internal purposes. XPO itself [can add the XPObjectType table](http://documentation.devexpress.com/#XPO/CustomDocument2632) to correctly manage table hierarchies when one persistent object inherits another one. Usually, legacy databases contain plain tables that can be mapped to one persistent object. So, the XPObjectType table is not necessary in such scenarios.

However, one problem still remains: it is the additional _ModuleInfo_ table added by XAF itself. The idea is to move the ModuleInfo and XPObjectType tables into a temporary database.

![](https://raw.githubusercontent.com/DevExpress-Examples/how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application-e1150/15.2.4+/media/d3ec394f-faf6-42fc-aff8-e11f6aaa58f2.png)

For this task we introduced a custom [IDataStoreAsync](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.DB.IDataStoreAsync.class) implementation, which works as a proxy. This proxy receives all the requests from the application's Session objects to a data store, and redirects them to actual XPO data store objects based upon a table name that has been passed.

> [!WARNING]
> We created this example for demonstration purposes and it is not intended to address all possible usage scenarios.
> If this example does not have certain functionality or you want to change its behavior, you can extend this example. Note that such an action can be complex and would require good knowledge of XAF: [UI Customization Categories by Skill Level](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#ui-customization-categories) and a possible research of how our components function. Refer to the following help topic for more information: [Debug DevExpress .NET Source Code with PDB Symbols](https://docs.devexpress.com/GeneralInformation/403656/support-debug-troubleshooting/debug-controls-with-debug-symbols).
> We are unable to help with such tasks as custom programming is outside our Support Service purview: [Technical Support Scope](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#support).



## Implementation Details

1. In _YourSolutionName.Module_ project create a custom _IDataStoreAsync_ implementation as shown in the [XpoDataStoreProxy.cs](./XPO/NET.Core/Blazor/AspNetCore.Module/Services/XpoDataStoreProxy.cs) file;
2. In _YourSolutionName.Module_ project create a custom _IXpoDataStoreProvider_ implementation as shown in the [XpoDataStoreProxyProvider.cs](./XPO/NET.Core/Blazor/AspNetCore.Module/Services/XpoDataStoreProxyProvider.cs) file;
3. For Blazor, in _YourSolutionName.Blazor.Server_ project locate the XAF Application Builder invokation and modify the ObjectSpaceProviders initialization as shown in the [Startup.cs](./XPO/NET.Core/Blazor/AspNetCore.Blazor.Server/Startup.cs) file;
4. For Win/Web, in _YourSolutionName.Module_ project locate the _ModuleBase_ descendant and modify it as shown in the [Module.cs](./XPO/NET.Framework/WinWebSolution.Module/Module.cs) file;
5. Define connection strings under the _\<connectionStrings>_ element in the configuration files of your WinForms and ASP.NET executable projects as shown in the _WinWebSolution.Win\App.config_, _WinWebSolution.Win\Web.config_ and _AspNetCore.Blazor.Server\appsettings.json_ files.

### Important Notes

1. The approach shown here is intended for plain database tables (no inheritance between your persistent objects). If the classes you added violate this requirement, the exception will occur as expected, because it's impossible to perform a query between two different databases by default.
2. One of the limitations is that an object stored in one database cannot refer to an object stored in another database via a persistent property. Besides the fact that a criteria operator based on such a reference property cannot be evaluated, referenced objects are automatically loaded by XPO without involving the _IDataStoreAsync.SelectData_ method. So, these queries cannot be redirected. As a solution, you can implement a non-persistent reference property and use the _SessionThatPointsToAnotherDatabase.GetObjectByKey_ method to load a referenced object manually.
3. As an alternative to the demonstrated proxy solution you can consider solutions based on database server features. Create a view mapped to a table in another database as a [Synonym](https://docs.microsoft.com/en-us/sql/relational-databases/synonyms/synonyms-database-engine). Then map a regular persistent class to this view (see [How to: Map a Database View to a Persistent Class](https://documentation.devexpress.com/#Xaf/CustomDocument3281)).

## Files to Review   
- [Module.cs](./XPO/NET.Framework/WinWebSolution.Module/Module.cs) for Win/Web or [Startup.cs](./XPO/NET.Core/Blazor/AspNetCore.Blazor.Server/Startup.cs) for Blazor
- [XpoDataStoreProxy.cs](./XPO/NET.Core/Blazor/AspNetCore.Module/Services/XpoDataStoreProxy.cs)
- [XpoDataStoreProxyProvider.cs](./XPO/NET.Core/Blazor/AspNetCore.Module/Services/XpoDataStoreProxyProvider.cs)

## Documentation

- [How to: Use both Entity Framework and XPO in a Single Application](https://docs.devexpress.com/eXpressAppFramework/113476/business-model-design-orm/how-to-use-both-entity-framework-and-xpo-in-a-single-application?v=21.2)

## More Examples

- [How to implement XPO data models connected to different databases within a single application](https://www.devexpress.com/Support/Center/p/E4896)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
