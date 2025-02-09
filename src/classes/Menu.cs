
namespace first_cs_project.src.classes
{
	internal class Menu
	{
		public readonly List<string> _menuOptions;
		private int _selectedIndex = 0;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
			private set
			{
				if (value < 0)
				{
					_selectedIndex = _menuOptions.Count - 1;

				} else if (value > _menuOptions.Count - 1)
				{
					_selectedIndex = 0;

				} else
				{
					_selectedIndex = value;
				}
				WriteOptions();
			}
		}

		public Menu(List<string> menuOptions, string backOptionName = "Back")
		{
			_menuOptions = menuOptions;
			_menuOptions.Insert(0, backOptionName);
		}

		static void Write(string? value) => Console.WriteLine(value);

		public int Call()
		{
			WriteOptions();
			return ListenForInput();
		}
		public void WriteOptions()
		{
			Console.Clear();
			Console.WriteLine("--------------------");
			for (int i = 0;i < _menuOptions.Count;i++)
			{
				if (i == _selectedIndex)
					WriteHighlighted($"{i}. {_menuOptions[i]}");
				else
					Write($"{i}. {_menuOptions[i]}");
			}
			Console.WriteLine("--------------------");
		}

		static void WriteHighlighted(string s)
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine(s);
			Console.ResetColor();
		}

		public static string TextInput(string? prompt)
		{
			Console.Clear();
			Console.Write(prompt);
			string input = Console.ReadLine()!;
			return input;
		}

		public static void WaitForEnter(string? prompt)
		{
			Console.WriteLine("\n");
			Console.Write(prompt);
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
		}

		public static int? NumericInput(string prompt, bool allowNegative = false, bool allowZero = true)
		{
			Console.Clear();
			Console.Write(prompt);
			string number = "";
			while (true)
			{
				string key = Console.ReadKey(true).Key.ToString();

				if (key == "Escape")
					return null;

				if (key == "Enter" && number != "" && number != "-")
					break;

				if (key == "Backspace" && number != "")
				{
					number = number.Remove(number.Length - 1);
					Console.Clear();
					Console.Write(prompt + number);
					continue;
				}

				if (allowNegative && key == "-" && number == "")
				{
					number += "-";
					Console.Write(key);
					continue;
				}

				if (int.TryParse(key.AsSpan(1), out int num))
				{
					if (!allowZero && num == 0 && (number == "-" || number == ""))
						continue;

					number += num.ToString();
					Console.Write(num);
				}
			}

			return int.Parse(number);
		}

		public int ListenForInput()
		{
			while (true)
			{
				string key = Console.ReadKey(true).Key.ToString();
				if (int.TryParse(key.AsSpan(1), out int num))
				{
					if (num == 0)
						return 0;

					
					if (0 <= num && num <= _menuOptions.Count - 1 && _menuOptions.Count - 1 <= 9)
						return num;

					continue;
				}

				switch (key.ToString())
				{
					case "DownArrow":
						SelectedIndex++;
						break;

					case "UpArrow":
						SelectedIndex--;
						break;

					case "Escape":
						return 0;

					case "Enter":
						return SelectedIndex;

					default:
						continue;
				}
			}
		}

	}
}
