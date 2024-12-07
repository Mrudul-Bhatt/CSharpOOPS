using System;

namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _3_CompareDiscreteValues
{
    class Template1
    {
        public enum Operation
        {
            SystemTest,
            Start,
            Stop,
            Reset
        }

        public class SystemController
        {
            public string PerformOperation(Operation command) =>
               command switch
               {
                   Operation.SystemTest => RunDiagnostics(),
                   Operation.Start => StartSystem(),
                   Operation.Stop => StopSystem(),
                   Operation.Reset => ResetToReady(),
                   _ => throw new ArgumentException("Invalid enum value for command", nameof(command)),
               };

            private string RunDiagnostics() => "Diagnostics running...";
            private string StartSystem() => "System started.";
            private string StopSystem() => "System stopped.";
            private string ResetToReady() => "System reset.";
        }

        static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation(Operation.Start)); // Output: System started.
            Console.WriteLine(controller.PerformOperation(Operation.Reset)); // Output: System reset.
        }

    }

    class Template2
    {
        public class SystemController
        {
            public string PerformOperation(string command) =>
               command switch
               {
                   "SystemTest" => RunDiagnostics(),
                   "Start" => StartSystem(),
                   "Stop" => StopSystem(),
                   "Reset" => ResetToReady(),
                   _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
               };

            private string RunDiagnostics() => "Diagnostics running...";
            private string StartSystem() => "System started.";
            private string StopSystem() => "System stopped.";
            private string ResetToReady() => "System reset.";
        }

        static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation("Start")); // Output: System started.
            Console.WriteLine(controller.PerformOperation("Invalid")); // Throws ArgumentException
        }

    }

    class Template3
    {
        public class SystemController
        {
            public string PerformOperation(ReadOnlySpan<char> command) =>
               command switch
               {
                   "SystemTest" => RunDiagnostics(),
                   "Start" => StartSystem(),
                   "Stop" => StopSystem(),
                   "Reset" => ResetToReady(),
                   _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
               };

            private string RunDiagnostics() => "Diagnostics running...";
            private string StartSystem() => "System started.";
            private string StopSystem() => "System stopped.";
            private string ResetToReady() => "System reset.";
        }

        static void Main()
        {
            // Usage:
            var controller = new SystemController();
            Console.WriteLine(controller.PerformOperation("Stop".AsSpan())); // Output: System stopped.
        }
    }
}
