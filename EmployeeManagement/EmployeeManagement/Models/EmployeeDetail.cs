using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models;

public partial class EmployeeDetail
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? DepartmentId { get; set; }
}
