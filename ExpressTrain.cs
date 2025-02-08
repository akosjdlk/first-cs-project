using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_cs_project
{
	internal class ExpressTrain : Train
	{
		public ExpressTrain(int capacity, Schedule schedule, List<Passenger>? initialContents = null) : base(capacity, schedule, initialContents?.ConvertAll(m => (IMovable)m)) { }

		protected override bool LoadCheck(IMovable movable)
		{
			return movable is Passenger passenger && passenger.HasTicket;
		}

		public override string ToString()
		{
			return "Express" + base.ToString();
		}
	}
}
