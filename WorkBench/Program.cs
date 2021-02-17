using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telemetry;

namespace WorkBench
{
    public class Program
    {
        private static Log4NetLogger _Log;
        private static DataDogMetricWriter _Metrics;

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += Application_Error;
            _Log = new Log4NetLogger(typeof(Program), "TelemetryTestApp", "test");
            _Metrics = new DataDogMetricWriter("https://api.datadoghq.com/api/v1/series", "foo", "DatadogTestBench", "test", _Log);
            var sw = Stopwatch.StartNew();

            Console.WriteLine($"Starting console application with arguments '{string.Concat(args)}'...");

            Experiment();
            AsyncExperiment().Wait();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Execution time: {sw.Elapsed}");
            Console.WriteLine("Press a key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void Application_Error(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                if (_Log != null)
                    _Log.Fatal("WorkBench application shutting down", ex, null);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal error encountered '{ex.Message}', cannot continue");
                Console.ResetColor();

                Console.WriteLine("Press a key to exit...");
                Console.ReadKey();

                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unhandled error: {ex.Message}, stacktrace: {ex.StackTrace}");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private static void Experiment()
        {
            _Log.Info("Starting application");
            _Log.Error("Test error");
        }

        private static async Task AsyncExperiment()
        {
            Random rand = new Random();

            DateTime endtime = DateTime.UtcNow.AddMinutes(100);

            while (DateTime.UtcNow < endtime)
            {
                await _Metrics.IncrementCounter("counter", new string[] { "group:" + (rand.Next(1, 4) + rand.Next(1, 4)).ToString() });

                //var payload = new List<KeyValuePair<DateTime, string>>
                //{
                //    new KeyValuePair<DateTime, string>(DateTime.UtcNow, rand.Next(1, 5).ToString()),
                //    new KeyValuePair<DateTime, string>(DateTime.UtcNow, rand.Next(1, 5).ToString()),
                //    new KeyValuePair<DateTime, string>(DateTime.UtcNow, rand.Next(1, 5).ToString())
                //};

                //await foo.Write(MetricType.count, "counter", null, payload);
                Thread.Sleep(rand.Next(1000, 10000));
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }
}