using TechBlog.NewsManager.API.Domain.Extensions;
using FluentAssertions;
using System.ComponentModel;

namespace TechBlog.NewsManager.API.Tests.Domain.Extensions
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void GetDescription_WithDescriptionAttribute_ReturnsDescription()
        {
            // Arrange
            var enumValue = TestEnum.ValueWithDescription;
            var expectedDescription = "This is a value with a description.";

            // Act
            var description = enumValue.GetDescription();

            // Assert
            description.Should().Be(expectedDescription);
        }

        [Fact]
        public void GetDescription_WithoutDescriptionAttribute_ReturnsEnumValueToString()
        {
            // Arrange
            var enumValue = TestEnum.ValueWithoutDescription;
            var expectedDescription = enumValue.ToString();

            // Act
            var description = enumValue.GetDescription();

            // Assert
            description.Should().Be(expectedDescription);
        }

        private enum TestEnum
        {
            [Description("This is a value with a description.")]
            ValueWithDescription,

            ValueWithoutDescription
        }
    }
}