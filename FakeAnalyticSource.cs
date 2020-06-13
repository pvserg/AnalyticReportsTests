using System;
using System.Collections.Generic;
using AAP.Registrar.AnalyticEnvironment.IntegratedReport;
using AAP.Registrar.AnalyticEnvironment.IntegratedReport.Sources;
using AAP.Registrar.DataDescriptions.AnalyticEnvironment;
using AAP.Registrar.DataDescriptions.Tools;
using AAP.Registrar.UIElements.Tools;

namespace AAP.UnitTests.AnalyticReportsTests.AnaliyticEnvironment
{
    /// <summary>
    /// Тестовый ряд данных для интегрального отчёта
    /// </summary>
    public class FakeAnalyticSource : IAnalyticSource
    {
        public string SourceName { get; private set; }
        public Guid SourceTypeID { get; private set; }
        public Guid UniqueID { get; private set; }
        public Guid SourceID { get; private set; }
        public ContainerDataType DataType { get; private set; }
        public AnalyticSourceTypeEnum CreationType { get; set; }
        public VNObservableCollection<IDateRange> SourceData { get; private set; }
        public int Number { get; set; }

        public FakeAnalyticSource(Guid sourceID, Guid uniqueID, Guid sourceTypeID, string sourceName,
            ContainerDataType dataType, AnalyticSourceTypeEnum creationType, int number)
        {
            SourceName = sourceName;
            SourceTypeID = sourceTypeID;
            UniqueID = uniqueID;
            SourceID = sourceID;
            DataType = dataType;
            DataType = dataType;
            CreationType = creationType;
            Number = number;
        }

        public static FakeAnalyticSource CreateSimple(Guid sourceID, string name)
        {
            return new FakeAnalyticSource(sourceID, sourceID, AnalyticSourceTypes.JournalSourceID, name, 
                ContainerDataType.Event, AnalyticSourceTypeEnum.LiveSource, 0);
        }

        public IDateRange CloneDateRange(IDateRange source)
        {
            return new DateRange(source.Start, source.End);
        }

        public IAnalyticSource Copy(IEnumerable<IDateRange> data)
        {
            return new FakeAnalyticSource(SourceID, UniqueID, SourceTypeID, SourceName,
                DataType, CreationType, Number);
        }
    }
}