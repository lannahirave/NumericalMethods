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
            foreach (var num in list) Console.Write($"{num,3} ");
            Console.WriteLine();
        }
    }

    private void DisplayInverseMatrix()
    {
        Console.WriteLine("Inverse matrix:");
        foreach (var list in InverseMatrix)
        {
            foreach (var num in list) Console.Write($"{num,3} ");
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
        for (var i = 0; i < size; i++)
        {
            for (var j = i + 1; j < size; j++)
            {
                // Getting ratio
                ratio = Matrix[j][i] / Matrix[i][i];
                for (var k = i; k <= size; k++)
                {
                    // Eliminating to Upper Triangle Matrix
                    Matrix[j][k] = Matrix[j][k] - ratio * Matrix[i][k];
                    // Finding Inverse Matrix
                    InverseMatrix[j][k] = InverseMatrix[j][k] - ratio * InverseMatrix[i][k];
                }
            }
            DisplayMatrix(i);
        }


        // finding Inverse Matrix
        for (int i = size - 2; i >= 0; i--)
        for (int j = 0; j < size; j++)
        for (int k = i + 1; k < size; k++)
            InverseMatrix[i][j] -= InverseMatrix[k][j] * Matrix[i][k];
        DisplayInverseMatrix();
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

        Console.WriteLine("Solution:");
        for (var i = 0; i < size; i++) Console.WriteLine($"x{i + 1} = {answers[i],4}");
    }
}