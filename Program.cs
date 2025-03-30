using MathNet.Numerics.LinearAlgebra;

class Program
{
    static void Main()
    {
        //Solve linear equation fulfilling the criteria Ax*b.
        //A is the coefficient matrix(each row is an equation).
        //x is a vector of the unknowns.
        //b is the result vector.
        //Solution matrix is: { {a,b,c},
        //                      {d,e,f},
        //                      {g,h,i},
        //                              }
        double[,] A = new double[,]
        {
            // Row sums
            {1, 1, 1, 0, 0, 0, 0, 0, 0}, // a + b + c = 35
            {0, 0, 0, 1, 1, 1, 0, 0, 0}, // d + e + f = 47
            {0, 0, 0, 0, 0, 0, 1, 1, 1}, // g + h + i = 34

            // Column sums
            {1, 0, 0, 1, 0, 0, 1, 0, 0}, // a + d + g = 31
            {0, 1, 0, 0, 1, 0, 0, 1, 0}, // b + e + h = 49
            {0, 0, 1, 0, 0, 1, 0, 0, 1},  // c + f + i = 36

            // Diagonal sums
            {1, 0, 0, 0, 0, 0, 0, 0, 0}, // a =10
            {0, 1, 0, 1, 0, 0, 0, 0, 0}, // d + b =27
            {0, 0, 1, 0, 1, 0, 1, 0, 0}, // g + e + c = 39
            {0, 0, 0, 0, 0, 1, 0, 1, 0}, // h + f = 29
            {0, 0, 0, 0, 0, 0, 0, 0, 1}  // i = 11
        };

        double[] b = { 35, 47, 34, 31, 49, 36, 10, 27, 39, 29, 11 };

        var matrixA = Matrix<double>.Build.DenseOfArray(A);
        var vectorB = Vector<double>.Build.DenseOfArray(b);

        // Use pseudoinverse to solve
        var solution = matrixA.PseudoInverse() * vectorB;

        // Print solution
        Console.WriteLine("Reconstructed 3x3 Matrix:");
        for (int i = 0; i < 9; i++)
        {
            Console.Write($"{solution[i],6:F2}");
            if ((i + 1) % 3 == 0) Console.WriteLine();
        }

        // Validate rows, columns, diagonals
        Console.WriteLine("\nValidation:");
        ValidateMatrix(solution.ToArray());
    }

    static void ValidateMatrix(double[] x)
    {
        int[] diagonalSums = { 10, 27, 39, 29, 11 };

        double a = x[0], b = x[1], c = x[2];
        double d = x[3], e = x[4], f = x[5];
        double g = x[6], h = x[7], i = x[8];

        // Row validations
        double r1 = a + b + c;
        double r2 = d + e + f;
        double r3 = g + h + i;

        // Column validations
        double c1 = a + d + g;
        double c2 = b + e + h;
        double c3 = c + f + i;

        // Diagonal validations (anti-diagonals)
        double d0 = a;           // index 0
        double d1 = d+b;       // index 1
        double d2 = g+e+c;   // index 2 (main anti-diagonal)
        double d3 = h+f;       // index 3
        double d4 = i;           // index 4

        Console.WriteLine($"Row 1: {r1:F2} (target: 35)");
        Console.WriteLine($"Row 2: {r2:F2} (target: 47)");
        Console.WriteLine($"Row 3: {r3:F2} (target: 34)");

        Console.WriteLine($"Col 1: {c1:F2} (target: 31)");
        Console.WriteLine($"Col 2: {c2:F2} (target: 49)");
        Console.WriteLine($"Col 3: {c3:F2} (target: 36)");

        Console.WriteLine($"Diag 0: {d0:F2} (target: {diagonalSums[0]})");
        Console.WriteLine($"Diag 1: {d1:F2} (target: {diagonalSums[1]})");
        Console.WriteLine($"Diag 2: {d2:F2} (target: {diagonalSums[2]})");
        Console.WriteLine($"Diag 3: {d3:F2} (target: {diagonalSums[3]})");
        Console.WriteLine($"Diag 4: {d4:F2} (target: {diagonalSums[4]})");
    }
}
