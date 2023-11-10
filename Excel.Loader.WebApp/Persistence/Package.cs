using System;
using System.Collections.Generic;
using Excel.Loader.WebApp.Persistence;

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

    public virtual ICollection<DestinationTransformation> DestinationTransformations { get; set; } = new List<DestinationTransformation>();

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();

    public virtual ICollection<Executable> Executables { get; set; } = new List<Executable>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<JobsHistory> JobsHistories { get; set; } = new List<JobsHistory>();

    public virtual ICollection<Mapping> Mappings { get; set; } = new List<Mapping>();

    public virtual ICollection<PackageParameter> PackageParameters { get; set; } = new List<PackageParameter>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<SourceTransformation> SourceTransformations { get; set; } = new List<SourceTransformation>();

    public virtual ICollection<Source> Sources { get; set; } = new List<Source>();
}
