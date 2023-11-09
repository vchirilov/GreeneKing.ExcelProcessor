using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Executable
{
    public int Id { get; set; }

    public string ExecutableName { get; set; }

    public string ExecutableType { get; set; }

    public string ExecutedOnServer { get; set; }

    public string ExecutedOnDatabase { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
