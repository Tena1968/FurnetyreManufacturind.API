using System;
using System.Collections.Generic;

namespace FurnetureManufacturind.API.Models;

public partial class Note
{
    public int NotesId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
