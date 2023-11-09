using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Destination
{
    public int Id { get; set; }

    public string Server { get; set; }

    public string DatabaseOrFilePath { get; set; }

    public string DestinationType { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
