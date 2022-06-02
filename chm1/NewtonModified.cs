namespace chm1;
public class NewtonMod
{
    public NewtonMod(double e = 1e-4)
    {
        Eps = e;
        this.NewtonModified();
    }
    double Eps;
    static double F(double x) => Math.Pow(x, 4) + 4*x -  2;
    static double Df(double x) => 4 * Math.Pow(x, 3) +  4;
    void NewtonModified()
    {
        double a = 0, b = 1;
        double x = b;
        double func = Df(b);
        Console.WriteLine($"1-th iteration: {x} {F(x)}");
        for (int i = 0; Math.Abs(F(x)) > Eps; ++i) {
            x -= F(x) / func;
            Console.WriteLine($"{i + 2}-th iteration: {x} {F(x)}");
        }
    }
}