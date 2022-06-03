namespace chm3;

public class NewtonNonlinear
{
    private readonly double Eps;
    private double x0 = 1.25;

    private double y0;

    // sin(x-0.6) - y - 1.6 = 0
    // 3x - cos(y) -  0.9 = 0
    public NewtonNonlinear()
    {
        Eps = 1e-4;
        Solve();
    }

    private List<List<double>> Function(double x, double y)
    {
        return new List<List<double>> { new() { Math.Sin(x - 0.6) - y - 1.6, 3 * x - Math.Cos(y) - 0.9 } };
    }

    private List<List<double>> Jacobian(double x, double y)
    {
        // [first list in list] - first row of jacobian
        // [second list in list] - second row of jacobian
        var jacobian = new List<List<double>>();
        jacobian.Add(new List<double> { Math.Cos(x - 0.6), -1 });
        jacobian.Add(new List<double> { 3, Math.Sin(y) });
        return jacobian;
    }

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

    private double NormOfMatrix(List<List<double>> matrix)
    {
        var n = matrix[0].Count;
        var max = matrix[0].Sum(x => Math.Abs(x));
        for (var i = 1; i < n; i++)
        {
            var sum = matrix[i].Sum(x => Math.Abs(x));
            if (sum > max)
                max = sum;
        }

        return max;
    }

    private List<double> MatrixMultiplication(List<List<double>> a, List<List<double>> b)
    {
        var x1 = a[0][0] * b[0][0] + a[0][1] * b[1][0];
        var x2 = a[0][0] * b[0][1] + a[0][1] * b[1][1];
        var c = new List<double> { x1, x2 };
        ;
        return c;
    }

    private double NormOfVector(List<double> vector0)
    {
        var e1 = Math.Abs(vector0[0]);
        var e2 = Math.Abs(vector0[1]);
        var answer = Math.Sqrt(e1 * e1 + e2 * e2);
        return answer;
    }

    private List<List<double>> MatrixTranspose(List<List<double>> a)
    {
        var c = new List<List<double>>();
        c.Add(new List<double> { 0, 0 });
        c.Add(new List<double> { 0, 0 });


        for (var i = 0; i < 2; i++)
        for (var j = 0; j < 2; j++)
            c[i][j] = a[j][i];
        return c;
    }

    private List<List<double>> InversedMatrix(List<List<double>> matrix)
    {
        var n = matrix[0].Count;
        var det = Det(matrix);
        if (det == 0)
        {
            Console.WriteLine("BAD MATRIX");
            throw new Exception();
        }

        var toTranspose = new List<List<double>>();
        for (var row = 0; row < n; row++)
        {
            var line = new List<double>();
            for (var col = 0; col < n; col++)
            {
                var newMatrix = new List<List<double>>();
                for (var i = 0; i < n; i++)
                {
                    var newRow = new List<double>();
                    for (var j = 0; j < n; j++)
                        if (j != col && i != row)
                            newRow.Add(matrix[i][j]);
                    if (!newRow.Any())
                        continue;
                    newMatrix.Add(newRow);
                }

                var det0 = Det(newMatrix);
                line.Add(det0);
            }

            toTranspose.Add(line);
        }

        var inv = MatrixTranspose(toTranspose);


        int sign;
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            if ((i + j) % 2 == 1) sign = -1;
            else sign = 1;
            inv[i][j] /= det * sign;
        }
        return inv;
    }


    private void Solve()
    {
        double x1 = x0, x2 = y0;
        var i = 1;
        do
        {
            Console.Write($"{i} iteration: ");
            x0 = x1;
            y0 = x2;
            var jacobi = Jacobian(x1, x2);
            var det = Det(jacobi);
            var func = Function(x1, x2);
            var A1 = new List<List<double>> { new() { func[0][0], jacobi[0][1] }, new() { func[0][1], jacobi[1][1] } };
            var A2 = new List<List<double>> { new() { jacobi[0][0], func[0][0] }, new() { jacobi[1][0], func[0][1] } };
            x1 = x0 - Det(A1) / det;
            x2 = y0 - Det(A2) / det;
            Console.WriteLine($"{x1:N4} {x2:N4}");
            i++;
        } while (NormOfVector(new List<double> { x1 - x0, x2 - y0 }) > Eps);
    }
}