using System.Collections.Generic;
using System.IO;
using System;

namespace AssetsModelGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			 Create object model from assets folder
			 Can be used via T4
			 */

			if (args.Length != 2)
			{
				Console.WriteLine("Usage: assets_folder output_path");
				return;
			}

			string assetsFolder = args[0];
			string codeFile = args[1];

			string code = @"using Urho.Gui;
using Urho.Urho2D;
using Urho.Resources;

namespace Urho
{" + "\n";

			Node rootNode = new Node { Name = "CoreAssets", Level = 1 };
			VisitFolder(assetsFolder, rootNode);
			GenerateCode(rootNode, ref code, assetsFolder);
			code += "\n}";
			File.WriteAllText(codeFile, code);
		}

		static void GetFiles(Node node, List<string> files)
		{
			files.AddRange(node.Files);
			foreach (var child in node.Children)
				GetFiles(child, files);
		}

		static void GenerateCode(Node node, ref string code, string rootFolder)
		{
			List<string> allFiles = new List<string>();
			GetFiles(node, allFiles);
			if (allFiles.Count < 1)
				return;

			//check if node has any files
			code += $"\n{Tabs(node.Level)}public static class {node.Name}";
			if (node.Level == 1)
				code += $"\n{Tabs(node.Level)}{{\n{Tabs(node.Level + 1)}public static ResourceCache Cache => Application.Current.ResourceCache;\n";
			else
				code += $"\n{Tabs(node.Level)}{{\n";

			foreach (var file in node.Files)
			{
				string relativePath = file.Remove(0, rootFolder.Length + 1).Replace("\\", "/");
				code += $"{Tabs(node.Level + 1)}public static {node.AssetType} {Path.GetFileNameWithoutExtension(file)} => Cache.Get{node.AssetType}(\"{relativePath}\");\n";
			}
			foreach (var child in node.Children)
			{
				GenerateCode(child, ref code, rootFolder);
			}
			code += $"{Tabs(node.Level)}}}\n";
		}

		static string GetShaderParameters(string file)
		{
			// file = @"..\..\..\..\Urho3D\CoreData\Shaders\GLSL\Uniforms.glsl";
			string code = "";
			var lines = File.ReadAllLines(file);

			int lineIndex = -1;
			code += "public static class ShaderParameters\n{\n";
			foreach (var line in lines)
			{
				lineIndex++;
				var paramName = line.Trim(';', ' ', '\t', '\n', '\r');
				if (!paramName.StartsWith("uniform "))
					continue;

				paramName = paramName.Remove(0, "uniform ".Length);
				if (!paramName.Contains(" "))
					continue;

				var paramType = paramName.Substring(0, paramName.IndexOf(' '));
				paramName = paramName.Remove(0, paramType.Length + 1);
				if (!paramName.StartsWith("c") || paramName.Contains("["))
					continue;

				paramName = paramName.Remove(0, 1);// remove 'c' prefix

				string comment = $"Type: {paramType}, Defined in {Path.GetFileName(file)}:L{lineIndex}";

				code += $"\t/// <summary>\n";
				code += $"\t/// {comment}\n";
				code += $"\t/// </summary>\n";
				code += $"\tpublic const string {paramName} = \"{paramName}\";\n\n";
			}
			code += "}";

			return code;
		}

		static string Tabs(int c) => new string('\t', c);

		static void VisitFolder(string folder, Node currentNode)
		{
			var folderName = Path.GetFileName(folder);
			switch (folderName)
			{
				case "Materials": currentNode.AssetType = AssetType.Material; break;
				case "Textures": currentNode.AssetType = AssetType.Texture2D; break;
				case "Models": currentNode.AssetType = AssetType.Model; break;
				case "Fonts": currentNode.AssetType = AssetType.Font; break;
				//case "Shaders": currentNode.AssetType = AssetType.Shader; break;
				case "Techniques": currentNode.AssetType = AssetType.Technique; break;
				case "RenderPaths": currentNode.AssetType = AssetType.XmlFile; break;
				case "Music": currentNode.AssetType = AssetType.Sound; break;
				case "Sounds": currentNode.AssetType = AssetType.Sound; break;
				case "Scenes": currentNode.AssetType = AssetType.XmlFile; break;
				case "PostProcess": currentNode.AssetType = AssetType.XmlFile; break;
			}

			if (currentNode.AssetType > AssetType.Unknown)
			{
				currentNode.Files.AddRange(Directory.GetFiles(folder));
			}
			
			foreach (var subDir in Directory.GetDirectories(folder))
			{
				Node childNode = new Node { Name = Path.GetFileName(subDir), AssetType = currentNode.AssetType, Level = currentNode.Level + 1 };
				currentNode.Children.Add(childNode);
				VisitFolder(subDir, childNode);
			}
		}

		public enum AssetType
		{
			Default = 0,
			Unknown,
			Model,
			Material,
			Texture2D,
			Font,
			XmlFile,
			//Shader,
			Technique,
			Sound
		}

		public class Node
		{
			public int Level { get; set; }
			public string Name { get; set; }
			public AssetType AssetType { get; set; }
			public List<Node> Children { get; } = new List<Node>();
			public List<string> Files { get; } = new List<string>();

			public override string ToString() => $"{Name}, Type={AssetType}, Files={Files.Count}, Children={Children.Count}";
		}
	}
}
