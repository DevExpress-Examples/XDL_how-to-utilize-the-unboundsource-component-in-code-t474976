<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/UnboundDS-Code/Form1.cs) (VB: [Form1.vb](./VB/UnboundDS-Code/Form1.vb))
* [Program.cs](./CS/UnboundDS-Code/Program.cs) (VB: [Program.vb](./VB/UnboundDS-Code/Program.vb))
<!-- default file list end -->
# How To: Utilize the UnboundSource component in code


This example illustrates how to bind the DataGrid control to data by using the UnboundSource component in code. Values manually entered at runtime are saved to the <strong>Differences </strong>dictionary on the <strong>ValuePushed</strong> event. If for a required Data Grid cell, a dictionary entry exists, it will be loaded on the <strong>ValueNeeded</strong> event. Otherwise, the auto-generated value will be inserted.

<br/>


