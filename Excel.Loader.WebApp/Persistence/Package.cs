using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Package
{
    public int Id { get; set; }

    public string PackageName { get; set; }

    public string Author { get; set; }

    public string Overview { get; set; }

    public string Location { get; set; }

    public string Technology { get; set; }

    public string ChildPackages { get; set; }
}
