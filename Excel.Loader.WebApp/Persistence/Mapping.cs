using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Mapping
{
    public int Id { get; set; }

    public string SourceServer { get; set; }

    public string SourceDatabase { get; set; }

    public string SourceTable { get; set; }

    public string SourceTableColumn { get; set; }

    public string Transformation { get; set; }

    public string DestinationServer { get; set; }

    public string DestinationDatabase { get; set; }

    public string DestinationTable { get; set; }

    public string DestinationTableColumn { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
