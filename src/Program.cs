

using first_cs_project.src.classes;

namespace first_cs_project
{
	internal partial class Program
    {
		
		static int currentDay = 1;

		static readonly List<Log> Logs = [];

        static void Main()
        {
			Menu menu = new(["Create Train", "Delete Train", "List Trains", "Start simulation"], "Exit");
			bool shouldExit = false;
			while (!shouldExit)
			{
				int option = menu.Call();
				if (option == 0)
					break;

				bool wroteToStdout = ProcessOption(option);

				if (wroteToStdout)
					Menu.WaitForEnter("Press Enter to continue. . .");
			}
			
		}

		static bool ProcessOption(int _option)
		{
			return _option switch
			{
				1 => AddTrain(),
				3 => ListTrains(),
				2 => RemoveTrain(),
				4 => RunSimulation(),
				_ => false,
			};
		}

		static bool RunSimulation()
		{
			if (Trains.Count == 0)
			{
				Console.WriteLine("There are no trains yet!");
				return true;
			}

			while (true)
			{
				Menu menu = new(["Step one day", "Step X days", "Show previous day logs", "Browse Logs"]);
				int choice = menu.Call();

				if (choice == 0)
					return false;

				if (ProcessSimulation(choice))
					Menu.WaitForEnter("Press Enter to continue. . .");
			}
		}
    }
}
