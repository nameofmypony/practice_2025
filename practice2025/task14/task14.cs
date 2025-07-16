namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double length = (b - a) / threadsnumber;
        double result = 0.0;
        object lockObj = new();

        using (Barrier barrier = new(threadsnumber + 1))
        {
            Thread[] threads = new Thread[threadsnumber];

            for (int i = 0; i < threadsnumber; i++)
            {
                double start = a + i * length;
                double end = (i == threadsnumber - 1) ? b : start + length;

                threads[i] = new Thread(() =>
                {
                    double sum = ComputePartialIntegral(start, end, function, step);
                    lock (lockObj)
                    {
                        result += sum;
                    }
                    barrier.SignalAndWait();
                });
                threads[i].Start();
            }

            barrier.SignalAndWait();
        }

        return result;
    }

    private static double ComputePartialIntegral(double start, double end, Func<double, double> function, double step)
    {
        double sum = 0.0;
        double current = start;

        while (current < end)
        {
            double next = Math.Min(current + step, end);
            double height1 = function(current);
            double height2 = function(next);
            double width = next - current;
            sum += (height1 + height2) * width / 2.0;
            current = next;
        }

        return sum;
    }
}
