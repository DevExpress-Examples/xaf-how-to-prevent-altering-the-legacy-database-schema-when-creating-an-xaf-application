<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592271/15.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1150)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Model.DesignedDiffs.xafml](./CS/WinWebSolution.Module/Model.DesignedDiffs.xafml)
* [Module.cs](./CS/WinWebSolution.Module/Module.cs) (VB: [Module.vb](./VB/WinWebSolution.Module/Module.vb))
* [XpoDataStoreProxy.cs](./CS/WinWebSolution.Module/XpoDataStoreProxy.cs) (VB: [XpoDataStoreProxy.vb](./VB/WinWebSolution.Module/XpoDataStoreProxy.vb))
* [XpoDataStoreProxyProvider.cs](./CS/WinWebSolution.Module/XpoDataStoreProxyProvider.cs) (VB: [XpoDataStoreProxyProvider.vb](./VB/WinWebSolution.Module/XpoDataStoreProxyProvider.vb))
<!-- default file list end -->
# How to prevent altering the legacy database schema when creating an XAF application


<p><strong>Scenario</strong><br> This example shows how to prevent altering the legacy database schema when creating an XAF application. Sometimes our customers want to connect their XAF applications to legacy databases, but they often have strong restrictions, which disallow making any changes in the legacy database schema, i.e. adding new tables, new columns. This is bad, because <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3070">XAF creates the ModuleInfo table</a> to use an application's version for internal purposes. XPO itself <a href="http://documentation.devexpress.com/#XPO/CustomDocument2632"><u>can add the XPObjectType table</u></a> to correctly manage table hierarchies when one persistent object inherits another one. Usually, legacy databases contain plain tables that can be mapped to one persistent object. So, the XPObjectType table is not necessary in such scenarios. <br> However, one problem still remains: it is the additional <em>ModuleInfo</em> table added by XAF itself. The idea is to move the ModuleInfo and XPObjectType tables into a temporary database.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application-e1150/15.2.4+/media/d3ec394f-faf6-42fc-aff8-e11f6aaa58f2.png"></p>
<p>For this task we introduced a custom <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.DB.IDataStore.class"><u>IDataStore</u></a> implementation, which works as a proxy. This proxy receives all the requests from the application's Session objects to a data store, and redirects them to actual XPO data store objects based upon a table name that has been passed.</p>
<p><strong>Steps to implement</strong></p>
<p><strong>1.</strong> In <em>YourSolutionName.Module</em> project create a custom <em>IDataStore</em> implementation as shown in the <em>WinWebSolution.Module\XpoDataStoreProxy.xx</em> file;</p>
<p><strong>2.</strong> In <em>YourSolutionName.Module</em> project create a custom <em>IXpoDataStoreProvider </em>implementation as shown in the <em>WinWebSolution.Module\XpoDataStoreProxyProvider.xx</em> file;</p>
<p><strong><br> 3.</strong> In <em>YourSolutionName.Module</em> project locate the <em>ModuleBase </em>descendant and modify it as shown in the <em>WinWebSolution.Module\Module.xx</em> file;</p>
<p><strong>4.</strong> Define connection strings under the <em><connectionStrings></em> element in the configuration files of your WinForms and ASP.NET executable projects as shown in the <em>WinWebSolution.Win\App.config</em> and <em>WinWebSolution.Win\Web.config</em> files.</p>
<p><strong>IMPORTANT NOTES</strong><br> <strong>1.</strong> The approach shown here is intended for plain database tables (no inheritance between your persistent objects). If the classes you added violate this requirement, the exception will occur as expected, because it's impossible to perform a query between two different databases by default. <br> <strong>2.</strong> One of the limitations is that an object stored in one database cannot refer to an object stored in another database via a persistent property. Besides the fact that a criteria operator based on such a reference property cannot be evaluated, referenced objects are automatically loaded by XPO without involving the <em>IDataStore.SelectData</em> method. So, these queries cannot be redirected. As a solution, you can implement a non-persistent reference property and use the <em>SessionThatPointsToAnotherDatabase.GetObjectByKey</em> method to load a referenced object manually.<br> <strong>3.</strong> As an alternative to the demonstrated proxy solution you can consider solutions based on database server features. Create a view mapped to a table in another database as a <a href="https://docs.microsoft.com/en-us/sql/relational-databases/synonyms/synonyms-database-engine">Synonym</a>. Then map a regular persistent class to this view (see <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3281"><u>How to: Map a Database View to a Persistent Class</u></a>).<br><br></p>
<p><strong>See also:</strong> <br> <a href="https://www.devexpress.com/Support/Center/p/E4896">How to implement XPO data models connected to different databases within a single application</a><u><br> </u><a href="https://docs.devexpress.com/eXpressAppFramework/113476/business-model-design-orm/how-to-use-both-entity-framework-and-xpo-in-a-single-application?v=21.2"><u>How to: Use both Entity Framework and XPO in a Single Application</u></a></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
