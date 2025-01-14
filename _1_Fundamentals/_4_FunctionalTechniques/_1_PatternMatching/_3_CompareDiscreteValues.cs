namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _3_CompareDiscreteValues
{
    private class Template1
    {
        public enum Operation
        {
            SystemTest,
            Start,
            Stop,
            Reset
        }

        private static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation(Operation.Start)); // Output: System started.
            Console.WriteLine(controller.PerformOperation(Operation.Reset)); // Output: System reset.
        }

        public class SystemController
        {
            public string PerformOperation(Operation command)
            {
                return command switch
                {
                    Operation.SystemTest => RunDiagnostics(),
                    Operation.Start => StartSystem(),
                    Operation.Stop => StopSystem(),
                    Operation.Reset => ResetToReady(),
                    _ => throw new ArgumentException("Invalid enum value for command", nameof(command))
                };
            }

            private string RunDiagnostics()
            {
                return "Diagnostics running...";
            }

            private string StartSystem()
            {
                return "System started.";
            }

            private string StopSystem()
            {
                return "System stopped.";
            }

            private string ResetToReady()
            {
                return "System reset.";
            }
        }
    }

    private class Template2
    {
        private static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation("Start")); // Output: System started.
            Console.WriteLine(controller.PerformOperation("Invalid")); // Throws ArgumentException
        }

        public class SystemController
        {
            public string PerformOperation(string command)
            {
                return command switch
                {
                    "SystemTest" => RunDiagnostics(),
                    "Start" => StartSystem(),
                    "Stop" => StopSystem(),
                    "Reset" => ResetToReady(),
                    _ => throw new ArgumentException("Invalid string value for command", nameof(command))
                };
            }

            private string RunDiagnostics()
            {
                return "Diagnostics running...";
            }

            private string StartSystem()
            {
                return "System started.";
            }

            private string StopSystem()
            {
                return "System stopped.";
            }

            private string ResetToReady()
            {
                return "System reset.";
            }
        }
    }

    private class Template3
    {
        private static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation("Stop".AsSpan())); // Output: System stopped.
        }

        public class SystemController
        {
            public string PerformOperation(ReadOnlySpan<char> command)
            {
                return command switch
                {
                    "SystemTest" => RunDiagnostics(),
                    "Start" => StartSystem(),
                    "Stop" => StopSystem(),
                    "Reset" => ResetToReady(),
                    _ => throw new ArgumentException("Invalid string value for command", nameof(command))
                };
            }

            private string RunDiagnostics()
            {
                return "Diagnostics running...";
            }

            private string StartSystem()
            {
                return "System started.";
            }

            private string StopSystem()
            {
                return "System stopped.";
            }

            private string ResetToReady()
            {
                return "System reset.";
            }
        }
    }
}