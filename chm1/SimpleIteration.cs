using System;
using System.Collections.Generic;
using System.Linq;
namespace chm1;

public class Simple
{
    public Simple(double e = 1e-3)
    {
        Eps = e;
        SimpleIter();
    }
    static double F(double x) => Math.Pow(x, 4) + 4*x - 2;
    static double G(double x) => 0.25*(2-Math.Pow(x, 4));
    
    private double Eps;
    void SimpleIter()
    {
        double xn = 0.5;
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