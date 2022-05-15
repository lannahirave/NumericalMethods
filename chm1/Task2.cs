namespace chm1;
public class Task2
{
    public Task2(double e = 1e-3)
    {
        Eps = e;
        this.Newton();
    }
    double Eps;
    static double F(double x) => Math.Pow(x, 3) + 3*Math.Pow(x, 2) - x - 3;
    static double Df(double x) => 3 * Math.Pow(x, 2) + 6 * x - 1;
    void Newton()
    {
        double a = -4, b = -2;
        double x = a;

        Console.WriteLine($"1-th iteration: {x} {F(x)}");
        for (int i = 0; Math.Abs(F(x)) > Eps; ++i) {
            x -= F(x) / Df(x);
            Console.WriteLine($"{i + 2}-th iteration: {x} {F(x)}");
        }
    }
}