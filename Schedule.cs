using first_cs_project;

internal class Schedule
{
	private double _accuracy;

	public double Accuracy
	{
		get { return _accuracy; }
		set
		{
			if (value < 0 || value > 1)
				throw new ArgumentException("'Accuracy' must be between 0 and 1");

			_accuracy = value;
		}
	}

	public int Frequency { get; init; }

	public Schedule(double accuracy, int frequency)
	{
		Accuracy = accuracy;
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
