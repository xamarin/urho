open System
open System.Xml
open System.Xml.XPath
open System.IO
open System.Xml.Linq
open System.Text
open System.Globalization
open System.Collections.Generic

let load path =
  let f = File.OpenText path
  let doc = XDocument.Load f
  f.Close()
  doc

let xp s =
  XElement.Parse (s)

let events = new System.Collections.Generic.Dictionary<string,(string*string)>()
let save path (doc:XDocument) =
  let settings = XmlWriterSettings ()
  settings.Indent <- true
  settings.Encoding <- new UTF8Encoding (false)
  settings.OmitXmlDeclaration <- true
  settings.NewLineChars <- Environment.NewLine
  let output = File.CreateText (path)
  let writer = XmlWriter.Create (output, settings)
  doc.Save (writer)
  writer.Close ()
  output.WriteLine ()
  output.Close ()
  
let select (node:XNode) path = node.XPathSelectElement path
let setval (node:XNode) path value = (select node path).Value <- value
let xname s = XName.Get(s)
let membername (x:XElement) =
  let mn = x.Attribute (xname "MemberName")
  mn.Value

let getTypeName (doc:XDocument) =
  let tnode = doc.XPathSelectElement ("Type")
  let attr = tnode.Attribute (xname "Name")
  attr.Value

let processEventArgs (doc:XDocument) =
  let typeName = getTypeName doc
  let tdocs = select doc "Type/Docs"
  match events.ContainsKey (typeName) with
    | false -> ()
    | true ->
      let vals = events.[typeName]
      setval tdocs "summary" <| sprintf "Event arguments for the %s's %s event" (snd vals) (fst vals) 
  doc

