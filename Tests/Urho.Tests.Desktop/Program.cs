using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho.Desktop;
using Urho.Samples;

namespace Urho.Tests.Desktop
{
	class Program
	{
		static void Main(string[] args)
		{
			Environment.CurrentDirectory = @"..\..\..\..\Urho3D\Source\bin";
			new AnimatingScene(new ApplicationOptions("Data")).Run();
		}
	}
}
