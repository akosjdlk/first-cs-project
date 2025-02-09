using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_cs_project.src.classes
{
	internal interface IMovable
	{
		int UsedSpace { get; }
		string Id { get; }
	}

	internal class Passenger(string name, bool hasTicket = false) : IMovable
	{
		public string Name { get; } = name;
		public string Id { get; } = Guid.NewGuid().ToString().Split("-")[0];
		public int UsedSpace { get; } = 1;
		public bool HasTicket { get; set; } = hasTicket;
	}

	internal class Cargo(int usedSpace, string? id = null) : IMovable
	{
		public string Id { get; } = id == null ? Guid.NewGuid().ToString().Split("-")[0] : id;
		public int UsedSpace { get; } = usedSpace;
	}
}
