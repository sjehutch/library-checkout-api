using FluentAssertions;
using LibraryCheckout.Api.Data.Seed;
using LibraryCheckout.Api.Features.Checkouts;
using Microsoft.Extensions.Logging.Abstractions;

namespace LibraryCheckout.Api.Tests;

public sealed class CheckoutServiceTests
{
    [Fact]
    public void CheckoutBook_ShouldFail_WhenBookAlreadyCheckedOut()
    {
        var store = LibraryDataSeeder.CreateStore();
        var service = new CheckoutService(store, NullLogger<CheckoutService>.Instance);

        var result = service.CheckoutBook(LibrarySeedData.PragmaticProgrammerBookId, LibrarySeedData.PriyaMemberId);

        result.IsSuccess.Should().BeFalse();
        result.ErrorCode.Should().Be("book_unavailable");
    }

    [Fact]
    public void ReturnBook_ShouldMarkAsReturned()
    {
        var store = LibraryDataSeeder.CreateStore();
        var service = new CheckoutService(store, NullLogger<CheckoutService>.Instance);
        var checkoutId = store.Checkouts.First(checkout => !checkout.IsReturned).Id;

        var result = service.ReturnBook(checkoutId);

        result.IsSuccess.Should().BeTrue();
        result.Checkout!.ReturnedAtUtc.Should().NotBeNull();
    }
}
