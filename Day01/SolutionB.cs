namespace Day1
{
    public class SolutionB
    {
        public static int Solution(string filePath)
        {
            int resultCount = 0;
            int? lastNumber = null;
            int currentNumber = 0;
            
            string[] lines = File.ReadAllLines(filePath);
            var depths = lines.Select(int.Parse).ToList();
            
            for (int i = 2; i < lines.Length; i++)
            {
                currentNumber = depths.GetRange(i-2, 3).Sum();
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

            return resultCount;
        }
    }
}