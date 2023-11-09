using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Job
{
    public int Id { get; set; }

    public string JobName { get; set; }

    public string Frequency { get; set; }

    public DateTime? LastUsed { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
