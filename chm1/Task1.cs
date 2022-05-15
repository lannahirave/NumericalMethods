namespace chm1;

public class Task1
{
    public Task1(double e = 1e-3)
    {
        Eps = e;
        this.Relaxsation();
    }
    static double F(double x) => Math.Pow(x, 3) - 6 * Math.Pow(x, 2) + 5 * x + 12;

    static double Df(double x) =>  3 * Math.Pow(x, 2) - 12 * x + 5;

    private double Eps;
    void Relaxsation()
    {
        double a = -2, b = -0;
        double min, max, t;
        min = Df(b);
        max = Df(a);
        t = 2 / (min + max);

        double x0 = a, xn;
        double subtraction;

        int i = 1;
        Console.WriteLine($"{i++}-th iteration {x0} {F(x0)}");
        do {
            xn = x0 - t * F(x0);
            subtraction = Math.Abs(xn - x0);
            x0 = xn;
            Console.WriteLine($"{i++}-th iteration {xn} {F(xn)}");

        } while (subtraction > Eps);
    }

}