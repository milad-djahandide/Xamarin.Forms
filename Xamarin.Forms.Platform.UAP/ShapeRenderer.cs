﻿using System.ComponentModel;
using Windows.UI.Xaml;
using WDoubleCollection = Windows.UI.Xaml.Media.DoubleCollection;
using WShape = Windows.UI.Xaml.Shapes.Shape;
using WStretch = Windows.UI.Xaml.Media.Stretch;

namespace Xamarin.Forms.Platform.UWP
{
	public class ShapeRenderer<TShape, TNativeShape> : ViewRenderer<TShape, TNativeShape>
		  where TShape : Shape
		  where TNativeShape : WShape
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TShape> args)
		{
			base.OnElementChanged(args);

			if (args.NewElement != null)
			{
				UpdateAspect();
				UpdateFill();
				UpdateStroke();
				UpdateStrokeThickness();
				UpdateStrokeDashArray();
				UpdateStrokeDashOffset();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(sender, args);

			if (args.PropertyName == VisualElement.HeightProperty.PropertyName)
				UpdateHeight();
			else if (args.PropertyName == VisualElement.WidthProperty.PropertyName)
				UpdateWidth();
			else if (args.PropertyName == Shape.AspectProperty.PropertyName)
				UpdateAspect();
			else if (args.PropertyName == Shape.FillProperty.PropertyName)
				UpdateFill();
			else if (args.PropertyName == Shape.StrokeProperty.PropertyName)
				UpdateStroke();
			else if (args.PropertyName == Shape.StrokeThicknessProperty.PropertyName)
				UpdateStrokeThickness();
			else if (args.PropertyName == Shape.StrokeDashArrayProperty.PropertyName)
				UpdateStrokeDashArray();
			else if (args.PropertyName == Shape.StrokeDashOffsetProperty.PropertyName)
				UpdateStrokeDashOffset();
		}

		void UpdateHeight()
		{
			Control.Height = Element.Height;
		}

		void UpdateWidth()
		{
			Control.Width = Element.Width;
		}

		void UpdateAspect()
		{
			Stretch aspect = Element.Aspect;
			WStretch stretch = WStretch.None;

			switch (aspect)
			{
				case Stretch.None:
					stretch = WStretch.None;
					break;
				case Stretch.Fill:
					stretch = WStretch.Fill;
					break;
				case Stretch.Uniform:
					stretch = WStretch.Uniform;
					break;
				case Stretch.UniformToFill:
					stretch = WStretch.UniformToFill;
					break;
			}

			Control.Stretch = stretch;

			if (aspect == Stretch.Uniform)
			{
				Control.HorizontalAlignment = HorizontalAlignment.Center;
				Control.VerticalAlignment = VerticalAlignment.Center;
			}
			else
			{
				Control.HorizontalAlignment = HorizontalAlignment.Left;
				Control.VerticalAlignment = VerticalAlignment.Top;
			}
		}

		void UpdateFill()
		{
			Control.Fill = Element.Fill.ToBrush();
		}

		void UpdateStroke()
		{
			Control.Stroke = Element.Stroke.ToBrush();
		}

		void UpdateStrokeThickness()
		{
			Control.StrokeThickness = Element.StrokeThickness;
		}

		void UpdateStrokeDashArray()
		{
			if (Control.StrokeDashArray != null)
				Control.StrokeDashArray.Clear();

			if (Element.StrokeDashArray != null && Element.StrokeDashArray.Count > 0)
			{
				if (Control.StrokeDashArray == null)
					Control.StrokeDashArray = new WDoubleCollection();

				double[] array = new double[Element.StrokeDashArray.Count];
				Element.StrokeDashArray.CopyTo(array, 0);

				foreach (double value in array)
				{
					Control.StrokeDashArray.Add(value);
				}
			}
		}

		void UpdateStrokeDashOffset()
		{
			Control.StrokeDashOffset = Element.StrokeDashOffset;
		}
	}
}