
using first_cs_project.src.classes;
using first_cs_project.src.classes.trains;

namespace first_cs_project
{
	internal partial class Program
	{
		static readonly List<Train> Trains = [];

		static readonly List<List<Train>> DeletedTrains = [];

		static List<Train> TrainsAddedToday = [];

		static bool ListTrains()
		{
			if (Trains.Count == 0)
			{
				Console.WriteLine("There are no trains yet!");
			}
			foreach (var item in Trains)
			{
				Console.WriteLine(item.ToString());
			}
			return true;
		}

		static bool RemoveTrain()
		{
			if (Trains.Count == 0)
			{
				Console.WriteLine("There are no trains yet!");
				return true;
			}
			List<string> options_3 = new();

			foreach (Train t in Trains)
				options_3.Add(t.ToString());

			Menu menu_3 = new(options_3);
			int train_index = menu_3.Call() - 1;
			if (train_index < 0)
				return false;

			Console.Write("Successfully deleted train: ");
			Console.WriteLine(Trains[train_index]);

			DeleteTrain(train_index);

			return true;
		}

		static void FillMissingLists()
		{
			int missingLists = currentDay - DeletedTrains.Count;
			for (int i = 0;i < missingLists;i++)
			{
				DeletedTrains.Add([]);
			}
		}

		static void DeleteTrain(int index)
		{
			FillMissingLists();
			DeletedTrains[currentDay - 1].Add(Trains[index]);
			Trains.RemoveAt(index);
		}

		static bool AddTrain()
		{
			Menu menu_1 = new(["Express", "Freight"]);
			int option_1 = menu_1.Call();
			if (option_1 == 0)
				return false;
			string type = menu_1._menuOptions[option_1];

			double accuracy;
			int frequency;

			while (true)
			{
				int? acc = Menu.NumericInput("How accurate should this train be? (0-100) ");
				if (acc == null)
					continue;

				if (0 > acc || acc > 100)
					continue;

				accuracy = (double)acc / 100.0;
				break;
			}

			while (true)
			{
				int? f = Menu.NumericInput("How frequently should this train arrive? (10-360) ", allowZero: false);
				if (f == null)
					continue;

				if (10 > f || f > 360)
					continue;

				frequency = (int)f;
				break;
			}
			
			Train train = CreateTrain(accuracy, frequency, type);
			Console.WriteLine($"Successfully created train: {train})");
			Trains.Add(train);
			TrainsAddedToday.Add(train);
			return true;
		}

		static Train CreateTrain(double accuracy, int frequency, string type)
		{
			switch (type.ToLower())
			{
				case "express":
					return new ExpressTrain(200, new Schedule(accuracy, frequency));

				case "freight":
					return new FreightTrain(2000, new Schedule(accuracy, frequency));

				default:
					throw new Exception($"'{type}' is not a valid Train type.");
			}
		}
	}
}
