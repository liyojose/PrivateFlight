using System;
using System.Collections.Generic;

namespace PrivateFlight.Data;

public partial class Message
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Message1 { get; set; }

    public string CountryCode { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Type { get; set; }
}
