using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace first_cs_project
{
    internal abstract class Train
    {
		protected readonly int _maxCapacity;
		public Schedule Schedule { get; }

		public List<IMovable> Contents { get; } = new List<IMovable>();
		protected abstract bool LoadCheck(IMovable movable);

		protected Train(int capacity, Schedule schedule, List<IMovable>? initialContents = null)
		{
			_maxCapacity = capacity;
			Schedule = schedule;

			if (initialContents != null)
			{
				foreach (var item in initialContents)
					Load(item);
			}
		}

		public override string ToString()
		{
			return $"Train(capacity={_maxCapacity-GetRemainingCapacity()}/{_maxCapacity}, accuracy={Schedule.Accuracy}, frequency={Schedule.Frequency})";
		}

		protected int GetRemainingCapacity()
		{
			int remaining = _maxCapacity;
			foreach (var item in Contents)
				remaining -= item.UsedSpace;

			return remaining;
		}

		public int Load(IMovable movable)
		{
			if (!LoadCheck(movable)) {
				return -1;
			}

			if (GetRemainingCapacity() >= movable.UsedSpace)
			{
				Contents.Add(movable);
				return Contents.Count-1; // current index
			}

			return -1;
		}

		public bool UnLoad(IMovable? movable, int? index)
		{
			if (movable == null && index == null)
			{
				throw new Exception("Either 'movable' or 'index' must be specified.");
			}

			if (movable != null && index != null)
			{
				throw new WarningException("'movable' and 'index' are mutually exclusive. 'movable' will take precedence.");
			}


			if (movable != null)
			{
				bool success;
				success = Contents.Remove(movable);

				if (success)
				{
					return true;

				}
			}
			
			if (index != null)
			{
				try
				{
					Contents.RemoveAt(index.Value);
				} catch (ArgumentOutOfRangeException)
				{
					return false;
				}

				return true;
			}

			return false;

		}

	}
}
