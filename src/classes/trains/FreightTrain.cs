

using first_cs_project.src.Classes;

namespace first_cs_project.src.classes.trains
{
	internal class FreightTrain(int capacity, Schedule schedule, List<Cargo>? initialContents = null) : Train(capacity, schedule, initialContents?.ConvertAll(m => (IMovable)m))
	{
		protected override bool LoadCheck(IMovable movable)
		{
			return movable is Cargo;
		}

		public override void RandomizeContents()
		{
			Contents.Clear();

			if (Random.Shared.NextDouble() < 0.95)
				while (Load(new Cargo(Random.Shared.Next(1, 200))) != -1) { }
		}

		public override void Tick(Log log)
		{
			CurrentTick++;
			string prefix = $"FreightTrain(id={Id}):";

			if (CurrentTick % Schedule.Frequency == 0 && CurrentTick > 0)
			{
				log.AddEvent($"{prefix} Arrived {-CurrentDelay} minutes late.");

				CurrentDelay = -Schedule.CalculateDelay();
				CurrentTick = CurrentDelay;
				RandomizeContents();

				if (_r.NextDouble() < 0.75)
				{
					log.AddEvent($"{prefix} Only travelled through this station and did not stop.");
					return;
				}

				int unloadCount = _r.Next(0, Contents.Count);
				int unloadAmount = 0;
				for (int i = 0;i < unloadCount;i++)
				{
					int weight = Contents[0].UsedSpace;
					UnLoad(index: 0);
					unloadAmount += weight;
				}
				log.AddEvent($"{prefix} {unloadAmount}kg of cargo was unloaded.");
		
				int loadAmount = 0;
				while (_r.NextDouble() < 0.8 && GetRemainingCapacity() > 0)
				{
					int remaining = GetRemainingCapacity();
					int weight = remaining <= 20 ? remaining : _r.Next(1, 200); 
					loadAmount += weight;
					Load(new Cargo(weight));
				}
				log.AddEvent($"{prefix} {loadAmount}kg of cargo was loaded.");
			}
		}

		public override string ToString()
		{
			return "Freight" + base.ToString();
		}
	}
}
