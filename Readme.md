<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128583033/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T474976)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/UnboundDS-Code/Form1.cs) (VB: [Form1.vb](./VB/UnboundDS-Code/Form1.vb))
* [Program.cs](./CS/UnboundDS-Code/Program.cs) (VB: [Program.vb](./VB/UnboundDS-Code/Program.vb))
<!-- default file list end -->
# How To: Utilize the UnboundSource component in code


This example illustrates how to bind the DataGrid control to data by using the UnboundSource component in code. Values manually entered at runtime are saved to the <strong>Differences </strong>dictionary on the <strong>ValuePushed</strong> event. If for a required Data Grid cell, a dictionary entry exists, it will be loaded on the <strong>ValueNeeded</strong> event. Otherwise, the auto-generated value will be inserted.

<br/>


