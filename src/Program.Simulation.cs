

using first_cs_project.src.classes;
using first_cs_project.src.classes.trains;

namespace first_cs_project
{
	internal partial class Program
	{
		static bool ProcessSimulation(int choice)
		{
			switch (choice)
			{
				case 1:
					SimulateDays(1);
					break;

				case 2:
					int? days = Menu.NumericInput("How many days should be simulated? ", allowZero: false);

					if (days == null)
						return false;

					SimulateDays(days.Value);
					break;

				case 3:
					BrowseLogs(currentDay - 1);
					break;

				case 4:
					while (BrowseLogs(null))
					{
						Menu.WaitForEnter("Press Enter to continue. . .");
					}
					return false;

				default:
					return false;
			}
			return true;
		}

		static void SimulateDays(int day)
		{
			
			for (int i = 0;i < day;i++)
			{
				Console.Clear();Console.WriteLine("\x1b[3J");
				Console.WriteLine($"Simulating day {i+1}/{day}. (Current day: {currentDay})");
				Inner();
			}

			Console.Clear();Console.WriteLine("\x1b[3J");
			if (day == 1)
			{
				Console.WriteLine("A day has been simulated, check the logs for details.");
			} else
			{
				Console.WriteLine($"{day} days have been simulated, check the logs for details.");
			}

			static void Inner()
			{
				Log log = new($"{currentDay}. day log");
				List<Train> trainsDeletedToday = [];

				FillMissingLists();
				trainsDeletedToday = new(DeletedTrains[currentDay - 1]);


				foreach (var train in TrainsAddedToday)
				{
					Train? deletedTrain = DeletedTrains[currentDay - 1].Find(t => t.Id == train.Id);
					if (deletedTrain != null)
					{
						trainsDeletedToday.Remove(deletedTrain);
						log.AddEvent($"Created and deleted train: {train}");
					} else
					{
						log.AddEvent($"Created train: {train}");
					}
				}

				foreach (var train in trainsDeletedToday)
					log.AddEvent($"Deleted train: {train}");

				for (int i = 0; i < 1440; i++)
				{
					foreach (var train in Trains)
					{
						train.Tick(log);
					}
				}
				log.Seal();
				Logs.Add(log);
				currentDay++;
				TrainsAddedToday.Clear();
			}
		}

		static bool BrowseLogs(int? day)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			if (currentDay == 1)
			{
				Console.WriteLine("You haven't simulated any days yet!");
				return false;
			}
			
			if (day == null)
			{
				List<string> logNames = [];

				foreach (var l in Logs)
				{
					logNames.Add($"day - {l.Id}");
				}

				Menu menu = new(logNames);
				int choice = menu.Call();

				if (choice == 0)
					return false;

				DisplayLog(Logs[choice - 1]);
				return true;
			} else
			{
				Log log;
				try
				{
					log = Logs[day.Value - 1];
				} catch (Exception)
				{
					Console.WriteLine("This day has not been simulated yet!");
					return false;
				}

				DisplayLog(log);
				return true;
			}
		}

		static void DisplayLog(Log log)
		{
			Console.Clear();Console.WriteLine("\x1b[3J");
			foreach (string e in log.Events)
			{
				Console.WriteLine(e);
			}
		}

	}
}
