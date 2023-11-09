using System;
using System.Collections.Generic;

namespace Excel.Loader.WebApp.Persistence;

public partial class ControlFlow
{
    public int Id { get; set; }

    public string PackageName { get; set; }

    public byte[] ControlFlow1 { get; set; }
}
