using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class PackageParameter
{
    public int Id { get; set; }

    public string ParameterName { get; set; }

    public string ParameterType { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
