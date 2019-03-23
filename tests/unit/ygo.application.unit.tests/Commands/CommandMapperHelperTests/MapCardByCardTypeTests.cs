using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using System;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.application.Enums;
using ygo.application.Mappings.Profiles;
using ygo.core.Models.Db;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands.CommandMapperHelperTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class MapCardByCardTypeTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new CardProfile()); }
            );

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Given_An_Invalid_CardType_Should_Throw_ArgumentOutOfRange_Exception()
        {
            // Arrange
            const YgoCardType cardType = (YgoCardType)7;

            var card = new Card {Id = 23424};

            // Act
            Action act = () => CommandMapperHelper.MapCardByCardType(_mapper, cardType, card);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Given_A_Spell_CardType_Should_Return_Object_Of_Type_SpellCardDto()
        {
            // Arrange
            const YgoCardType cardType = YgoCardType.Spell;

            var card = new Card { Id = 23424};

            // Act
            var result = CommandMapperHelper.MapCardByCardType(_mapper, cardType, card);

            // Assert
            result.Should().BeOfType<SpellCardDto>();
        }

        [Test]
        public void Given_A_Trap_CardType_Should_Return_Object_Of_Type_TrapCardDto()
        {
            // Arrange
            const YgoCardType cardType = YgoCardType.Trap;

            var card = new Card { Id = 23424};

            // Act
            var result = CommandMapperHelper.MapCardByCardType(_mapper, cardType, card);

            // Assert
            result.Should().BeOfType<TrapCardDto>();
        }


        [Test]
        public void Given_A_Monster_CardType_Should_Return_Object_Of_Type_TrapCardDto()
        {
            // Arrange
            const YgoCardType cardType = YgoCardType.Monster;

            var card = new Card { Id = 23424};

            // Act
            var result = CommandMapperHelper.MapCardByCardType(_mapper, cardType, card);

            // Assert
            result.Should().BeOfType<MonsterCardDto>();
        }
    }
}