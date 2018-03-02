using System;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Playgrounds.Console
{
	public class EarthDemo : SimpleApplication
	{
		Node earthNode;
		MonoDebugHud hud;

		[Preserve]
		public EarthDemo(ApplicationOptions options) : base(options) { }

		public static void RunApp()
		{
			var app = new EarthDemo(
				new ApplicationOptions(@"..\..\Samples\HoloLens\02_HelloWorldAdvanced\Data") {
				Width = 960,
				Height = 720,
				UseDirectX11 = false
			});
			app.Run();
		}

		protected override async void Start()
		{
			base.Start();

			hud = new MonoDebugHud(this);
			hud.Show(Color.Yellow, 24);

			var timeLabel = new Text {
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Top
			};
			timeLabel.SetColor(new Color(0f, 1f, 0f));
			timeLabel.SetFont(font: CoreAssets.Fonts.AnonymousPro , size: 30);
			UI.Root.AddChild(timeLabel);

			var azAltLabel = new Text {
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Bottom
			};
			azAltLabel.SetColor(new Color(0f, 1f, 0f));
			azAltLabel.SetFont(font: CoreAssets.Fonts.AnonymousPro, size: 30);
			UI.Root.AddChild(azAltLabel);

			ResourceCache.AutoReloadResources = true;
			Viewport.SetClearColor(Color.Black);

			earthNode = RootNode.CreateChild();
			earthNode.SetScale(5f);
			earthNode.Rotation = new Quaternion(0, -180, 0);
			var earthModel = earthNode.CreateComponent<Sphere>();
			earthModel.Material = Material.FromImage("Textures/Earth.jpg");

			Zone.AmbientColor = new Color(0.2f, 0.2f, 0.2f);
			LightNode.ChangeParent(Scene);
			LightNode.Position = Vector3.Zero;
			Light.Range = 10;
			Light.Brightness = 1f;
			Light.LightType = LightType.Directional;

			AddMarker(0, 0, "(0, 0)");
			AddMarker(53.9045f, 27.5615f, "Minsk");
			AddMarker(51.5074f, 0.1278f, "London");
			AddMarker(40.7128f, -74.0059f, "New-York");
			AddMarker(37.7749f, -122.4194f, "San Francisco");
			AddMarker(39.9042f, 116.4074f, "Beijing");
			AddMarker(-31.9505f, 115.8605f, "Perth");

			var sunNode = RootNode.CreateChild();
			var sunModelNode = sunNode.CreateChild();
			sunModelNode.Position = new Vector3(0, 4, 0);
			sunModelNode.SetScale(1);

			var sun = sunModelNode.CreateComponent<Sphere>();
			sun.Color = new Color(15, 10, 5);

			// update the Sun's position based on time 
			var time = DateTime.Now;
			float alt, az;
			SunPosition.CalculateSunPosition(time, 0f, 0f, out az, out alt);
			sunNode.Rotation = new Quaternion(-az, 0, alt);
			LightNode.SetDirection(RootNode.WorldPosition - sunModelNode.WorldPosition);

			timeLabel.Value = time.ToShortTimeString();
			azAltLabel.Value = $"Azimuth: {az:F1},  Altitude: {alt:F1}";
		}

		public void AddMarker(float lat, float lon, string name)
		{
			var height = earthNode.Scale.Y / 2f;

			lat = (float) Math.PI * lat / 180f - (float)Math.PI/2f;
			lon = (float) Math.PI * lon / 180f;

			float x = height * (float)Math.Sin(lat) * (float)Math.Cos(lon);
			float z = height * (float)Math.Sin(lat) * (float)Math.Sin(lon);
			float y = height * (float)Math.Cos(lat);

			var markerNode = RootNode.CreateChild();
			markerNode.Scale = Vector3.One * 0.1f;
			markerNode.Position = new Vector3((float)x, (float)y, (float)z);
			markerNode.CreateComponent<Sphere>();
			markerNode.RunActionsAsync(new RepeatForever(
				new TintTo(0.5f, Color.White), 
				new TintTo(0.5f, Color.Cyan)));

			var textPos = markerNode.Position;
			textPos.Normalize();
			
			var textNode = markerNode.CreateChild();
			textNode.Position = textPos * 1;
			textNode.SetScale(2f);
			textNode.LookAt(Vector3.Zero, Vector3.Up, TransformSpace.Parent);
			var text = textNode.CreateComponent<Text3D>();
			text.SetFont(CoreAssets.Fonts.AnonymousPro, 150);
			text.EffectColor = Color.Black;
			text.TextEffect = TextEffect.Stroke;
			text.Text = name;
		}

		// http://guideving.blogspot.com.by/2010/08/sun-position-in-c.html
		public static class SunPosition
		{
			private const double Deg2Rad = Math.PI / 180.0;
			private const double Rad2Deg = 180.0 / Math.PI;

			/*!
			 * \brief Calculates the sun light.
			 *
			 * CalcSunPosition calculates the suns "position" based on a
			 * given date and time in local time, latitude and longitude
			 * expressed in decimal degrees. It is based on the method
			 * found here:
			 * http://www.astro.uio.no/~bgranslo/aares/calculate.html
			 * The calculation is only satisfiably correct for dates in
			 * the range March 1 1900 to February 28 2100.
			 * \param dateTime Time and date in local time.
			 * \param latitude Latitude expressed in decimal degrees.
			 * \param longitude Longitude expressed in decimal degrees.
			 */
			public static void CalculateSunPosition(
				DateTime dateTime, double latitude, double longitude, out float az, out float alt)
			{
				// Convert to UTC
				dateTime = dateTime.ToUniversalTime();

				// Number of days from J2000.0.
				double julianDate = 367 * dateTime.Year -
					(int)((7.0 / 4.0) * (dateTime.Year +
					(int)((dateTime.Month + 9.0) / 12.0))) +
					(int)((275.0 * dateTime.Month) / 9.0) +
					dateTime.Day - 730531.5;

				double julianCenturies = julianDate / 36525.0;

				// Sidereal Time
				double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

				double siderealTimeUT = siderealTimeHours +
					(366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

				double siderealTime = siderealTimeUT * 15 + longitude;

				// Refine to number of days (fractional) to specific time.
				julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
				julianCenturies = julianDate / 36525.0;

				// Solar Coordinates
				double meanLongitude = CorrectAngle(Deg2Rad *
					(280.466 + 36000.77 * julianCenturies));

				double meanAnomaly = CorrectAngle(Deg2Rad *
					(357.529 + 35999.05 * julianCenturies));

				double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
					Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

				double elipticalLongitude =
					CorrectAngle(meanLongitude + equationOfCenter);

				double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

				// Right Ascension
				double rightAscension = Math.Atan2(
					Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
					Math.Cos(elipticalLongitude));

				double declination = Math.Asin(
					Math.Sin(rightAscension) * Math.Sin(obliquity));

				// Horizontal Coordinates
				double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

				if (hourAngle > Math.PI)
				{
					hourAngle -= 2 * Math.PI;
				}

				double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
					Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
					Math.Cos(declination) * Math.Cos(hourAngle));

				// Nominator and denominator for calculating Azimuth
				// angle. Needed to test which quadrant the angle is in.
				double aziNom = -Math.Sin(hourAngle);
				double aziDenom =
					Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
					Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

				double azimuth = Math.Atan(aziNom / aziDenom);

				if (aziDenom < 0) // In 2nd or 3rd quadrant
				{
					azimuth += Math.PI;
				}
				else if (aziNom < 0) // In 4th quadrant
				{
					azimuth += 2 * Math.PI;
				}

				alt = (float)(altitude * Rad2Deg);
				az = (float)(azimuth * Rad2Deg);
			}

			private static double CorrectAngle(double angleInRadians)
			{
				if (angleInRadians < 0)
					return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
				if (angleInRadians > 2 * Math.PI)
					return angleInRadians % (2 * Math.PI);
				return angleInRadians;
			}
		}
	}
}
