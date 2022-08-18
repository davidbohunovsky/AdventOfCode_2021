string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

string line = File.ReadAllText(testDataPath);

int epochs = 256;

Dictionary<int,long>  population = new Dictionary<int, long>();

foreach (var point in line.Split(",").Select(int.Parse))
{
    if (population.ContainsKey(point))
    {
        population[point]++;
    }
    else
    {
        population.Add(point,1);
    }
}

for (int i = 0; i < epochs; i++)
{
    Dictionary<int, long> newPopulation = new Dictionary<int, long>();

    foreach (var point in population)
    {
        // Just get older
        if (point.Key != 0)
        {
            if (newPopulation.ContainsKey(point.Key - 1))
            {
                newPopulation[point.Key - 1] += point.Value;
            }
            else
            {
                newPopulation.Add(point.Key-1, point.Value);
            }
        }
        else
        {
            // Create new "points"
            if (newPopulation.ContainsKey(6))
            {
                newPopulation[6] += point.Value;
            }
            else
            {
                newPopulation.Add(6,point.Value);
            }
            
            newPopulation.Add(8,point.Value);
        }
    }
    
    population = newPopulation;
}

Console.WriteLine(population.Select(x => x.Value).Sum());
