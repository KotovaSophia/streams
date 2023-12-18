class Program
{
    static void Main(string[] args)
    {
        var vector = new double[] { 1, 2, 3, 4, 5 };
        var taskCount = 2; 

        var sum = CalculateSum(vector, taskCount); 
        var norm = Math.Sqrt(sum);
        Console.WriteLine("Сумма квадратов: " + sum + "; Норма вектора: " + norm);

        DateTime dt1, dt2;
        dt1 = DateTime.Now;
      
        dt2 = DateTime.Now;
        TimeSpan ts = dt2 - dt1;
        Console.WriteLine("Total time: {0}", ts.TotalMilliseconds);
    }

    private static double CalculateSum(double[] vector, int taskCount)
    {
        var len = vector.Length;

        if (taskCount > len)
            throw new ArgumentException();

        var step = (len + 1) / taskCount; //Интервал для суммы
        var tasks = new Task<double>[taskCount];
        for (var i = 0; i < taskCount; i++)
        {
            tasks[i] = Task<double>.Factory.StartNew((obj) => Sum(vector, (int)obj * step, ((int)obj + 1) * step - 1), i);
        }
        Task.WaitAll(tasks);
        var sum = tasks.Select(x => x.Result).Sum();
        return sum;
    }



    static double Sum(IEnumerable<double> vector, int index1, int index2)
    {
        index2 = index2 >= vector.Count() ? vector.Count() : index2;
        return vector.Skip(index1).Take(index2 - index1 + 1).Select(x => x * x).Sum();
    }
}