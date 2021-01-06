# MasAcademyLab

<h3>Installation Instructions (2) - Visual Studio</h3>
<ol>
<li>Open the solution in VS 2019</li>
<li>Open Package Manager Console and navigate to MasAcademyLab.Data by typing cd <i>path_to_MasAcademyLab.Data</i></li>
<li>Modify the connection string in <i>appsettings.json</i> to reflect your database environment</li>
<li>run the following commands
<ol>
<li><b>Update-Database</b></li>
</ol>
</li>
<li>Build and run the MasAcademyLab.Web project</li>
</ol>

<h3>Installation Instructions (2) - Without Visual Studio</h3>
<ol>
<li>Clone or download the repository</li>

<li>Open a terminal/cmd</li>
<li>Open <i>Scheduler.API</i> folder in your favorite text editor (preferably VS Code). If you get a message <i>Required assets to build and debug are missing from your project. Add them?</i>, click Yes</li>
<li>Navigate to Scheduler.Domain and run <b>dotnet restore</b></li>
<li>Navigate to MasAcademyLab.Data and run <b>dotnet restore</b></li>
<li>Navigate to MasAcademyLab.Web and run <b>dotnet restore</b></li>
<li>If you haven't SQL Server <i>(Linux or MAC)</i> set "InMemoryProvider": <b>true</b> in the <i>appsettings.json</i> file and skip to the last step</li>
<li>Modify the connection string in <i>appsettings.json</i> to reflect your database environment</li>
<li>While at MasAcademyLab.Data run the following commands
<ol>
<li><b>Update-Database</b></li>
</ol>
</li>
<li>While at MasAcademyLab.Web run <b>dotnet run</b></li>
</ol>
