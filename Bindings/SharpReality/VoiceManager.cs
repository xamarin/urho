using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;

namespace Urho
{
	public class VoiceManager
	{
		Dictionary<string, Action> cortanaCommands = null;

		public SpeechRecognizer SpeechRecognizer { get; private set; }
		public SpeechSynthesizer Synthesizer { get; private set; } 

		public async Task TextToSpeech(string text)
		{
			if (string.IsNullOrEmpty(text))
				return;

			if (Synthesizer == null)
				Synthesizer = new SpeechSynthesizer();

			var tcs = new TaskCompletionSource<bool>();
			var stream = await Synthesizer.SynthesizeTextToStreamAsync(text);
			var player = BackgroundMediaPlayer.Current;
			TypedEventHandler<MediaPlayer, object> mediaEndedHandler = null;
			mediaEndedHandler = (s, e) =>
				{
					tcs.TrySetResult(true);
					//subscribe once.
					player.MediaEnded -= mediaEndedHandler;
				};
			player.SetStreamSource(stream);
			player.MediaEnded += mediaEndedHandler;
			player.Play();
			await tcs.Task;
		}

		public async Task<bool> RegisterCortanaCommands(Dictionary<string, Action> commands)
		{
			try
			{
				cortanaCommands = commands;
				SpeechRecognizer = new SpeechRecognizer();
				var constraint = new SpeechRecognitionListConstraint(cortanaCommands.Keys);
				SpeechRecognizer.Constraints.Clear();
				SpeechRecognizer.Constraints.Add(constraint);
				var result = await SpeechRecognizer.CompileConstraintsAsync();
				if (result.Status == SpeechRecognitionResultStatus.Success)
				{
					SpeechRecognizer.ContinuousRecognitionSession.StartAsync();
					SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated += (s, e) =>
					{
						if (e.Result.RawConfidence >= 0.5f)
						{
							Action handler;
							if (cortanaCommands.TryGetValue(e.Result.Text, out handler))
								Application.InvokeOnMain(handler);
						}
					};
					return true;
				}
				return false;
			}
			catch (Exception exc)
			{
				LogSharp.Warn("RegisterCortanaCommands: " + exc);
				return false;
			}
		}
	}
}
