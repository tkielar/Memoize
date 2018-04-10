using FluentAssertions;
using Xunit;

namespace Tkielar.Utils.Memoize.Tests
{
    public class MemoizeTests
    {
        [Fact]
        public void CallingMemoizedFunctionForTheSecondTimeDoesNotInvokeFactory()
        {
            int numberOfFactoryCalls = 0;
            var memoized = Memoize.Create((string arg) => numberOfFactoryCalls++);

            memoized("test");
            memoized("test");

            numberOfFactoryCalls.Should().Be(1);
        }

        [Fact]
        public void CallingMemoizedFunctionWithDifferentArgumentInvokesFactory()
        {
            int numberOfFactoryCalls = 0;
            var memoized = Memoize.Create((string arg) => numberOfFactoryCalls++);

            memoized("test");
            memoized("test2");

            numberOfFactoryCalls.Should().Be(2);
        }

        [Fact]
        public void WhenMemoizedIsCalledItReturnsValueReturnedByFactory()
        {
            var memoized = Memoize.Create((int arg) => $"Result: {arg}");

            memoized(1).Should().Be("Result: 1");
            memoized(43).Should().Be("Result: 43");
        }

        [Fact]
        public void WhenMemoizedIsCalledWithSameArgumentItReturnsCachedResult()
        {
            int numberOfFactoryCalls = 0;
            var memoized = Memoize.Create((long arg) => ++numberOfFactoryCalls);

            memoized(3).Should().Be(1);
            memoized(3).Should().Be(1);
        }

        [Fact]
        public void WhenMemoizedIsCalledWithNullItAlwaysInvokesFactory()
        {
            int numberOfFactoryCalls = 0;
            var memoized = Memoize.Create((object arg) => ++numberOfFactoryCalls);

            memoized(null).Should().Be(1);
            memoized(null).Should().Be(2);
        }
    }
}
