using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.User;

public class User
{
    [Column("UserId")]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public bool IsDeleted { get; set; }
    public ERoleEnum Roles { get; set; }
}

