using App.Metrics;
using App.Metrics.Counter;

namespace MonitoringApi_Demo.AppMetrics
{
    public class MetricsRegistry
    {
        public static CounterOptions QuantidadePessoasCriadas =>
            new CounterOptions
            {
                Name = "Pessoas criadas",
                Context = "PessoasApi",
                MeasurementUnit = Unit.Calls
            };

        public static CounterOptions QuantidadeConexoesDBCriadas =>
            new CounterOptions
            {
                Name = "Conexoes DB criadas",
                Context = "Database",
                MeasurementUnit = Unit.Calls
            };
    }
}
