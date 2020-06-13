using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AAP.Registrar.AnalyticEnvironment.IntegratedReport.Sources;
using AAP.Registrar.DataDescriptions.Tools;

namespace AAP.UnitTests.AnalyticReportsTests.AnaliyticEnvironment
{
    [TestClass]
    public class AnalyticSourceHelperTest
    {
        [TestMethod]
        [Description("Попытка расширить диапазоны в пустом списоке диапазонов дат")]
        public void ExpandEmptyDateRangesTest()
        {
            var source = Enumerable.Empty<IDateRange>();
            var before = new TimeSpan(1000);
            var after = new TimeSpan(1000);
            var collection = AnalyticSourceHelper.ExpandIntervals(source, before, after);
            Assert.IsNotNull(collection, "ExpandIntervals вернул null");
            Assert.AreEqual(collection.Count(), 0, "Коллекция не пуста");
        }

        [TestMethod]
        [Description("Попытка расширить диапазоны в списке из одного диапазона дат")]
        public void ExpandOneDateRangeTest()
        {
            var source = new[] { new DateRange(new DateTime(2020, 1, 15), new DateTime(2020, 2, 1))  };
            var before = TimeSpan.FromDays(1);
            var after = TimeSpan.FromDays(5);
            var expected = new[] { new DateRange(new DateTime(2020, 1, 14), new DateTime(2020, 2, 6)) };
            var collection = AnalyticSourceHelper.ExpandIntervals(source, before, after);
            DateRangeCollectionsComparer(expected, collection);
        }

        [TestMethod]
        [Description("Попытка расширить диапазоны в списке из двух не пересекающихся диапазонов дат")]
        public void ExpandTwoCutDateRangesTest()
        {
            var source = new[]
            {
                new DateRange(new DateTime(2020, 1, 15), new DateTime(2020, 2, 1)),
                new DateRange(new DateTime(2020, 1, 15), new DateTime(2020, 2, 1)),
            };
            var before = TimeSpan.FromDays(1);
            var after = TimeSpan.FromDays(5);
            var expected = new[]
            {
                new DateRange(new DateTime(2020, 1, 14), new DateTime(2020, 2, 6)),
                new DateRange(new DateTime(2020, 1, 14), new DateTime(2020, 2, 6)),
            };
            var collection = AnalyticSourceHelper.ExpandIntervals(source, before, after);
            DateRangeCollectionsComparer(expected, collection);
        }

        [TestMethod]
        [Description("Попытка расширить диапазоны в списке из двух пересекающихся диапазонов дат")]
        public void ExpandTwoSplitDateRangesTest()
        {
            var source = new[]
            {
                new DateRange(new DateTime(2020, 1, 15), new DateTime(2020, 2, 1)),
                new DateRange(new DateTime(2020, 2, 6), new DateTime(2020, 2, 1)),
            };
            var before = TimeSpan.FromDays(1);
            var after = TimeSpan.FromDays(5);
            var expected = new[]
            {
                new DateRange(new DateTime(2020, 1, 14), new DateTime(2020, 2, 6)),
            };
            var collection = AnalyticSourceHelper.ExpandIntervals(source, before, after);
            DateRangeCollectionsComparer(expected, collection);
        }

        [TestMethod]
        [Description("Попытка расширить диапазоны в списке. Комбинированный тест. Есть диапазоны, которые объединяются и которые не объединяются")]
        public void ExpandCombineRangesTest()
        {
            var source = new[]
            {
                new DateRange(new DateTime(2020, 1, 15), new DateTime(2020, 2, 1)),
                new DateRange(new DateTime(2020, 2, 6), new DateTime(2020, 2, 1)),
                new DateRange(new DateTime(2017, 1, 15), new DateTime(2017, 2, 1)),
            };
            var before = TimeSpan.FromDays(1);
            var after = TimeSpan.FromDays(5);
            var expected = new[]
            {
                new DateRange(new DateTime(2020, 1, 14), new DateTime(2020, 2, 6)),
                new DateRange(new DateTime(2017, 1, 14), new DateTime(2017, 2, 6)),
            };
            var collection = AnalyticSourceHelper.ExpandIntervals(source, before, after);
            DateRangeCollectionsComparer(expected, collection);
        }

        private void DateRangeCollectionsComparer(IEnumerable<IDateRange> expected, IEnumerable<IDateRange> real)
        {
            Assert.IsNotNull(real, "Коллекция интервалов равна null");
            var expectedList = expected.ToList();
            var realList = real.ToList();
            Assert.AreEqual(expectedList.Count, realList.Count, "Размер коллекции интервалов не соответствует ожидаемому");
            for (var i = 0; i < expectedList.Count; i++)
            {
                var expectedRange = new DateRange(expectedList[i]);
                var realRange = new DateRange(realList[i]);
                Assert.AreEqual(expectedRange, realRange, string.Format("Интервал {0} в коллекции не соответствует ожидаемому", i));
            }
        }
    }
}