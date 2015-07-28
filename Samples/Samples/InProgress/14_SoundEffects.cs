using Urho;

class _14_SoundEffects : Sample
{
    private Scene scene;

    // Custom variable identifier for storing sound effect name within the UI element
    // const StringHash VAR_SOUNDRESOURCE("SoundResource");

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
        button.SetStyleAuto(null);
        button.SetPosition(x, y);
        button.SetSize(xSize, ySize);
        root.AddChild(button);

        Text buttonText = new Text(Context);
        buttonText.SetAlignment(HorizontalAlignment.HA_CENTER, VerticalAlignment.VA_CENTER);
        buttonText.SetFont(font, 12);
        button.AddChild(buttonText);
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
        sliderText.SetPosition(x, y);
        sliderText.SetFont(font, 12);
        sliderText.Value = text;
        root.AddChild(sliderText);

        Slider slider = new Slider(Context);
        slider.SetStyleAuto(null);
        slider.SetPosition(x, y + 20);
        slider.SetSize(xSize, ySize);
        // Use 0-1 range for controlling sound/music master volume
        slider.Range=1.0f;
        root.AddChild(slider);
    
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

#warning MISSING_API //enum?
        const string SOUND_EFFECT = "Effect";
        const string SOUND_MUSIC = "Music";

        
        // Create buttons for playing back sounds
        for (int i = 0; i < soundNames.Length; ++i)
        {
            Button b = CreateButton(i * 140 + 20, 20, 120, 40, soundNames[i]);
            // Store the sound effect resource name as a custom variable into the button
#warning MISSING_API
            ////b.SetVar(VAR_SOUNDRESOURCE, soundResourceNames[i]);
            
            var j = i; //avoid closure
            SubscribeToPressed(args =>
                {
                    if (args.Element != b) 
                        return;

                    // Get the sound resource
                    Sound sound = cache.GetSound(soundResourceNames[j]);

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
                });
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
                    musicSource.SetSoundType(SOUND_MUSIC);
                    musicSource.Play(music);
                }
                else if (args.Element == stopMusicButton)
                {
                    scene.RemoveChild(scene.GetChild("Music", false));
                }
            });

        Audio audio = Audio;


        // Create sliders for controlling sound and music master volume
        var soundSlider = CreateSlider(20, 140, 200, 20, "Sound Volume");
        soundSlider.Value=audio.GetMasterGain(SOUND_EFFECT);
        
        var musicSlider = CreateSlider(20, 200, 200, 20, "Music Volume");
        musicSlider.Value=audio.GetMasterGain(SOUND_MUSIC);
        
        SubscribeToSliderChanged(args =>
        {
            if (args.Element == soundSlider)
            {
                float newVolume = args.Value;
                Audio.SetMasterGain(SOUND_EFFECT, newVolume);
            }
            else if (args.Element == musicSlider)
            {
                float newVolume = args.Value;
                Audio.SetMasterGain(SOUND_MUSIC, newVolume);
            }
        });
    }
}
