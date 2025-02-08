using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_cs_project
{
	internal interface IMovable
	{
		int UsedSpace { get; }
		string Name { get; }
	}

	internal class Passenger : IMovable
	{
		public string Name { get; }
		public int UsedSpace { get; } = 1;
		public bool HasTicket { get; set; }

		public Passenger(string name, bool hasTicket = false)
		{
			Name = name;
			HasTicket = hasTicket;
		}
	}

	internal class Cargo : IMovable
	{
		public string Name { get; }
		public int UsedSpace { get; }

		public Cargo(string name, int usedSpace)
		{
			Name = name;
			UsedSpace = usedSpace;
		}
	}
}
