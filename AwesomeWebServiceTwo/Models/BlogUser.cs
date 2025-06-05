﻿using System.ComponentModel.DataAnnotations;

namespace AwesomeWebServiceTwo.Models;

public class BlogUser
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}