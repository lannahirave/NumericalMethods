namespace chm2;

public class Task1
{
    // 5 2 1 0 = 14
    // 1 3 2 8 = 65 
    // 4 -6 1 0 = -3
    // 5 0 3 2 = 32
    public Task1(int size_ = 4)
    {
        size = size_;
        Gauss();
    }

    private int size;
    private List<List<double>> Matrix = new();
    private List<List<double>> InverseMatrix = new();

    private void DisplayMatrix(int? i)
    {
        if (i is not null) Console.WriteLine($"{i}-th iteration:");
        else Console.WriteLine("Matrix:");
        foreach (var list in Matrix)
        {
            foreach (var num in list) Console.Write($"{$"{num:0.00000}"} ");
            Console.WriteLine();
        }
    }

    private void DisplayInverseMatrix()
    {
        Console.WriteLine("Inverse matrix:");
        foreach (var list in InverseMatrix)
        {
            for (int i = 0; i < size; i++) 
                Console.Write($"{list[i]:0.0000} ");
            Console.WriteLine();
        }
    }

    private void Gauss()
    {
        Matrix.Add(new List<double>() {5, 2, 1, 0, 14});
        Matrix.Add(new List<double>() {1, 3, 2, 8, 65});
        Matrix.Add(new List<double>() {4, -6, 1, 0, -3});
        Matrix.Add(new List<double>() {5, 0, 3, 2, 32});
        InverseMatrix.Add(new List<double>() {1, 0, 0, 0, 0});
        InverseMatrix.Add(new List<double>() {0, 1, 0, 0, 0});
        InverseMatrix.Add(new List<double>() {0, 0, 1, 0, 0});
        InverseMatrix.Add(new List<double>() {0, 0, 0, 1, 0});


        //Gauss elimination
        double ratio;
        //Geting inverse matrix +
        for (int pos = size - 1; pos >= 0; pos--)
        {
            ratio = Matrix[pos][pos];
            if (ratio != 0)
                for (int col = size; col >= 0; col--)
                {
                    Matrix[pos][col] /= ratio;
                    InverseMatrix[pos][col] /= ratio;
                }
        }
        DisplayMatrix(125125);
        DisplayInverseMatrix();
        for (var i = 0; i < size; i++)
        {
            // ROW MAIN
            for (var j = i + 1; j < size; j++)
            {
                // Getting ratio
                ratio = Matrix[j][i] / Matrix[i][i];
                for (var k = i; k <= size; k++)
                {
                    // Eliminating to Upper Triangle Matrix
                    Matrix[j][k] -= ratio * Matrix[i][k];
                    // Finding Inverse Matrix
                    InverseMatrix[j][k] -= ratio * InverseMatrix[i][k];
                }
            }
            DisplayMatrix(i);
        }
        
        // Matrix is Upper Triangle now
        var answers = Enumerable.Repeat(0.0, size).ToList();
        // Getting solution
        answers[size - 1] = Matrix[size - 1][size] / Matrix[size - 1][size - 1];
        for (var i = size - 2; i >= 0; i--)
        {
            answers[i] = Matrix[i][size];

            for (var j = i + 1; j < size; j++)
                answers[i] = answers[i] - Matrix[i][j] * answers[j];

            answers[i] = answers[i] / Matrix[i][i];
        }
        
        
        DisplayMatrix(1241241124);
        DisplayInverseMatrix();
        
        for (int pos = size - 1; pos >= 0; pos--)
        {
            for (int row = pos-1; row >= 0; row--)
            {
                ratio = Matrix[row][pos];
                if (ratio == 0) continue;
                Matrix[row][size] -= ratio * Matrix[pos][size];
                Matrix[row][pos] -= ratio * Matrix[pos][pos];
                InverseMatrix[row][pos] -= ratio * InverseMatrix[pos][pos];
            }
            DisplayInverseMatrix();
        }
        DisplayMatrix(124124);

        DisplayInverseMatrix();
        Console.WriteLine("Solution:");
        for (var i = 0; i < size; i++) Console.WriteLine($"x{i + 1} = {answers[i],4}");
    }
}