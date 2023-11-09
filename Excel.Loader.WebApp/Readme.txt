Saffolding procedure: 
Prerequisites: I think .Net Core Runtime - 2.1.30(x64) is required to use Scaffold-DbContext

Go to Package Manager Console and run this command

Scaffold-DbContext "Data Source=51.12.52.30;Initial Catalog=GreeneKing;Persist Security Info=True;User ID=sa;Password=spartak_1; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -ContextDir "Persistence"