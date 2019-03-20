using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.application.Queries;
using ygo.application.Queries.ArchetypeSearch;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetArchetypeSearchTests
    {
        private IMediator _mediator;
        private ArchetypesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypesController(_mediator);
        }

        [Test]
        public async Task Given_An_ArchetypeSearchQuery_If_Not_Successful_Should_Return_BadRequest()
        {
            // Arrange
            var query = new ArchetypeSearchQuery();

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult());

            // Act
            var result = await _sut.GetArchetypeSearch(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_ArchetypeSearchQuery_If_Not_Successful_Should_Return_BadRequest_With_Errors()
        {
            // Arrange
            const string expected = "Page Number must greater than 0.";

            var query = new ArchetypeSearchQuery();

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult{ Errors = new List<string>{ "Page Number must greater than 0."}});

            // Act
            var result = await _sut.GetArchetypeSearch(query) as BadRequestObjectResult;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_An_ArchetypeSearchQuery_If_Successful_But_No_Archetypes_Found_Should_Return_NotFound()
        {
            // Arrange
            var query = new ArchetypeSearchQuery{ SearchTerm = "toons"};

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult{ IsSuccessful = true, Data = new PagedList<ArchetypeDto>(new List<ArchetypeDto>(), 0, 1, 10)});
            _sut.ControllerContext = new ControllerContext();
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _sut.GetArchetypeSearch(query);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_An_ArchetypeSearchQuery_If_Successful_Should_Return_OkResult()
        {
            // Arrange
            var query = new ArchetypeSearchQuery{ SearchTerm = "toons"};

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult{ IsSuccessful = true, Data = new PagedList<ArchetypeDto>(new List<ArchetypeDto>
            {
                new ArchetypeDto
                {
                    Id = 2342,
                    Name = "toons",
                    Url = "http://localhost/toons"
                }
            }, 0, 1, 10)});
            _sut.ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()};

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.Link(Arg.Any<string>(), Arg.Any<object>()).Returns("http://localhost/toons");

            _sut.Url = urlHelper;

            // Act
            var result = await _sut.GetArchetypeSearch(query);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_An_ArchetypeSearchQuery_Should_Invoke_ArchetypeSearchQuery_Once()
        {
            // Arrange
            var query = new ArchetypeSearchQuery{ SearchTerm = "toons"};

            var pagedList = new PagedList<ArchetypeDto>(new List<ArchetypeDto>
            {
                new ArchetypeDto
                {
                    Id = 2342,
                    Name = "toons",
                    Url = "http://localhost/toons"
                }
            }, 40, 2, 10);

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult{ IsSuccessful = true, Data = pagedList});
            _sut.ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()};

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.Link(Arg.Any<string>(), Arg.Any<object>()).Returns("http://localhost/toons");

            _sut.Url = urlHelper;

            // Act
            await _sut.GetArchetypeSearch(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<ArchetypeSearchQuery>());
        }


        [Test]
        public async Task Given_An_ArchetypeSearchQuery_Should_Return_Pagination_Header()
        {
            // Arrange
            var query = new ArchetypeSearchQuery{ SearchTerm = "toons"};

            _mediator.Send(Arg.Any<ArchetypeSearchQuery>()).Returns(new QueryResult
            {
                IsSuccessful = true,
                Data = new PagedList<ArchetypeDto>(new List<ArchetypeDto>
                {
                    new ArchetypeDto
                    {
                        Id = 2342,
                        Name = "toons",
                        Url = "http://localhost/toons"
                    }
                }, 0, 1, 10)
            });
            _sut.ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()};

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.Link(Arg.Any<string>(), Arg.Any<object>()).Returns("http://localhost/toons");

            _sut.Url = urlHelper;

            // Act
            await _sut.GetArchetypeSearch(query);

            // Assert
            _sut.Response.Headers.Should().ContainKey(ArchetypesController.XPagination);
        }
    }
}