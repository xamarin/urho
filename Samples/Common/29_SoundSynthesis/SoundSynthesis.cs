using System;
using Urho;

public class _29_SoundSynthesis : Sample
{
	/// Scene node for the sound component.
	Node node;
	/// Sound stream that we update.
	BufferedSoundStream soundStream;
	/// Instruction text.
	Text instructionText;
	/// Filter coefficient for the sound.
	float filter;
	/// Synthesis accumulator.
	float accumulator;
	/// First oscillator.
	float osc1;
	/// Second oscillator.
	float osc2;


	public _29_SoundSynthesis(Context ctx) : base(ctx)
	{
		filter = 0.5f;
		accumulator = 0f;
		osc1 = 0f;
		osc2 = 180f;
	}

	public override void Start()
	{
		base.Start();
		CreateSound();
		CreateInstructions();
	}
	
	protected override void OnUpdate(float timeStep)
	{
		// Use keys to control the filter constant
		Input input = Input;
		if (input.GetKeyDown(Key.Up))
			filter += timeStep * 0.5f;
		if (input.GetKeyDown(Key.Down))
			filter -= timeStep * 0.5f;
		filter = Clamp(filter, 0.01f, 1.0f);

		instructionText.Value = "Use cursor up and down to control sound filtering\nCoefficient: " + filter;

		UpdateSound();
	}

	private void CreateInstructions()
	{
		var cache = ResourceCache;
		UI ui = UI;

		// Construct new Text object, set string to display and font to use
		instructionText = new Text(Context);
		instructionText.Value = ("Use arrow up and down to control sound filtering");
		instructionText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 15);

		// Position the text relative to the screen center
		instructionText.TextAlignment = HorizontalAlignment.HA_CENTER;
		instructionText.HorizontalAlignment = HorizontalAlignment.HA_CENTER;
		instructionText.VerticalAlignment = VerticalAlignment.VA_CENTER;
		instructionText.SetPosition(0, ui.Root.Height/4);

		ui.Root.AddChild(instructionText);
	}



	private void CreateSound()
	{
		// Sound source needs a node so that it is considered enabled
		node = new Node(Context);
		SoundSource source = node.CreateComponent<SoundSource>();

		soundStream = new BufferedSoundStream();
		// Set format: 44100 Hz, sixteen bit, mono
		soundStream.SetFormat(44100, true, false);

		// Start playback. We don't have data in the stream yet, but the SoundSource will wait until there is data,
		// as the stream is by default in the "don't stop at end" mode
		source.Play(soundStream);
	}

	private void UpdateSound()
	{
		// Try to keep 1/10 seconds of sound in the buffer, to avoid both dropouts and unnecessary latency
		float targetLength = 1.0f / 10.0f;
		float requiredLength = targetLength - soundStream.BufferLength;
		if (requiredLength < 0.0f)
			return;

		uint numSamples = (uint)(soundStream.Frequency * requiredLength);
		if (numSamples == 0)
			return;

		// Allocate a new buffer and fill it with a simple two-oscillator algorithm. The sound is over-amplified
		// (distorted), clamped to the 16-bit range, and finally lowpass-filtered according to the coefficient
		var newData = new short[numSamples];
		for (int i = 0; i < numSamples; ++i)
		{
			osc1 = osc1 + 1.0f % 360.0f;
			osc2 = osc2 + 1.002f % 360.0f;

			float newValue = Clamp((float) ((Math.Sin(osc1) + Math.Sin(osc2)) * 100000.0f), -32767.0f, 32767.0f);
			accumulator = Lerp(accumulator, newValue, filter);
			newData[i] = (short)accumulator;
		}

		// Queue buffer to the stream for playback
		soundStream.AddData(newData, 0, newData.Length);
	}

	private float Lerp(float lhs, float rhs, float t) //TODO: move to MathHleper
	{
		return lhs*(1.0f - t) + rhs*t;
	}

}
