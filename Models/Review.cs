using System;
using System.Collections.Generic;

namespace Mentorship.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? MentorId { get; set; }

    public int? MenteeId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Mentee? Mentee { get; set; }

    public virtual Mentor? Mentor { get; set; }
}
