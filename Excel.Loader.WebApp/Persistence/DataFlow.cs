using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class DataFlow
{
    public int Id { get; set; }

    public string PackageName { get; set; }

    public byte[] DataFlow1 { get; set; }
}
