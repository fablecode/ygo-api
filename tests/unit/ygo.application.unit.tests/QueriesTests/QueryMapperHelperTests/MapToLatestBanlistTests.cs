using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ygo.application.Queries;
using ygo.core.Constants;
using ygo.core.Models.Db;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests.QueryMapperHelperTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class MapToLatestBanlistTests
    {
        [Test]
        public void Given_An_Invalid_Banlist_Format_Should_Be_Null()
        {
            // Arrange

            // Act
            var result = QueryMapperHelper.MapToLatestBanlist(null);

            // Assert
            result.Format.Should().BeNull();
        }

        [Test]
        public void Given_A_Valid_Banlist_With_Forbidden_Cards_Forbidden_List_Should_Not_Be_NullOrEmpty()
        {
            // Arrange
            var banlist = new Banlist
            {
                BanlistCard = new List<BanlistCard>
                {
                    new BanlistCard
                    {
                        Limit = new Limit
                        {
                            Name = BanlistConstants.Forbidden
                        },
                        Card = new Card
                        {
                            Id = 43535,
                            Name = "Monster Reborn"
                        }
                    }
                },
                Format = new Format
                {
                    Acronym = "TCG"
                }
            };

            // Act
            var result = QueryMapperHelper.MapToLatestBanlist(banlist);

            // Assert
            result.Forbidden.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Given_A_Valid_Banlist_With_Limited_Cards_Limited_List_Should_Not_Be_NullOrEmpty()
        {
            // Arrange
            var banlist = new Banlist
            {
                BanlistCard = new List<BanlistCard>
                {
                    new BanlistCard
                    {
                        Limit = new Limit
                        {
                            Name = BanlistConstants.Limited
                        },
                        Card = new Card
                        {
                            Id = 43535,
                            Name = "Monster Reborn"
                        }
                    }
                },
                Format = new Format
                {
                    Acronym = "TCG"
                }
            };

            // Act
            var result = QueryMapperHelper.MapToLatestBanlist(banlist);

            // Assert
            result.Limited.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Given_A_Valid_Banlist_With_SemiLimited_Cards_SemiLimited_List_Should_Not_Be_NullOrEmpty()
        {
            // Arrange
            var banlist = new Banlist
            {
                BanlistCard = new List<BanlistCard>
                {
                    new BanlistCard
                    {
                        Limit = new Limit
                        {
                            Name = BanlistConstants.SemiLimited
                        },
                        Card = new Card
                        {
                            Id = 43535,
                            Name = "Monster Reborn"
                        }
                    }
                },
                Format = new Format
                {
                    Acronym = "TCG"
                }
            };

            // Act
            var result = QueryMapperHelper.MapToLatestBanlist(banlist);

            // Assert
            result.SemiLimited.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Given_A_Valid_Banlist_With_Unlimited_Cards_Unlimited_List_Should_Not_Be_NullOrEmpty()
        {
            // Arrange
            var banlist = new Banlist
            {
                BanlistCard = new List<BanlistCard>
                {
                    new BanlistCard
                    {
                        Limit = new Limit
                        {
                            Name = BanlistConstants.Unlimited
                        },
                        Card = new Card
                        {
                            Id = 43535,
                            Name = "Monster Reborn"
                        }
                    }
                },
                Format = new Format
                {
                    Acronym = "TCG"
                }
            };

            // Act
            var result = QueryMapperHelper.MapToLatestBanlist(banlist);

            // Assert
            result.Unlimited.Should().NotBeNullOrEmpty();
        }

    }
}