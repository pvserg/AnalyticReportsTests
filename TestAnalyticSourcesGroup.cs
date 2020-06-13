using System;
using System.Collections.Generic;
using AAP.Registrar.AnalyticEnvironment.IntegratedReport.Sources;

namespace AAP.UnitTests.AnalyticReportsTests.AnaliyticEnvironment
{
    public class TestAnalyticSourcesGroup : AnalyticSourcesGroupBase
    {
        private readonly Dictionary<Guid, Guid[]> _partitionLinks = new Dictionary<Guid, Guid[]>();

        public void AddLink(Guid device, Guid[] partitions)
        {
            _partitionLinks[device] = partitions;
        }

        protected override Guid[] GetPartitions(IManagedEntityLink entityLink)
        {
            if (entityLink != null)
            {
                Guid[] partitions;
                if (_partitionLinks.TryGetValue(entityLink.SourceID, out partitions))
                {
                    return partitions;
                }
            }
            return new Guid[]{};
        }
    }
}