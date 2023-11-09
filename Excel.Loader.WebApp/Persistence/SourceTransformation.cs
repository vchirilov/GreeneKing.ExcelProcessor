using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class SourceTransformation
{
    public string Server { get; set; }

    public string DatabaseOrFilePath { get; set; }

    public string TableName { get; set; }

    public string ColumnName { get; set; }

    public string Read { get; set; }

    public string Write { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
