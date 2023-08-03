using System;
using System.Collections.Generic;

namespace Mentorship.Models;

public partial class Mentor
{
    public int MentorId { get; set; }

    public int? UserId { get; set; }

    public string? Bio { get; set; }

    public string? AreaOfExpertise { get; set; }

    public decimal? HourlyRate { get; set; }

    public string? Availability { get; set; }

    public virtual ICollection<MentorMenteeAssignment> MentorMenteeAssignments { get; set; } = new List<MentorMenteeAssignment>();

    public virtual ICollection<MentorshipSession> MentorshipSessions { get; set; } = new List<MentorshipSession>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User? User { get; set; }
}
