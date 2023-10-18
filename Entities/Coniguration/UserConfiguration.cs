using System;
using Entities.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Coniguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                FirstName = "Joe",
                LastName = "Do",
                Login = "admin",
                Password = "admin",
                Roles = ERoleEnum.User,
                IsDeleted = false,
            },

            new User
            {
                Id = new Guid("0f8fad5b-d9cb-469f-a165-60650528950e"),
                FirstName = "Kim",
                LastName = "Don",
                Login = "admin",
                Password = "admin",
                Roles = ERoleEnum.Admin,
                IsDeleted = false,
            });

    }
}

