using System;
using System.Threading;
using System.Threading.Tasks;

namespace Urho
{
	public class GameTimer
	{
		private float interval;
		private readonly CancellationToken cancellationToken;
		private TaskCompletionSource<bool> TaskSource => new TaskCompletionSource<bool>(); 

		private GameTimer(float interval, CancellationToken cancellationToken)
		{
			this.interval = interval;
			this.cancellationToken = cancellationToken;
			Application.Update += HandleUpdate;
		}

		public static Task Delay(float intervalMs, CancellationToken cancellationToken = default(CancellationToken))
		{
			var timer = new GameTimer(intervalMs, cancellationToken);
			return timer.TaskSource.Task;
		}

		public static Task Delay(TimeSpan interval, CancellationToken cancellationToken = default(CancellationToken))
		{
			var timer = new GameTimer((float) interval.TotalMilliseconds, cancellationToken);
			return timer.TaskSource.Task;
		}

		internal void HandleUpdate(UpdateEventArgs args)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				TaskSource.SetCanceled();
			}
			else
			{
				interval -= args.TimeStep;
				if (interval <= 0)
				{
					Application.Update -= HandleUpdate;
					TaskSource.TrySetResult(true);
				}
			}
		}
	}
}
