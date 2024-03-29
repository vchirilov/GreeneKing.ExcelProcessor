﻿using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; }

    public string PackageName { get; set; }

    public virtual Package PackageNameNavigation { get; set; }
}
