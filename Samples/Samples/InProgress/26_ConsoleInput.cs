using Urho;

class _26_ConsoleInput : Sample
{
    /// Game on flag.
    bool gameOn;
    /// Food dispensed flag.
    bool foodAvailable;
    /// Whether ate on the previous turn.
    bool eatenLastTurn;
    /// Number of turns survived.
    int numTurns;
    /// Player's hunger level.
    int hunger;
    /// Threat of Urho level.
    int urhoThreat;

    private string[] hungerLevels =
        {
            "bursting",
            "well-fed",
            "fed",
            "hungry",
            "very hungry",
            "starving"
        };

    // Urho threat level descriptions
    private string[] urhoThreatLevels =
        {
            "Suddenly Urho appears from a dark corner of the fish tank",
            "Urho seems to have his eyes set on you",
            "Urho is homing in on you mercilessly"
        };

    public _26_ConsoleInput(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        SubscribeToConsoleCommand(HandleConsoleCommand);
        SubscribeToUpdate(HandleUpdate);
        SubscribeToKeyDown(HandleEscKeyDown);
        IsLogoVisible = false;

        // Show the console by default, make it large. Console will show the text edit field when there is at least one
        // subscriber for the console command event
        var console = Console;
        console.NumRows = (uint) (Graphics.Height / 16);
        console.NumBufferedRows = 2 * console.NumRows;
        console.CommandInterpreter = GetType().Name;
        console.SetVisible(true);
        console.CloseButton.SetVisible(false);

        // Show OS mouse cursor
        Input.SetMouseVisible(true, false);

        // Open the operating system console window (for stdin / stdout) if not open yet
#warning MISSING_API OpenConsoleWindow
        ////OpenConsoleWindow();

        // Initialize game and print the welcome message
        StartGame();

        // Randomize from system clock
        //// SetRandomSeed(Time::GetSystemTime());
    }

    private void HandleConsoleCommand(ConsoleCommandEventArgs args)
    {
        if (args.Id == GetType().Name)
            HandleInput(args.Command);
    }

    private void HandleUpdate(UpdateEventArgs args)
    {
        // Check if there is input from stdin


#warning MISSING_API GetConsoleInput
        ////string input = GetConsoleInput();
        ////if (!string.IsNullOrEmpty(input))
        ////    HandleInput(input);
    }

    private void HandleEscKeyDown(KeyDownEventArgs args)
    {
        // Unlike the other samples, exiting the engine when ESC is pressed instead of just closing the console
        if (args.Key == Key.Esc)
            Engine.Exit();
    }

    private void StartGame()
    {
        Print("Welcome to the Urho adventure game! You are the newest fish in the tank; your\n" +
              "objective is to survive as long as possible. Beware of hunger and the merciless\n" +
              "predator cichlid Urho, who appears from time to time. Evading Urho is easier\n" +
              "with an empty stomach. Type 'help' for available commands.");

        gameOn = true;
        foodAvailable = false;
        eatenLastTurn = false;
        numTurns = 0;
        hunger = 2;
        urhoThreat = 0;
    }

    private void EndGame(string message)
    {
        Print(message);
        Print("Game over! You survived " + numTurns + " turns.\n" +
              "Do you want to play again (Y/N)?");

        gameOn = false;
    }

    private void Advance()
    {
        if (urhoThreat > 0)
        {
            ++urhoThreat;
            if (urhoThreat > 3)
            {
                EndGame("Urho has eaten you!");
                return;
            }
        }
        else if (urhoThreat < 0)
            ++urhoThreat;
        if (urhoThreat == 0 && NextRandom() < 0.2f)
            ++urhoThreat;

        if (urhoThreat > 0)
            Print(urhoThreatLevels[urhoThreat - 1] + ".");

        if ((numTurns & 3) == 0 && !eatenLastTurn)
        {
            ++hunger;
            if (hunger > 5)
            {
                EndGame("You have died from starvation!");
                return;
            }
            else
                Print("You are " + hungerLevels[hunger] + ".");
        }

        eatenLastTurn = false;

        if (foodAvailable)
        {
            Print("The floating pieces of fish food are quickly eaten by other fish in the tank.");
            foodAvailable = false;
        }
        else if (NextRandom() < 0.15f)
        {
            Print("The overhead dispenser drops pieces of delicious fish food to the water!");
            foodAvailable = true;
        }

        ++numTurns;
    }

    private void HandleInput(string input)
    {
        string inputLower = input.ToLower().Trim();
        if (string.IsNullOrEmpty(inputLower))
        {
            Print("Empty input given!");
            return;
        }

        if (inputLower == "quit" || inputLower == "exit")
            Engine.Exit();
        else if (gameOn)
        {
            // Game is on
            if (inputLower == "help")
                Print("The following commands are available: 'eat', 'hide', 'wait', 'score', 'quit'.");
            else if (inputLower == "score")
                Print("You have survived " + numTurns + " turns.");
            else if (inputLower == "eat")
            {
                if (foodAvailable)
                {
                    Print("You eat several pieces of fish food.");
                    foodAvailable = false;
                    eatenLastTurn = true;
                    hunger -= (hunger > 3) ? 2 : 1;
                    if (hunger < 0)
                    {
                        EndGame("You have killed yourself by over-eating!");
                        return;
                    }
                    else
                        Print("You are now " + hungerLevels[hunger] + ".");
                }
                else
                    Print("There is no food available.");

                Advance();
            }
            else if (inputLower == "wait")
            {
                Print("Time passes...");
                Advance();
            }
            else if (inputLower == "hide")
            {
                if (urhoThreat > 0)
                {
                    bool evadeSuccess = hunger > 2 || NextRandom() < 0.5f;
                    if (evadeSuccess)
                    {
                        Print("You hide behind the thick bottom vegetation, until Urho grows bored.");
                        urhoThreat = -2;
                    }
                    else
                        Print("Your movements are too slow; you are unable to hide from Urho.");
                }
                else
                    Print("There is nothing to hide from.");

                Advance();
            }
            else
                Print("Cannot understand the input '" + input + "'.");
        }
        else
        {
            // Game is over, wait for (y)es or (n)o reply
            if (inputLower[0] == 'y')
                StartGame();
            else if (inputLower[0] == 'n')
                Engine.Exit();
            else
                Print("Please answer 'y' or 'n'.");
        }
    }

    private void Print(string output)
    {
        // Logging appears both in the engine console and stdout
        Log.WriteRaw(output + "\n", false);
    }

}
