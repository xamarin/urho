using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Urho;

class _16_Chat : Sample
{
    // Identifier for the chat network messages
    const int MSG_CHAT = 32;
    // UDP port we will use
    const short CHAT_SERVER_PORT = 2345;

    /// Strings printed so far.
    List<string> chatHistory = new List<string>();
    /// Chat text element.
    Text chatHistoryText;
    /// Button container element.
    UIElement buttonContainer;
    /// Server address / chat message line editor element.
    LineEdit textEdit;
    /// Send button.
    Button sendButton;
    /// Connect button.
    Button connectButton;
    /// Disconnect button.
    Button disconnectButton;
    /// Start server button.
    Button startServerButton;


    public _16_Chat(Context ctx) : base(ctx) { }

    public override void Start()
    {
        base.Start();
        Input.SetMouseVisible(true, false);
        CreateUI();
        SubscribeToEvents();
    }

    private void CreateUI()
    {
        IsLogoVisible = false; // We need the full rendering window

        var graphics = Graphics;
        UIElement root = UI.Root;
        var cache = ResourceCache;
        XMLFile uiStyle = cache.GetXmlFile("UI/DefaultStyle.xml");
        // Set style to the UI root so that elements will inherit it
        root.SetDefaultStyle(uiStyle);

        Font font = cache.GetFont("Fonts/Anonymous Pro.ttf");
        chatHistoryText = new Text(Context);
        chatHistoryText.SetFont(font, 12);
        root.AddChild(chatHistoryText);

        buttonContainer = new UIElement(Context);
        buttonContainer.SetFixedSize(graphics.Width, 20);
        buttonContainer.SetPosition(0, graphics.Height - 20);
        buttonContainer.LayoutMode=LayoutMode.LM_HORIZONTAL;
        root.AddChild(buttonContainer);

        textEdit = new LineEdit(Context); 
        textEdit.SetStyleAuto(null);
        textEdit.AddChild(buttonContainer);

        sendButton = CreateButton("Send", 70);
        connectButton = CreateButton("Connect", 90);
        disconnectButton = CreateButton("Disconnect", 100);
        startServerButton = CreateButton("Start Server", 110);

        UpdateButtons();

        int rowHeight = chatHistoryText.RowHeight;
        ////// Row height would be zero if the font failed to load
        ////if (rowHeight >= 0)
        ////    chatHistory_.Resize((graphics.Height - 20) / rowHeight);

        // No viewports or scene is defined. However, the default zone's fog color controls the fill color
        Renderer.DefaultZone.FogColor = new Color(0.0f, 0.0f, 0.1f);
    }

    private void SubscribeToEvents()
    {
        SubscribeToTextFinished(args => HandleSend());
        SubscribeToReleased(args =>
            {
                if (args.Element == sendButton)
                    HandleSend();
                if (args.Element == connectButton)
                    HandleConnect();
                if (args.Element == disconnectButton)
                    HandleDisconnect();
                if (args.Element == startServerButton)
                    HandleStartServer();
            });
        SubscribeToLogMessage(HandleLogMessage);
        SubscribeToNetworkMessage(HandleNetworkMessage);
        SubscribeToServerConnected(args => UpdateButtons());
        SubscribeToServerDisconnected(args => UpdateButtons());
        SubscribeToConnectFailed(args => UpdateButtons());
    }
    
    Button CreateButton(string text, int width)
    {
        var cache = ResourceCache;
        Font font = cache.GetFont("Fonts/Anonymous Pro.ttf");

        Button button = new Button(Context);
        button.SetStyleAuto(null);
        button.SetFixedWidth(width);
        buttonContainer.AddChild(button);
    
        var buttonText = new Text(Context);
        buttonText.SetFont(font, 12);
        buttonText.SetAlignment(HorizontalAlignment.HA_CENTER, VerticalAlignment.VA_CENTER);
        button.AddChild(buttonText);

        buttonText.Value = text;
    
        return button;
    }

