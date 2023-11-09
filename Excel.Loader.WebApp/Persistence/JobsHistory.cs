using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class JobsHistory
{
    public int Id { get; set; }

    public string JobName { get; set; }

    public string StepName { get; set; }

    public DateTime? LastRunDateTime { get; set; }

    public TimeSpan? LastRunDuration { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
