//
// UIElement C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using static System.Console;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class UIElement
	{
	    Dictionary<string, object> varMap = null; 

	    public void SetVar(string key, object obj)
	    {
	        if (varMap == null) //create inner dictionary on demand
                varMap = new Dictionary<string, object>();
	        varMap[key] = obj;
	    }

	    public object GetVar(string key)
	    {
	        if (varMap == null)
	            return null;

	        object obj;
	        varMap.TryGetValue(key, out obj);
	        return obj;
	    }

		public T CreateChild<T> (StringHash code, string name = "", uint index = UInt32.MaxValue) where T:UIElement
		{
			var ptr = UIElement_CreateChild (handle, code.Code, name, index);
			return Runtime.LookupObject<T> (ptr);
		}

		public BorderImage CreateBorderImage (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<BorderImage> (BorderImage.TypeStatic, name, index);
		}
		
		public Button CreateButton (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Button> (Button.TypeStatic, name, index);
		}
		
		public Menu CreateMenu (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Menu> (Menu.TypeStatic, name, index);
		}
		
		public DropDownList CreateDropDownList (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<DropDownList> (DropDownList.TypeStatic, name, index);
		}
		
		public CheckBox CreateCheckBox (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<CheckBox> (CheckBox.TypeStatic, name, index);
		}
		
		public Cursor CreateCursor (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Cursor> (Cursor.TypeStatic, name, index);
		}
		
		public LineEdit CreateLineEdit (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<LineEdit> (LineEdit.TypeStatic, name, index);
		}
		
		public Slider CreateSlider (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Slider> (Slider.TypeStatic, name, index);
		}
		
		public Window CreateWindow (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Window> (Window.TypeStatic, name, index);
		}
		
		public View3D CreateView3D (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<View3D> (View3D.TypeStatic, name, index);
		}
		
		public ScrollBar CreateScrollBar (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<ScrollBar> (ScrollBar.TypeStatic, name, index);
		}
		
		public ScrollView CreateScrollView (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<ScrollView> (ScrollView.TypeStatic, name, index);
		}
		
		public ListView CreateListView (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<ListView> (ListView.TypeStatic, name, index);
		}
		
		public Sprite CreateSprite (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Sprite> (Sprite.TypeStatic, name, index);
		}
		
		public Text CreateText (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<Text> (Text.TypeStatic, name, index);
		}
		
		public ToolTip CreateToolTip (string name = "", uint index = UInt32.MaxValue)
		{
			return CreateChild<ToolTip> (ToolTip.TypeStatic, name, index);
		}
	}
}
