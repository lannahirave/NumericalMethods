using System;
using System.Collections.Generic;
using System.Linq;
namespace chm1;

public class Task3
{
    public Task3(double e = 1e-3)
    {
        Eps = e;
        Simple();
    }
    static double F(double x) => Math.Pow(x, 3) + Math.Pow(x, 2) - 4 * x - 4;
    static double G(double x) => Math.Pow((4 * x + 4 - x * x), (double)1/3);
    
    private double Eps;
    void Simple()
    {
        double xn = 2.5;
        double xnplus1;
        int i = 1;
        while (true)
        {
            xnplus1 = G(xn);
            if (Math.Abs(xnplus1 - xn) < Eps)
            {
                break;
            }

            xn = xnplus1;
            Console.WriteLine($"{i}-th iteration: {xnplus1} {F(xnplus1)}");
            i++;
        }
    }
}