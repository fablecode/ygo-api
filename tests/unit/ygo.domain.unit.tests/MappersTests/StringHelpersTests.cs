using FluentAssertions;
using NUnit.Framework;
using ygo.domain.Helpers;
using ygo.tests.core;

namespace ygo.domain.unit.tests.MappersTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class StringHelpersTests
    {
        [TestCase("image*.png", "image.png")]
        [TestCase("image<.png", "image.png")]
        [TestCase("image>.png", "image.png")]
        [TestCase("image|.png", "image.png")]
        public void Given_An_Invalid_FileName_Should_Return_Santized_FileName(string filename, string expected)
        {
            // Arrange
            // Act
            var result = filename.SanitizeFileName();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

    }
}