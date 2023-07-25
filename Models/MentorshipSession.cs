using System;
using System.Collections.Generic;

namespace Mentorship.Models;

public partial class MentorshipSession
{
    public int SessionId { get; set; }

    public int? MentorId { get; set; }

    public int? MenteeId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? SessionNotes { get; set; }

    public int? SessionRating { get; set; }

    public virtual Mentee? Mentee { get; set; }

    public virtual Mentor? Mentor { get; set; }
}
