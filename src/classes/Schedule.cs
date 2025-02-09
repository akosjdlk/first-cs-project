using first_cs_project;

internal class Schedule
{
	public readonly double Accuracy;

	public readonly int Frequency;

	public Schedule(double accuracy, int frequency)
	{
		if (accuracy < 0.0 || accuracy > 1.0)
		{
			throw new ArgumentException("'accuracy' should be between 0.0 and 1.0");
		}

		Accuracy = accuracy;

		if (10 > frequency || frequency > 360)
		{
			throw new ArgumentException("'frequency' should be between 10 and 360");
		}

		Frequency = frequency;
	}

	public int CalculateDelay()
	{
		double chance = Random.Shared.NextDouble();

		double delayFactor = (Frequency - 1) / 10.0;
		double baseDelay = -Math.Log(chance) * delayFactor * (1 - Accuracy);
		double randomSpikeChance = Random.Shared.NextDouble();
		if (randomSpikeChance < (1 - Accuracy))
		{
			baseDelay += (3 + Random.Shared.NextDouble() * 5) * Frequency / 10;
		}

		return (int)Math.Round(baseDelay);
	}
}
