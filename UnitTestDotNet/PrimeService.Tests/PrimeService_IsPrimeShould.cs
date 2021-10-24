using System;
using Xunit;

namespace PrimeService.Tests
{
    public class PrimeService_IsPrimeShould
    {
        private readonly Prime.Services.PrimeService _primeService;
        public PrimeService_IsPrimeShould()
        {
            _primeService = new Prime.Services.PrimeService();
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = _primeService.IsPrime(value);

            Assert.False(result, $"{value} should not be prime");
        }
        //public void IsPrime_InputIs1_ReturnFalse()
        //{
        //    var primeService = new Prime.Services.PrimeService();
        //    bool result = primeService.IsPrime(2);

        //    Assert.False(result, "1 should not be prime");
        //}
    }
}
