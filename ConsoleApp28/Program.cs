using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("number of items: ");
        int n = int.Parse(Console.ReadLine());

        int[] A = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter the value ({0}): ", i + 1);
            A[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("\nThe items is: ");
        for (int i = 0; i < n; i++)
        {
            Console.Write("  {0}", A[i] + " |  ");
        }


        Console.WriteLine("\n\n---------------------------------------------------------------");
        //madian
        MedianFinder findeMED = new MedianFinder();
        Console.WriteLine("\n\nThe median value is: {0}", findeMED.FindMedian(A));




        Console.WriteLine("---------------------------------------------------------------");
        //mode
        ModeFinder findeMOD = new ModeFinder();
        int[] modes = findeMOD.FindMode(A);

        if (modes.Length == 1)
        {
            Console.WriteLine("\nThe mode value is: {0}", modes[0]);
        }
        else
        {
            Console.WriteLine("\nThe mode values are:");
            foreach (int mode in modes)
            {
                Console.WriteLine("{0}", mode);



            }
        }

        Console.WriteLine("---------------------------------------------------------------");
        //range
        RangeFinder findeRange = new RangeFinder();
        int range = findeRange.FindRange(A);

        Console.WriteLine("\nThe range is: {0}", range);
        //FirstQuartile
        Console.WriteLine("\n\n---------------------------------------------------------------");
        double firstQuartile = FIRSTQuartileCalc.FirstQuartile(A);

        Console.WriteLine("The first quartile is: {0}", firstQuartile);
        //third_Quartile
        Console.WriteLine("\n\n---------------------------------------------------------------");
        double thirdQuartile = third_DQuartileCalc.ThirdQuartile(A);

        Console.WriteLine("The third quartile is: {0}", thirdQuartile);

        Console.WriteLine("\n\n---------------------------------------------------------------");
        double p90 = P90Calc.P90(A);

        Console.WriteLine("The P90 of the items is: {0}", p90);

        Console.WriteLine("\n\n---------------------------------------------------------------");

        double iqr = QuartileCalculator.InterquartileRange(A);
        Console.WriteLine("The interquartile range of the items is: {0}", iqr);
        Console.WriteLine("\n\n---------------------------------------------------------------");

        Tuple<double, double> boundaries = QuartileCalculator.OutlierBoundaries(A);
        Console.WriteLine("The boundaries of the outlier region are: [{0}, {1}]", boundaries.Item1, boundaries.Item2);
        Console.WriteLine("\n\n---------------------------------------------------------------");


        Console.Write("Enter a value to check if it's an outlier: ");
        int value = int.Parse(Console.ReadLine());
        bool isOutlier = QuartileCalculator.IsOutlier(value, A);
        if (isOutlier)
        {
            Console.WriteLine("{0} is an outlier.", value);
        }
        else
        {
            Console.WriteLine("{0} is not an outlier.", value);







        }
    }
}


class MedianFinder
{
    public double FindMedian(int[] nums)
    {
        Array.Sort(nums);
        Console.WriteLine("\nThe items from From smallest to largest:");
        for (int i = 0; i < nums.Length; i++)
        {
            Console.Write("  {0}", nums[i] + " |  ");
        }

        int N = nums.Length;
        if (N % 2 == 0)
        {
            return (nums[N / 2 - 1] + nums[N / 2]) / 2.0;
        }
        else
        {
            return nums[N / 2];
        }
    }
}




class ModeFinder
{
    public int[] FindMode(int[] nums)
    {
        int bigRep = 0;
        int numModes = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            int rep = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] == nums[i])
                {
                    rep++;
                }
            }
            if (rep > bigRep)
            {
                bigRep = rep;
                numModes = 1;
            }
            else if (rep == bigRep)
            {
                numModes++;
            }
        }
        int[] modes = new int[numModes];
        int x = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            int rep = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] == nums[i])
                {
                    rep++;
                }
            }
            if (rep == bigRep)
            {
                modes[x] = nums[i];
                x++;
            }
        }
        return modes;
    }
}

class RangeFinder
{
    public int FindRange(int[] nums)
    {
        int min = nums[0];
        int max = nums[0];
        foreach (int num in nums)
        {
            if (num < min)
            {
                min = num;
            }
            if (num > max)
            {
                max = num;
            }
        }
        return max - min;
    }
}





class FIRSTQuartileCalc
{
    public static double FirstQuartile(int[] values)
    {
        Array.Sort(values);

        int n = values.Length;
        int mid = n / 2;

        
        int lowerHalfEnd = (n % 2 == 0) ? mid - 1 : mid;

        int[] lowerHalf = new int[lowerHalfEnd + 1];
        Array.Copy(values, 0, lowerHalf, 0, lowerHalfEnd + 1);

        return Median(lowerHalf);
    }

    private static double Median(int[] values)
    {
        int n = values.Length;
        int mid = n / 2; 

        if (n % 2 == 0)
        {
            
            return (values[mid - 1] + values[mid]) / 2.0;
        }
        else
        {
            return values[mid];
        }
    }
}






class third_DQuartileCalc
{
    public static double ThirdQuartile(int[] values)
    {
        int n = values.Length;
        int middle = n / 2;

        int[] upperHalf = new int[n - middle];
        Array.Copy(values, middle, upperHalf, 0, n - middle);

        return Median(upperHalf);
    }

    private static double Median(int[] values)
    {
        int n = values.Length;
        int middle = n / 2;

        if (n % 2 == 0)
        {
            return (values[middle - 1] + values[middle]) / 2.0;
        }
        else
        {
            return values[middle];
        }
    }
}


class P90Calc
{
    public static double P90(int[] values)
    {
        Array.Sort(values);
        int n = values.Length;
        int index = (int)Math.Ceiling(0.9 * n) - 1;

        return values[index];
    }
}


class QuartileCalculator
{
    public static double FirstQuartile(int[] values)
    {
        Array.Sort(values);
        int n = values.Length;
        int middle = n / 2;

        if (n % 2 == 0)
        {
            return Median(values, 0, middle - 1);
        }
        else
        {
            return Median(values, 0, middle);
        }
    }

    public static double ThirdQuartile(int[] values)
    {
        Array.Sort(values);
        int n = values.Length;
        int middle = n / 2;

        if (n % 2 == 0)
        {
            return Median(values, middle, n - 1);
        }
        else
        {
            return Median(values, middle + 1, n - 1);
        }
    }

    private static double Median(int[] values, int start, int end)
    {
        int n = end - start + 1;
        int middle = n / 2;

        if (n % 2 == 0)
        {
            return (values[start + middle - 1] + values[start + middle]) / 2.0;
        }
        else
        {
            return values[start + middle];
        }
    }

    public static double InterquartileRange(int[] values)
    {
        double q1 = FirstQuartile(values);
        double q3 = ThirdQuartile(values);
        return q3 - q1;
    }

    public static Tuple<double, double> OutlierBoundaries(int[] values)
    {
        double q1 = FirstQuartile(values);
        double q3 = ThirdQuartile(values);
        double iqr = InterquartileRange(values);

        double lowerBoundary = q1 - (1.5 * iqr);
        double upperBoundary = q3 + (1.5 * iqr);

        return Tuple.Create(lowerBoundary, upperBoundary);
    }

    public static bool IsOutlier(int value, int[] values)
    {
        Tuple<double, double> boundaries = OutlierBoundaries(values);
        double lowerBoundary = boundaries.Item1;
        double upperBoundary = boundaries.Item2;

        return (value < lowerBoundary || value > upperBoundary);
    }
}










