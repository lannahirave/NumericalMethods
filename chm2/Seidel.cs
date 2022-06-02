using System.ComponentModel;

namespace chm2;
public class Seidel
{
    public Seidel(double e = 1e-4)
    {
        Eps = e;
        Solve();
    }

    private double Eps;

    double f1(double x1, double x2, double x3) => (double)1/3*(x2-x3+1);
    double f2(double x1, double x2, double x3) => 0.5*(x1-0.5*x3+1.75);
    double f3(double x1, double x2, double x3) => (double) 1/3*(-x1-0.5*x2+2.5);
    
    
double Det(List<List<double>> a)
{
    var matrix = a.Select(x => x.ToList()).ToList();
        int N = matrix[0].Count;
        double c, r = 1;
        for (int i = 0; i < N; i++)
        {
            for (int k = i + 1; k < N; k++)
            {
                c = matrix[k][i] / matrix[i][i];
                for (int j = i; j < N; j++)
                    matrix[k][j] = matrix[k][j] - c * matrix[i][j];
            }
        }
        for (int i = 0; i < N; i++)
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

    List<List<double>> InversedMatrix(List<List<double>> matrix)
    {
        int n = matrix[0].Count;
        double det = Det(matrix);
        if (det == 0)
        {
            Console.WriteLine("BAD MATRIX");
            throw new InvalidEnumArgumentException();
        }

        var toTranspose = new List<List<double>>();
        for (int row = 0; row < n; row++)
        {
            var line = new List<double>();
            for (int col = 0; col < n; col++)
            {
                var newMatrix = new List<List<double>>();
                for (int i = 0; i < n; i++)
                {
                    var newRow = new List<double>();
                    for (int j = 0; j < n; j++)
                    {
                        if (j != col && i != row)
                        {
                            newRow.Add(matrix[i][j]);
                        }
                    }
                    if (!newRow.Any())
                        continue;
                    newMatrix.Add(newRow);
                }
                var det0 =  Det(newMatrix);
                line.Add(det0);
            }
            toTranspose.Add(line);
        }

        var inv = MatrixTranspose(toTranspose);


        int sign = 1;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if ((i + j) % 2 == 1) sign = -1;
                else sign = 1;
                inv[i][j] /= det * sign;
            }
        }
        Console.WriteLine("Inversed matrix:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{inv[i][j]:N4} ");
                if (j % n == n-1) Console.Write("\n");
            }
        }

        return inv;
    }
    
    private void Solve()
    {
        double x01 = 0, x02 = 0, x03 = 0;
        double x11, x22, x33, x44;
        double e1, e2, e3, e;
        int i = 1;
        do
        {
            x11 = f1(x01, x02, x03);
            x22 = f2(x01, x02, x03);
            x33 = f3(x01, x02, x03);
            Console.WriteLine($"{i}-th iteration: {x11:n5}, {x22:n5}, {x33:n5}");
            e1 = Math.Abs(x01 - x11);
            e2 = Math.Abs(x02 - x22);
            e3 = Math.Abs(x03 - x33);
            e = Math.Sqrt(e1*e1 + e2*e2 + e3*e3);
            if (i==1) Console.WriteLine($"Euclid norm: ||({x11:n5},{x22:n5},{x33:n5})T-({x01:n5},{x02:n5},{x03:n5})T|| = {e}");
            if (e < Eps) break;
            x01 = x11;
            x02 = x22;
            x03 = x33;
            i++;
        } while (e > Eps);
        
        Console.WriteLine($"Euclid norm: ||({x11:n5},{x22:n5},{x33:n5})T-({x01:n5},{x02:n5},{x03:n5})T|| = {e}");
        Console.WriteLine("Solution:");
        Console.WriteLine($"x1 = {x11}");
        Console.WriteLine($"x2 = {x22}");
        Console.WriteLine($"x3 = {x33}");
        
        List<List<Double>> matrix = new List<List<double>>();
        matrix.Add(new List<double>() { 3, -1, 1});
        matrix.Add(new List<double>() { -1, 2, 0.5 });
        matrix.Add(new List<double>() { 1, 0.5, 3 });
        
        InversedMatrix(matrix);
        double normofmatrix = NormOfMatrix(matrix);
        double normofinver = NormOfMatrix(InversedMatrix(matrix));
        double det = Det(matrix);
        Console.WriteLine($"det(A) = {det}");;
        Console.WriteLine($"Norm of matrix: {normofmatrix}");
        Console.WriteLine($"Norm of inverted: {normofinver}");
        Console.WriteLine($"Cond(A) = {normofmatrix*normofinver}");
    }
}