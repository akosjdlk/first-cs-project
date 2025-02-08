namespace first_cs_project
{
	internal class Program
    {
		static List<Train> Trains = [];
        static void Main()
        {
			Menu menu = new(["Create Train", "Delete Train", "List Trains", "Start simulation"], "Exit");
			bool shouldExit = false;
			while (!shouldExit)
			{
				int option = menu.Step();
				if (option == 0)
					break;

				bool wroteToStdout = ProcessOption(option);

				if (wroteToStdout)
					menu.WaitForEnter("Press any key to continue. . .");
			}
			
		}

		static bool ProcessOption(int _option)
		{
			switch (_option)
			{
				case 1: // Add Train

					Menu menu_1 = new(["Express", "Freight"]);
					int option_1 = menu_1.Step();
					if (option_1 == 0)
						return false;
					string type = menu_1._menuOptions[option_1];

					double accuracy;
					int frequency;

					while (true)
					{
						int? acc = menu_1.NumericInput("How accurate should this train be? (0-10) ");
						if (acc == null)
							continue;

						if (0 > acc || acc > 10)
							continue;

						 accuracy = (double)acc / 10.0;
						break;
					}

					while (true)
					{
						int? f = menu_1.NumericInput("How frequently should this train arrive? (10-120) ");
						if (f == null)
							continue;

						if (10 > f || f > 120)
							continue;

						frequency = (int)f;
						break;
					}
					Console.Clear();
					Train train = CreateTrain(accuracy, frequency, type);
					Console.WriteLine($"Successfully created train: {train})");
					Trains.Add(train);
					return true;

				case 3: // List Trains
					if (Trains.Count == 0)
					{
						Console.WriteLine("There are no trains yet!");
					}
					foreach (var item in Trains)
					{
						Console.WriteLine(item.ToString());
					}
					return true;

				case 2: // Remove Train
					if (Trains.Count == 0)
					{
						Console.WriteLine("There are no trains yet!");
						return true;
					}
					List<string> options_3 = new();
					
					foreach (Train t in Trains)
						options_3.Add(t.ToString());

					Menu menu_3 = new(options_3);
					int train_index = menu_3.Step() - 1;
					if (train_index < 0)
						return false;

					Console.Write("Successfully deleted train: ");
					Console.WriteLine(Trains[train_index]);

					Trains.RemoveAt(train_index);

					return true;

				case 4: // Run simulation
					Menu simulationMenu = new(["Step one day"]);
					// TODO
					return false;


				default:
					return false;
			}
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
