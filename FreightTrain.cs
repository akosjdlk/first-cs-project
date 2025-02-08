using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_cs_project
{
	internal class FreightTrain : Train
	{
		public FreightTrain(int capacity, Schedule schedule, List<Cargo>? initialContents = null) : base(capacity, schedule, initialContents?.ConvertAll(m => (IMovable)m)) { }

		protected override bool LoadCheck(IMovable movable)
		{
			return movable is Cargo;
		}

		public override string ToString()
		{
			return "Freight" + base.ToString();
		}
	}
}