let processType (doc:XDocument) =
  let typeName = getTypeName doc
    
  let fillBaseType() =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='BaseType']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's type system base type."
        setval mdoc "value" "StringHash representing the base type for this Urho type."
        setval mdoc "remarks" "This returns the Urho type system base type and is surfaced for low-level Urho code."
  let fillType() =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='Type']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's type system type."
        try
          setval mdoc "value" "StringHash representing the type for this C# type."
        with
          | ex -> ()
        setval mdoc "remarks" "This returns the Urho's type and is surfaced for low-level Urho code."
  let fillTypeName() = 
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='TypeName']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's low-level type name."
        setval mdoc "value" "Stringified low-level type name."
        setval mdoc "remarks" ""
  let fillTypeNameStatic() =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='TypeNameStatic']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's low-level type name, accessible as a static method."
        setval mdoc "value" "Stringified low-level type name."
        setval mdoc "remarks" ""

  let fillTypeCtor() =
    for x in doc.XPathSelectElements "Type/Members/Member[@MemberName='.ctor']" do
      match x with
      | null -> ()
      | mem ->
        match mem.XPathSelectElement "Parameters/Parameter[@Name='handle']" with
          | null -> ()
          | hmember ->
            let mdoc = mem.XPathSelectElement "Docs"
            setval mdoc "param[@name='handle']" "Pointer to the raw unmanaged Urho object."
            setval mdoc "summary" <| (sprintf "Constructs a new instance of %s, given a raw pointer to an unmanaged object" typeName)
            let remarks = select mdoc "remarks"
            remarks.RemoveAll ()
            XElement.Parse ("<para>This creates a new managed wrapper for the type using the raw pointer to an unmanaged object.</para>") |> remarks.Add
            XElement.Parse ("<para>Objects that are created in this fashion get registered with the UrhoSharp runtime.</para>") |> remarks.Add
            XElement.Parse ("<para>This is intended to be used by the UrhoSharp runtime, and is not intended to be used by users.</para>") |> remarks.Add

  let fillTypeEmpty() =
    for x in doc.XPathSelectElements "Type/Members/Member[@MemberName='.ctor']" do
      match x with
      | null -> ()
      | mem ->
        match mem.XPathSelectElement "Parameters/Parameter[@Name='emptyFlag']" with
          | null -> ()
          | hmember ->
            let mdoc = mem.XPathSelectElement "Docs"
            setval mdoc "param[@name='emptyFlag']" "Pass UrhoObjectFlag.Empty."
            setval mdoc "summary" "Empty constructor, chain to this constructor when you provide your own constructor that sets the handle field."
            let remarks = select mdoc "remarks"
            remarks.RemoveAll ()
            XElement.Parse ("<para>This constructor should be invoked by your code if you provide your own constructor that sets the handle field.</para>") |> remarks.Add
            XElement.Parse ("<para>This essentially circumvents the default path that creates a new object and sets the handle and does not call RegisterObject on the target, you must do this on your own constructor.</para>") |> remarks.Add
            XElement.Parse ("<para>You would typically chain to this constructor from your own, and then set the handle to the unmanaged object from your code, and then register your object.</para>") |> remarks.Add

  let fillTypeContext() =
    for x in doc.XPathSelectElements "Type/Members/Member[@MemberName='.ctor']" do
      match x with
      | null -> ()
      | mem ->
        match mem.XPathSelectElement "Parameters/Parameter[@Name='context']" with
          | null -> ()
          | hmember ->
            if mem.XPathSelectElements "Parameters" |> Seq.length = 1 then
              let mdoc = mem.XPathSelectElement "Docs"
              setval mdoc "param[@name='context']" "The context that this object will be attached to."
              setval mdoc "summary" <| (sprintf "Creates an instance of %s that is attached to an execution context." typeName)
              let remarks = select mdoc "remarks"
              remarks.RemoveAll ()
              sprintf "<para>This creates an instance of %s attached to the specified execution context.</para>" typeName |> XElement.Parse |> remarks.Add
              ()

  // For subscriptions, we like to fill in the stubs, but let the user
  // enter a different value if he wants to for the summary.
  let fillSubscribe() =
    for x in doc.XPathSelectElements "Type/Members/Member[ReturnValue/ReturnType = 'Urho.Subscription']" do
      match x with
        | null -> ()
        | m ->
          let name = membername m
          let eventName = name.Substring 11
          let mdoc = m.XPathSelectElement "Docs"
          setval mdoc "param[@name='handler']" "The handler to invoke when this event is raised."
          let summary = select mdoc "summary"
          if summary.Value = "To be added." then
            setval mdoc "summary" <| sprintf "Subscribes to the %s event raised by the %s (single subscriber)." eventName typeName
          setval mdoc "returns" "Returns an Urho.Subscription that can be used to cancel the subscription."
          let remarks = select mdoc "remarks"
          remarks.RemoveAll ()
          xp "<para>This method will override any prior subscription, including those assigned to on event handlers.</para>"  |> remarks.Add
          xp "<para>This has the advantage that it does a straight connection and returns a handle that is easy to unsubscribe from.</para>" |> remarks.Add
          sprintf "<para>For a more event-like approach, use the <see cref=\"E:Urho.%s.%s\"/> event.</para>" typeName eventName |> xp |> remarks.Add
          // Remember the method, so we can reference it from the event args
          let parameter = m.XPathSelectElement ("Parameters/Parameter[@Name='handler']")
          let eventArgsType = (((parameter.Attribute (xname "Type")).Value).Replace ("System.Action<Urho.","")).Replace (">","")
          if events.ContainsKey (eventArgsType) then
            ()
          else
            events.Add (eventArgsType, (eventName,typeName))

          // Now do the event handler
          //          let evtNode = doc.XPathSelectElement <| sprintf "Type/Members/Member[@MemberName='%s']" eventName
          // For now, we do nothing.
          // Perhaps the master documentation for the event should live here, and the SubscribeTo can copy that.

          // One time use below, because after this, I went and typed manual docs.
          //let evtRem = select evtNode "Docs/remarks"
          //if evtRem.Value = "To be added." then
          //  evtRem.RemoveAll ()
          //  sprintf "<para>The event can register multiple callbacks and invoke all of them.   If this is not desired, and you only need a single shot callback, you can use the <see cref=\"M:Urho.%s\"/> method.   That one will force that callback and will ignore any previously set events here.</para>" name |> xp |> evtRem.Add
 
  let fillStartAction() =
    for x in doc.XPathSelectElements "Type/Members/Member[@MemberName='StartAction']" do
      match x with
      | null -> ()
      | mem ->
        if (mem.ToString ()).Contains ("Urho.ActionState") then
          let mdoc = mem.XPathSelectElement "Docs"
          setval mdoc "summary" "Creates the action state for this action, called on demand from the framework to start executing the recipe."
          let remarks = select mdoc "remarks"
          remarks.RemoveAll ()
          xp "<para>The new <see cref=\"T:Urho.ActionState\"/> that encapsulates the state and provides the implementation to perform this action.</para>" |> remarks.Add
          setval mdoc "remarks" "New action that will perform the inverse of this action"
          let par = select mdoc "param[@name='target']"
          par.RemoveAll ();
          sprintf "<para>The new <see cref=\"T:Urho.ActionState\"/> that encapsulates the state and provides the implementation to perform your action.</para>" |> xp |> par.Add

  let fillReverse() =
    for x in doc.XPathSelectElements "Type/Members/Member[@MemberName='Reverse']" do
      match x with
      | null -> ()
      | mem ->
        if (mem.ToString ()).Contains ("Urho.FiniteTimeAction") then
          let mdoc = mem.XPathSelectElement "Docs"
          setval mdoc "summary" "Returns a new action that performs the exact inverse of this action."
          setval mdoc "remarks" ""
          setval mdoc "returns" "New action that will perform the inverse of this action"
          ()


  fillBaseType()
  fillType()
  fillTypeName()
  fillTypeNameStatic()
  fillTypeCtor()
  fillSubscribe()
  fillStartAction()
  //fillReverse()
  doc

let processPath path =
  match load path with
    | null ->
      printfn "Problem loading %A" path
      ()
    | doc ->
      match path.Contains "EventArgs" with
        | false -> processType doc |> save path
        | true  -> ()


for xmlDoc in Directory.GetFiles ("Urho", "*xml") do
  processPath xmlDoc

for xmlDoc in Directory.GetFiles ("Urho", "*EventArgs.xml") do
  match load xmlDoc with
    | null -> printfn "Failed to load %s" xmlDoc
    | doc ->
      processEventArgs doc |> save xmlDoc
      