    private void ShowChatText(string row)
    {
        chatHistory.RemoveAt(0);
        chatHistory.Add(row);

        // Concatenate all the rows in history
        string allRows = String.Empty;
        for (int i = 0; i < chatHistory.Count; ++i)
            allRows += chatHistory[i] + "\n";

        ////chatHistoryText_.Text = allRows;
    }

    private void UpdateButtons()
    {
        Network network = Network;
        Connection serverConnection = network.ServerConnection;
        bool serverRunning = network.IsServerRunning();
    
        // Show and hide buttons so that eg. Connect and Disconnect are never shown at the same time
        sendButton.SetVisible(serverConnection != null);
        connectButton.SetVisible(serverConnection == null && !serverRunning);
        disconnectButton.SetVisible(serverConnection != null || serverRunning);
        startServerButton.SetVisible(serverConnection == null && !serverRunning);
    }

    private void HandleLogMessage(LogMessageEventArgs args)
    {
        ShowChatText(args.Message);
    }

    private void HandleSend()
    {
        string text = textEdit.Text;
        if (string.IsNullOrEmpty(text))
            return; // Do not send an empty message
    
        Network network = Network;
        Connection serverConnection = network.ServerConnection;
    
        if (serverConnection != null)
        {
            // A VectorBuffer object is convenient for constructing a message to send

#warning MISSING_API VectorBuffer
            ////VectorBuffer msg;
            ////msg.WriteString(text);
            ////// Send the chat message as in-order and reliable
            ////serverConnection.SendMessage(MSG_CHAT, true, true, msg);
            // Empty the text edit after sending
            textEdit.Text=String.Empty;
        }
    }

    private void HandleConnect()
    {
        Network network = Network;
        string address = textEdit.Text.Trim();
        if (string.IsNullOrEmpty(address))
            address = "localhost"; // Use localhost to connect if nothing else specified
        // Empty the text edit after reading the address to connect to
        textEdit.Text= string.Empty;

        // Connect to server, do not specify a client scene as we are not using scene replication, just messages.
        // At connect time we could also send identity parameters (such as username) in a VariantMap, but in this
        // case we skip it for simplicity
        network.Connect(address, CHAT_SERVER_PORT, null);
    
        UpdateButtons();
    }

    private void HandleDisconnect()
    {
        Network network = Network;
        Connection serverConnection = network.ServerConnection;
        // If we were connected to server, disconnect
        if (serverConnection != null)
            serverConnection.Disconnect(0);
        // Or if we were running a server, stop it
        else if (network.IsServerRunning())
            network.StopServer();
    
        UpdateButtons();
    }

    private void HandleStartServer()
    {
        Network network = Network;
        network.StartServer(CHAT_SERVER_PORT);
    
        UpdateButtons();
    }

    private void HandleNetworkMessage(NetworkMessageEventArgs args)
    {
        Network network = Network;
    
        int msgID = args.MessageID;
        if (msgID == MSG_CHAT)
        {
#warning MISSING_API MemoryBuffer (MemoryStream?)
            ////// Use a MemoryBuffer to read the message data so that there is no unnecessary copying
            ////MemoryBuffer msg = new MemoryBuffer(args.Data);
            ////string text = msg.ReadString();
            Marshal.PtrToStringUni(args.Data, 100);//???

            ////// If we are the server, prepend the sender's IP address and port and echo to everyone
            ////// If we are a client, just display the message
            ////if (network.IsServerRunning())
            ////{
            ////    Connection sender = args.Connection;

            ////    text = sender.ToString() + " " + text;

            ////    VectorBuffer sendMsg;
            ////    sendMsg.WriteString(text);
            ////    // Broadcast as in-order and reliable
            ////    network.BroadcastMessage(MSG_CHAT, true, true, sendMsg);
            ////}

            ////ShowChatText(text);
        }
    }

}
