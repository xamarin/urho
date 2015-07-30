using System.Collections.Generic;
using System.Linq;
using Urho;

class _14_SoundEffects : Sample
{
    private Scene scene;

    readonly string[] soundNames = {
        "Fist",
        "Explosion",
        "Power-up"
    };

    readonly string[] soundResourceNames = {
        "Sounds/PlayerFistHit.wav",
        "Sounds/BigExplosion.wav",
        "Sounds/Powerup.wav"
    };

    public _14_SoundEffects(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        Input.SetMouseVisible(true, false);
        CreateUI();
    }

    Button CreateButton(int x, int y, int xSize, int ySize, string text)
    {
        UIElement root = UI.Root;
        var cache = ResourceCache;
        Font font = cache.GetFont("Fonts/Anonymous Pro.ttf");

        // Create the button and center the text onto it
        Button button = new Button(Context);
        root.AddChild(button);
        button.SetStyleAuto(null);
        button.SetPosition(x, y);
        button.SetSize(xSize, ySize);

        Text buttonText = new Text(Context);
        button.AddChild(buttonText);
        buttonText.SetAlignment(HorizontalAlignment.HA_CENTER, VerticalAlignment.VA_CENTER);
        buttonText.SetFont(font, 12);
        buttonText.Value = text;

        return button;
    }

    Slider CreateSlider(int x, int y, int xSize, int ySize, string text)
    {
        UIElement root = UI.Root;
        ResourceCache cache = ResourceCache;
        Font font = cache.GetFont("Fonts/Anonymous Pro.ttf");
        // Create text and slider below it
        Text sliderText = new Text(Context);
        root.AddChild(sliderText);
        sliderText.SetPosition(x, y);
        sliderText.SetFont(font, 12);
        sliderText.Value = text;

        Slider slider = new Slider(Context);
        root.AddChild(slider);
        slider.SetStyleAuto(null);
        slider.SetPosition(x, y + 20);
        slider.SetSize(xSize, ySize);
        // Use 0-1 range for controlling sound/music master volume
        slider.Range = 1.0f;

        return slider;
    }

    private void CreateUI()
    {
        var cache = ResourceCache;
        scene = new Scene(Context);
        // Create a scene which will not be actually rendered, but is used to hold SoundSource components while they play sounds

        UIElement root = UI.Root;
        XMLFile uiStyle = cache.GetXmlFile("UI/DefaultStyle.xml");
        // Set style to the UI root so that elements will inherit it
        root.SetDefaultStyle(uiStyle);

        // Create buttons for playing back sounds
        List<Button> soundButtons = new List<Button>();
        for (int i = 0; i < soundNames.Length; ++i)
        {
            Button b = CreateButton(i * 140 + 20, 20, 120, 40, soundNames[i]);
            soundButtons.Add(b);
        }
        
        // Create buttons for playing/stopping music
        var playMusicButton = CreateButton(20, 80, 120, 40, "Play Music");
        var stopMusicButton = CreateButton(160, 80, 120, 40, "Stop Music");
        SubscribeToReleased(args =>
            {
                if (args.Element == playMusicButton)
                {
                    if (scene.GetChild("Music", false) != null)
                        return;

                    var music = cache.GetSound("Music/Ninja Gods.ogg");
                    music.SetLooped(true);
                    Node musicNode = scene.CreateChild("Music");
                    SoundSource musicSource = musicNode.CreateComponent<SoundSource>();
                    // Set the sound type to music so that master volume control works correctly
                    musicSource.SetSoundType(SoundType.Music.ToString());
                    musicSource.Play(music);
                }
                else if (args.Element == stopMusicButton)
                {
                    scene.RemoveChild(scene.GetChild("Music", false));
                }
                else
                {
                    var button = soundButtons.FirstOrDefault(b => b == args.Element);
                    if (button == null)
                        return;

                    // Get the sound resource
                    Sound sound = cache.GetSound(soundResourceNames[soundButtons.IndexOf(button)]);

                    if (sound != null)
                    {
                        // Create a scene node with a SoundSource component for playing the sound. The SoundSource component plays
                        // non-positional audio, so its 3D position in the scene does not matter. For positional sounds the
                        // SoundSource3D component would be used instead
                        Node soundNode = scene.CreateChild("Sound");
                        SoundSource soundSource = soundNode.CreateComponent<SoundSource>();
                        soundSource.Play(sound);
                        // In case we also play music, set the sound volume below maximum so that we don't clip the output
                        soundSource.Gain = 0.75f;
                        // Set the sound component to automatically remove its scene node from the scene when the sound is done playing
                        soundSource.AutoRemove = true;
                    }
                }
            });

        Audio audio = Audio;

        // Create sliders for controlling sound and music master volume
        var soundSlider = CreateSlider(20, 140, 200, 20, "Sound Volume");
        soundSlider.Value = audio.GetMasterGain(SoundType.Effect.ToString());

        var musicSlider = CreateSlider(20, 200, 200, 20, "Music Volume");
        musicSlider.Value = audio.GetMasterGain(SoundType.Music.ToString());

        SubscribeToSliderChanged(args =>
            {
                if (args.Element == soundSlider)
                {
                    float newVolume = args.Value;
                    Audio.SetMasterGain(SoundType.Effect.ToString(), newVolume);
                }
                else if (args.Element == musicSlider)
                {
                    float newVolume = args.Value;
                    Audio.SetMasterGain(SoundType.Music.ToString(), newVolume);
                }
            });
    }
}
