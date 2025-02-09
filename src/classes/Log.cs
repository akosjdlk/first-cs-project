
namespace first_cs_project.src.classes
{
	internal class Log(string name)
	{
		public string Name { get; private set; } = name;

		private readonly List<string> _events = [];
		public IReadOnlyList<string> Events => _events;

		private bool _isSealed = false;

		public string Id = Guid.NewGuid().ToString().Split("-")[0];

		public void Seal()
		{
			if (!_isSealed)
			{
				_isSealed = true;
			}
		}

		public void AddEvent(string log)
		{
			if (!_isSealed)
			{
				_events.Add(log);
			} else
			{
				throw new Exception("This Log is already sealed and cannot be modified.");
			}
		}

	}
}
