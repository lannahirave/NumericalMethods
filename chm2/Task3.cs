namespace chm2;
public class Task3
{
    public Task3(double e = 1e-3)
    {
        Eps = e;
        Jacobi();
    }

    private double Eps;

    double f1(double x1, double x2, double x3, double x4) => 0.25 * (11 - x3 - x4);
    double f2(double x1, double x2, double x3, double x4) => (double) 1 / 3 * (10 - x4);
    double f3(double x1, double x2, double x3, double x4) => 0.5 * (7 - x1);
    double f4(double x1, double x2, double x3, double x4) => 0.2 * (23 - x1 - x2);

    private void Jacobi()
    {
        double x01 = 0, x02 = 0, x03 = 0, x04 = 0;
        double x11, x22, x33, x44;
        double e1, e2, e3, e4;
        int i = 1;
        do
        {
            x11 = f1(x01, x02, x03, x04);
            x22 = f2(x01, x02, x03, x04);
            x33 = f3(x01, x02, x03, x04);
            x44 = f4(x01, x02, x03, x04);
            Console.WriteLine($"{i}-th iteration: {x11}, {x22}, {x33}, {x44}");
            e1 = Math.Abs(x01 - x11);
            e2 = Math.Abs(x02 - x22);
            e3 = Math.Abs(x03 - x33);
            e4 = Math.Abs(x04 - x44);

            x01 = x11;
            x02 = x22;
            x03 = x33;
            x04 = x44;
            i++;
        } while (e1 > Eps && e2 > Eps && e3 > Eps && e4 > Eps);

        Console.WriteLine("Solution:");
        Console.WriteLine($"x1 = {x11}");
        Console.WriteLine($"x2 = {x22}");
        Console.WriteLine($"x3 = {x33}");
        Console.WriteLine($"x4 = {x44}");
    }
}