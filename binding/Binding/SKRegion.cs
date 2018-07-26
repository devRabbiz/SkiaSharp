﻿using System;

namespace SkiaSharp
{
	public class SKRegion : SKObject
	{
		[Preserve]
		internal SKRegion(IntPtr handle,  bool owns)
			: base (handle, owns)
		{
		}

		public SKRegion()
			: this (SkiaApi.sk_region_new(), true)
		{
		}

		public SKRegion(SKRegion region)
			: this(SkiaApi.sk_region_new2(region.Handle), true)
		{
		}

		public SKRectI Bounds {
			get {
				SKRectI rect;
				SkiaApi.sk_region_get_bounds(Handle, out rect);
				return rect;
			}
		}

		public bool Contains(SKRegion src)
		{
			if (src == null)
				throw new ArgumentNullException (nameof (src));
			return SkiaApi.sk_region_contains(Handle, src.Handle); 
		}

		public bool Contains(SKPointI xy)
		{
			return SkiaApi.sk_region_contains2(Handle, xy.X, xy.Y);
		}

		public bool Contains(int x, int y)
		{
			return SkiaApi.sk_region_contains2(Handle, x, y);
		}

		public bool Intersects(SKRegion region)
		{
			if (region == null)
				throw new ArgumentNullException (nameof (region));
			return SkiaApi.sk_region_intersects(Handle, region.Handle);
		}

		public bool Intersects(SKRectI rect)
		{
			return SkiaApi.sk_region_intersects(Handle, rect);
		}

		public bool SetRegion(SKRegion region)
		{
			if (region == null)
				throw new ArgumentNullException (nameof (region));
			return SkiaApi.sk_region_set_region(Handle, region.Handle);
		}

		public bool SetRect(SKRectI rect)
		{
			return SkiaApi.sk_region_set_rect(Handle, ref rect); 
		}

		public bool SetPath(SKPath path, SKRegion clip)
		{
			if (path == null)
				throw new ArgumentNullException (nameof (path));
			if (clip == null)
				throw new ArgumentNullException (nameof (clip));
			return SkiaApi.sk_region_set_path(Handle, path.Handle, clip.Handle); 
		}

		public bool SetPath(SKPath path)
		{
			if (path == null)
				throw new ArgumentNullException (nameof (path));

			using (var clip = new SKRegion()) {
				SKRect rect;
				if (path.GetBounds(out rect)) {
					var recti = new SKRectI(
						(int)rect.Left,
						(int)rect.Top,
						(int)Math.Ceiling(rect.Right),
						(int)Math.Ceiling(rect.Bottom));
					clip.SetRect(recti);
				}

				return SkiaApi.sk_region_set_path(Handle, path.Handle, clip.Handle);
			}
		}

		public bool Op(SKRectI rect, SKRegionOperation op)
		{
			return SkiaApi.sk_region_op(Handle, rect.Left, rect.Top, rect.Right, rect.Bottom, op);
		}

		public bool Op(int left, int top, int right, int bottom, SKRegionOperation op)
		{
			return SkiaApi.sk_region_op(Handle, left, top, right, bottom, op);
		}

		public bool Op(SKRegion region, SKRegionOperation op)
		{
			return SkiaApi.sk_region_op2(Handle, region.Handle, op);
		}
	}
}
