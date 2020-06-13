using AAP.Registrar.AnalyticEnvironment.IntegratedReport.Sources;

namespace AAP.Registrar.AnalyticReportsTests.AnaliyticEnvironment
{
    /// <summary>
    /// Тестовая фабрика ряда с событиями для интегрального отчёта
    /// всегда возвращает заранее заданный ряд
    /// </summary>
    public class AnalyticSingleSourceFactory : IAnalyticSourceFactory
    {
        private readonly IAnalyticSource _source;

        public AnalyticSingleSourceFactory(IAnalyticSource source)
        {
            _source = source;
        }

        public IAnalyticSource GetSource()
        {
            return _source;
        }
    }
}