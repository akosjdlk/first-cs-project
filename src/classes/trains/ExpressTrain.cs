using first_cs_project.src.Classes;

namespace first_cs_project.src.classes.trains
{
	internal class ExpressTrain(int capacity, Schedule schedule, List<Passenger>? initialContents = null) : Train(capacity, schedule, initialContents?.ConvertAll(m => (IMovable)m))
	{
		protected override bool LoadCheck(IMovable movable)
		{
			return movable is Passenger passenger && passenger.HasTicket;
		}

		public override void RandomizeContents()
		{
			Contents.Clear();

			while (Load(new Passenger(RandomNameGenerator.GenerateRandomName(), true)) != -1 && Random.Shared.NextDouble() < 0.95) { }
		}

		public override void Tick(Log log)
		{
			CurrentTick++;
			string prefix = $"ExpressTrain(id={Id}):";

			if (CurrentTick % Schedule.Frequency == 0 && CurrentTick > 0)
			{
				log.AddEvent($"{prefix} Arrived {-CurrentDelay} minutes late.");
				CurrentDelay = -Schedule.CalculateDelay();
				CurrentTick = CurrentDelay;
				RandomizeContents();
				
				int getOffCount = _r.Next(0, Contents.Count);
				for (int i = 0;i < getOffCount;i++)
				{
					UnLoad(index: i);
				}
				log.AddEvent($"{prefix} {getOffCount} passengers left the train.");

				int getOnCount = 0;
				while (_r.NextDouble() < 0.97 && GetRemainingCapacity() > 0)
				{
					if (_r.NextDouble() < 0.99)
					{
						getOnCount++;
						Load(new Passenger(RandomNameGenerator.GenerateRandomName(), true));
					} else
					{
						bool isMale = _r.NextDouble() < 0.5;
						string name = RandomNameGenerator.GenerateRandomName();
						log.AddEvent($"{prefix} {name} could not board the train because {(isMale ? "he" : "she")} did not have a ticket!");
					}
				}
				log.AddEvent($"{prefix} {getOnCount} passengers boarded the train.");
			}
		}

		public override string ToString()
		{
			return "Express" + base.ToString();
		}
	}
}
