namespace chm2;

public class SquareRoot
{
    public SquareRoot(double e = 10 - 4)
    {
        Eps = e;
        Solve();
    }

    private List<List<Double>> A;
    private List<List<Double>> S;
    private List<List<Double>> D;
    private List<double> b;
    private double Eps;

    List<List<double>> MatrixMultiplication(List<List<double>> a, List<List<double>> b)
    {
        var c = new List<List<double>>();
        c.Add(new List<double>() {0, 0, 0});
        c.Add(new List<double>() {0, 0, 0});
        c.Add(new List<double>() {0, 0, 0});

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                c[i][j] = 0;
                for (int k = 0; k < 3; k++)
                {
                    c[i][j] += a[i][k] * b[k][j];
                }
            }
        }
        return c;
    }
    List<List<double>> MatrixTranspose(List<List<double>> a)
    {
        var c = new List<List<double>>();
        c.Add(new List<double>() {0, 0, 0});
        c.Add(new List<double>() {0, 0, 0});
        c.Add(new List<double>() {0, 0, 0});

        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                c[i][j] = a[j][i];
            }
        }
        return c;
    }
    private void DisplayMatrix(List<List<double>> Matrix)
    {
        foreach (var list in Matrix)
        {
            foreach (var num in list) Console.Write($"{num:n3} ");
            Console.WriteLine();
        }
    }
   
    void Solve()
    {
        A = new List<List<double>>();
        A.Add(new List<double>() { 3, -1, 1});
        A.Add(new List<double>() { -1, 2, 0.5});
        A.Add(new List<double>() { 1, 0.5 , 3});
        
        b = new List<double>();
        b = new List<double>() { 1, 1.75, 2.5};
        
        var at = MatrixTranspose(A);
        for(int i = 0; i < 3; i++)
        {

            if (!A[i].SequenceEqual(at[i]))
            {
                Console.WriteLine("METHOD IS NOT APPLIABLE.");
                return;
            }
        }
        S = new List<List<double>>();
        D = new List<List<double>>();
        

        var d11 = Math.Sign(A[0][0]);
        var s11 = Math.Sqrt(Math.Abs(A[0][0]));
        var s12 = A[0][1] / d11 / s11;
        var s13 = A[0][2] / d11 / s11;
        
        var d22 = Math.Sign(A[1][1] - s12 * s12 * d11);
        var s22 = Math.Sqrt(Math.Abs(A[1][1] - s12 * s12 * d11));
        var s23 = (A[1][2] - s12 * d11 * s13) / (d22 * s22);
        
        var d33 = Math.Sign(A[2][2] - s13 * s13 * d11 - s23 * s23 * d22);
        var s33 = Math.Sqrt(Math.Abs(A[2][2] - s13 * s13 * d11 - s23 * s23 * d22));

        S.Add(new List<double>() { s11, s12, s13 });
        S.Add(new List<double>() { 0, s22, s23 });
        S.Add(new List<double>() { 0, 0, s33 });
        Console.WriteLine("Matrix S:");
        DisplayMatrix(S);
        D.Add(new List<double>() { d11, 0, 0 });
        D.Add(new List<double>() { 0, d22, 0 });
        D.Add(new List<double>() { 0, 0, d33 });
        Console.WriteLine("Matrix D:");
        DisplayMatrix(D);
        
        var st = MatrixTranspose(S);
        var std = MatrixMultiplication(st, D);
        var y1 = b[0]/std[0][0];
        var y2 = (b[1]-std[1][0]*y1)/std[1][1];
        var y3 = (b[2] - std[2][0] * y1 - std[2][1] * y2)/std[2][2];

        var x3 = y3/S[2][2];
        var x2 = (y2 - S[1][2] * x3) / S[1][1];
        var x1 = (y1 - S[0][1]*x2 - S[0][2]*x3)/S[0][0];

        double det = d11 * s11 * s11 * d22 * s22 * s22 * d33 * s33 * s33;
        Console.WriteLine($"Det(A) = {det}");
        Console.WriteLine($"x1 = {x1}");
        Console.WriteLine($"x2 = {x2}");
        Console.WriteLine($"x3 = {x3}");
    }
}