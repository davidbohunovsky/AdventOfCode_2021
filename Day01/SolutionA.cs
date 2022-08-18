namespace Day1
{
    public static class SolutionA
    {
        public static int Solution(string filePath)
        {
            int resultCount = 0;
            
            using (StreamReader sr = new StreamReader(filePath))
            {
                int? lastNumber = null;
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    int.TryParse(line, out var currentNumber);

                    if (lastNumber == null)
                    {
                        Console.WriteLine($"{currentNumber} (N/A - no previous measurement)");
                        lastNumber = currentNumber;
                        continue;
                    }

                    if (currentNumber > lastNumber)
                    {
                        resultCount++;
                        Console.WriteLine($"{currentNumber} (increased)");
                        lastNumber = currentNumber;
                        continue;
                    }

                    Console.WriteLine($"{currentNumber} (decreased)");
                    lastNumber = currentNumber;
                }
            }
            
            return resultCount;
        }
    }
}