using System;
using Android.Content;
using Android.Graphics;
using Android.Hardware;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace Urho.Android
{
	public class SDLSurface : SurfaceView, ISurfaceHolderCallback, View.IOnKeyListener, View.IOnTouchListener, ISensorEventListener
	{
		protected SDLSurface(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public SDLSurface(Context context) : base(context)
		{
		}

		public SDLSurface(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public SDLSurface(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public SDLSurface(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		public void SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			throw new NotImplementedException();
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			throw new NotImplementedException();
		}

		public bool OnKey(View v, Keycode keyCode, KeyEvent e)
		{
			throw new NotImplementedException();
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			throw new NotImplementedException();
		}

		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{
			throw new NotImplementedException();
		}

		public void OnSensorChanged(SensorEvent e)
		{
			throw new NotImplementedException();
		}
	}
}