using Entities.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Entities.DTO.User;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
}

