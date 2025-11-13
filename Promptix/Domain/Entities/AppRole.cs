using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppRole : IdentityRole<int>
{
    public RoleType RoleType { get; set; }
}