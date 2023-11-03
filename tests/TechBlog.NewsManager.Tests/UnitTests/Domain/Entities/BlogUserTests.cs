using FluentAssertions;
using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.Tests.UnitTests.Domain.Entities
{
    public class BlogUserTests
    {
        [Fact]
        public void BlogUser_Exists_Should_Return_True_When_InternalId_Is_Not_Empty()
        {
            // Arrange
            var blogUser = new BlogUser
            {
                InternalId = Guid.NewGuid()
            };

            // Act
            var result = blogUser.Exists;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void BlogUser_Exists_Should_Return_False_When_InternalId_Is_Empty()
        {
            // Arrange
            var blogUser = new BlogUser();

            // Act
            var result = blogUser.Exists;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void BlogUser_WithInternalIdMapped_Should_Return_The_Same_Instance()
        {
            // Arrange
            var blogUser = new BlogUser
            {
                Id = Guid.NewGuid().ToString()
            };

            // Act
            var result = blogUser.WithInternalIdMapped();

            // Assert
            result.Should().BeSameAs(blogUser);
        }

        [Fact]
        public void BlogUser_WithInternalIdMapped_Should_Map_Id_To_InternalId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var blogUser = new BlogUser
            {
                Id = id.ToString()
            };

            // Act
            var result = blogUser.WithInternalIdMapped();

            // Assert
            result.InternalId.Should().Be(id);
        }
    }
}