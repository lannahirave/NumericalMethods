namespace chm3;

public class PowerMethod
{
    public PowerMethod(double e)
    {
        Eps = e;
        Solve();
    }

    private double Eps;
    private double Det(List<List<double>> a)
    {
        var matrix = a.Select(x => x.ToList()).ToList();
        var N = matrix[0].Count;
        double c, r = 1;
        for (var i = 0; i < N; i++)
        for (var k = i + 1; k < N; k++)
        {
            c = matrix[k][i] / matrix[i][i];
            for (var j = i; j < N; j++)
                matrix[k][j] = matrix[k][j] - c * matrix[i][j];
        }

        for (var i = 0; i < N; i++)
            r *= matrix[i][i];
        return r;
    }
    double  NormOfMatrix(List<List<double>> matrix)
    {
        int n = matrix[0].Count;
        double max = matrix[0].Sum(x => Math.Abs(x));
        for ( int i = 1; i < n; i++)
        {
            double sum = matrix[i].Sum(x => Math.Abs(x));
            if (sum > max)
                max = sum;
        }
        return max;
    }

    void DisplayMatrix(List<List<double>> matrix)
    {
        int n = matrix[0].Count;
        int k = matrix.Count;
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < k; j++)
            {
                Console.Write(matrix[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    List<List<double>> MatrixSubstraction(List<List<double>> a, List<List<double>> b)
    {
        int n = a[0].Count;
        var crr1 = new List<List<double>>();

        for (int i = 0; i < n; i++)
        {
            crr1.Add(new List<double>() { 0, 0, 0 });
            for (int j = 0; j < n; j++)
                crr1[i][j] = a[i][j] - b[i][j];
        }

        return crr1;
    }

    void Solve()
    {
        List<List<double>> matrix = new List<List<double>>();
        matrix.Add(new List<double>() {1, 1.0/2, 1.0/3});
        matrix.Add(new List<double>() {1.0/2,1.0/3, 1.0/4});
        matrix.Add(new List<double>() {1.0/3, 1.0/4, 1.0/5});
        
        Console.WriteLine($"First minor: {matrix[0][0]}");
        Console.WriteLine($"Second minor: {matrix[0][0]*matrix[1][1]-matrix[0][1]*matrix[1][0]}");
        var det = Det(matrix);
        Console.WriteLine($"Third minor: {det}");
        
        var norm = NormOfMatrix(matrix);
        Console.WriteLine($"Norm and max eigen value: {norm}");
        
        var B = MatrixSubstraction(new List<List<double>>() 
        { 
            new List<double>() { norm, 0, 0 },
            new List<double>() { 0, norm, 0 },
            new List<double>() { 0, 0, norm } 
        }, matrix);
        Console.WriteLine(" Matrix B:");
        DisplayMatrix(B);
        Console.WriteLine();
        var xAns = new List<double>() { 1, 1, 1 };
        
        int n = matrix[0].Count;
        double eigenValue0, eigenValue1;
        
        var xNew = new List<double>();
        for (int j = 0; j < n; j++)
        {
            double ans = 0;
            for (int i = 0; i < n; i++)
            {
                ans += B[i][j] * xAns[i];
            }
            xNew.Add(ans);
        }

        int step = 1;
        Console.Write($"x{step} = (");
        foreach(var c in xNew) Console.Write($"{c} ");
        Console.WriteLine(")");
        eigenValue0 = xNew[0] / xAns[0];
        var vectorNorm = Math.Sqrt(xNew.Sum(x => Math.Pow(x, 2)));
        var normalizedVector = xNew.Select(x => x / vectorNorm).ToList();
        Console.Write($"e{step} = (");
        foreach(var c in normalizedVector) Console.Write($"{c} ");
        Console.WriteLine(")");
        Console.WriteLine($"Eigen value {step}: {eigenValue0}");

        while (true)
        {
            step++;
            xNew = new List<double>();
            for (int j = 0; j < n; j++)
            {
                double ans = 0;
                for (int i = 0; i < n; i++)
                {
                    ans += B[i][j] * normalizedVector[i];
                }
                xNew.Add(ans);
            }
            Console.Write($"x{step} = (");
            foreach(var c in xNew) Console.Write($"{c} ");
            Console.WriteLine(")");
            
            eigenValue1 = xNew[0] / normalizedVector[0];
            Console.WriteLine($"Eigen value {step}: {eigenValue1}");
            vectorNorm = Math.Sqrt(xNew.Sum(x => Math.Pow(x, 2)));
            normalizedVector = xNew.Select(x => x / vectorNorm).ToList();
            if (Math.Abs(eigenValue1 - eigenValue0) < Eps)
                break;
            eigenValue0 = eigenValue1;
        }
        Console.WriteLine($"Min Eigen Value:  {norm - eigenValue1}");
    }
    
}