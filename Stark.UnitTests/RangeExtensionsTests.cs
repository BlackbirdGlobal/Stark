using Stark.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Stark.UnitTests
{
    public class RangeExtensionsTests
    {
        [Fact]
        public void MergeOverlapping()
        {
            //Arrange
            var ranges = new List<Range>
            {
                new Range() { L = 100, R = 130 },
                new Range() { L = 130, R = 150 },
                new Range() { L = 150, R = 200 }
            };
            //Act
            var merged = ranges.MergeOverlappingRanges();
            //Assert
            Assert.Single(merged);
            var result = merged.FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal(100, result.L);
            Assert.Equal(200, result.R);
        }

        [Fact]
        public void MergeOverlapping_NonOverlappingStays()
        {
            //Arrange
            var ranges = new List<Range>
            {
                new Range() { L = 100, R = 130 },
                new Range() { L = 130, R = 150 },
                new Range() { L = 150, R = 200 },
                new Range() { L = 201, R = 500 }
            };
            //Act
            var merged = ranges.MergeOverlappingRanges().OrderBy(x => x.L);
            //Assert
            Assert.Equal(2, merged.Count());
            var result = merged.FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal(100, result.L);
            Assert.Equal(200, result.R);
            result = merged.LastOrDefault();
            Assert.NotNull(result);
            Assert.Equal(201, result.L);
            Assert.Equal(500, result.R);
        }
    }
}
