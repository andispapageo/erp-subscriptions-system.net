﻿using Application.Shared.Commands.Orders;
using Application.Shared.Exceptions;
using Application.Shared.ViewModels;
using FluentAssertions;
using static Functional.Tests.TestingUnit;
namespace Functional.Tests.Commands
{
    public class OrderDiscountsCommandsTests : BaseTest
    {
        [Test]
        public async Task OrderDiscountsCommandValidationTest()
        {
            var command = new InheritorPresenterCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task OrderDiscountsCommandTest()
        {
            var item = await SendAsync(new InheritorPresenterCommand
            {
                CustomerId = 1,
            });

            item.Should().NotBeNull();
            item.Should().As<IEnumerable<OrderVm>?>();
        }

    }
}