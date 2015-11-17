open System
open System.Xml
open System.Xml.XPath
open System.IO
open System.Xml.Linq
open System.Text
open System.Globalization

let load path =
  let f = File.OpenText path
  let doc = XDocument.Load f
  f.Close()
  doc

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
  output.Close ();
  
let select (node:XNode) path = node.XPathSelectElement path
let setval (node:XNode) path value = (select node path).Value <- value
let xname s = XName.Get(s)

let processType (doc:XDocument) =
  let typeName =
    let tnode = doc.XPathSelectElement ("Type")
    let attr = tnode.Attribute (xname "Name")
    attr.Value
    
  let fillBaseType =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='BaseType']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's type system base type."
        setval mdoc "value" "StringHash representing the base type for this Urho type."
        setval mdoc "remarks" "This returns the Urho type system base type and is surfaced for low-level Urho code."
  let fillType =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='Type']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's type system type."
        try
          setval mdoc "value" "StringHash representing the type for this C# type."
        with
          | ex -> ()
        setval mdoc "remarks" "This returns the Urho's type and is surfaced for low-level Urho code."
  let fillTypeName = 
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='TypeName']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's low-level type name."
        setval mdoc "value" "Stringified low-level type name."
        setval mdoc "remarks" ""
  let fillTypeNameStatic =
    match doc.XPathSelectElement "Type/Members/Member[@MemberName='TypeNameStatic']/Docs" with
      | null -> ()
      | mdoc ->
        setval mdoc "summary" "Urho's low-level type name, accessible as a static method."
        setval mdoc "value" "Stringified low-level type name."
        setval mdoc "remarks" ""

  let fillTypeCtor =
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

  let fillTypeEmpty =
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

  let fillTypeContext =
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

  fillBaseType
  fillType 
  fillTypeName 
  fillTypeNameStatic 
  fillTypeCtor 
  doc

let processPath path =
  let xml path = load path
  match load path with
    | null ->
      printfn "Problem loading %A" path
      ()
    | doc -> processType doc |> save path


for xmlDoc in Directory.GetFiles ("Urho", "*xml") do
  processPath xmlDoc


