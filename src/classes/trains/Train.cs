
using System.Diagnostics;

namespace first_cs_project.src.classes.trains
{
	internal abstract class Train
	{
		// Properties
		protected readonly int _maxCapacity;
		public Schedule Schedule { get; }
		public List<IMovable> Contents { get; private set;  } = [];
		public string Id { get; } = Guid.NewGuid().ToString().Split("-")[0];

		protected static readonly Random _r = new();
		protected int CurrentTick;
		protected int CurrentDelay;

		// Abstracts
		protected abstract bool LoadCheck(IMovable movable);

		public abstract void RandomizeContents();

		public abstract void Tick(Log log);

		// Methods
		protected Train(int capacity, Schedule schedule, List<IMovable>? initialContents = null)
		{
			_maxCapacity = capacity;
			Schedule = schedule;
			CurrentDelay = -Schedule.CalculateDelay();
			CurrentTick = CurrentDelay;

			if (initialContents != null)
			{
				foreach (var item in initialContents)
					Load(item);
			}
		}

		public override string ToString()
		{
			return $"Train(capacity={_maxCapacity}, accuracy={Schedule.Accuracy}, frequency={Schedule.Frequency})";
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
			if (!LoadCheck(movable))
			{
				return -1;
			}

			if (GetRemainingCapacity() >= movable.UsedSpace)
			{
				Contents.Add(movable);
				return Contents.Count - 1; // current index
			}

			return -1;
		}
		public bool UnLoad(IMovable? movable = null, int? index = null)
		{
			if (movable == null && index == null)
			{
				throw new Exception("Either 'movable' or 'index' must be specified.");
			}

			if (movable != null && index != null)
			{
				throw new Exception("'movable' and 'index' are mutually exclusive. 'movable' will take precedence.");
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
