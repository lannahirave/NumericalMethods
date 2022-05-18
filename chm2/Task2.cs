namespace chm2;

public class Task2
{
    //2x1+4x2+0x3=20
    //4x1+1x2+5x3=37
    //0x1+5x2+2x3=30

    private List<List<double>> Matrix { get; set; } = new();

    public Task2(double e = 1e-3, int size = 3)
    {
        eps = e;
        this.size = size;
        Tridiagonal();
    }

    private double eps;
    private int size;

    private void Tridiagonal()
    {
        Matrix.Add(new List<double>() {2, 4, 0, 20});
        Matrix.Add(new List<double>() {4, 1, 5, 37});
        Matrix.Add(new List<double>() {0, 5, 2, 30});
        List<double> A = Enumerable.Repeat(0.0, size).ToList(),
            B = Enumerable.Repeat(0.0, size).ToList(),
            result = Enumerable.Repeat(0.0, size).ToList();
        A[0] = Matrix[0][1] / Matrix[0][0];
        B[0] = Matrix[0][size] / Matrix[0][0];
        double x;
        for (var i = 1; i < size; i++)
        {
            x = Matrix[i][i] - A[i - 1] * Matrix[i][i - 1];
            A[i] = Matrix[i][i + 1] / x;
            B[i] = (Matrix[i][size] - Matrix[i][i - 1] * B[i - 1]) / x;
        }

        result[size - 1] = B[size - 1];
        for (var i = size - 2; i >= 0; i--) result[i] = B[i] - A[i] * result[i + 1];
        Console.WriteLine("Solution: ");
        for (var i = 0; i < size; i++) Console.WriteLine($"x{i} = {result[i]}");
    }
}