using System;
using System.Collections.Generic;

namespace Mentorship.Models;

public partial class MentorMenteeAssignment
{
    public int AssignmentId { get; set; }

    public int? MentorId { get; set; }

    public int? MenteeId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public virtual Mentee? Mentee { get; set; }

    public virtual Mentor? Mentor { get; set; }
}
