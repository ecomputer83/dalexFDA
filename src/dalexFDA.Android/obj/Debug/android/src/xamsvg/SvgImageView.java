package xamsvg;


public class SvgImageView
	extends android.view.View
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_verifyDrawable:(Landroid/graphics/drawable/Drawable;)Z:GetVerifyDrawable_Landroid_graphics_drawable_Drawable_Handler\n" +
			"n_invalidateDrawable:(Landroid/graphics/drawable/Drawable;)V:GetInvalidateDrawable_Landroid_graphics_drawable_Drawable_Handler\n" +
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"n_onSizeChanged:(IIII)V:GetOnSizeChanged_IIIIHandler\n" +
			"n_onDraw:(Landroid/graphics/Canvas;)V:GetOnDraw_Landroid_graphics_Canvas_Handler\n" +
			"n_getBaseline:()I:GetGetBaselineHandler\n" +
			"n_isPressed:()Z:GetIsPressedHandler\n" +
			"n_setPressed:(Z)V:GetSetPressed_ZHandler\n" +
			"n_isActivated:()Z:GetIsActivatedHandler\n" +
			"n_setActivated:(Z)V:GetSetActivated_ZHandler\n" +
			"n_isSelected:()Z:GetIsSelectedHandler\n" +
			"n_setSelected:(Z)V:GetSetSelected_ZHandler\n" +
			"";
		mono.android.Runtime.register ("XamSvg.SvgImageView, XamSvg.Droid", SvgImageView.class, __md_methods);
	}


	public SvgImageView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SvgImageView.class)
			mono.android.TypeManager.Activate ("XamSvg.SvgImageView, XamSvg.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public SvgImageView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SvgImageView.class)
			mono.android.TypeManager.Activate ("XamSvg.SvgImageView, XamSvg.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public SvgImageView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SvgImageView.class)
			mono.android.TypeManager.Activate ("XamSvg.SvgImageView, XamSvg.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SvgImageView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == SvgImageView.class)
			mono.android.TypeManager.Activate ("XamSvg.SvgImageView, XamSvg.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public boolean verifyDrawable (android.graphics.drawable.Drawable p0)
	{
		return n_verifyDrawable (p0);
	}

	private native boolean n_verifyDrawable (android.graphics.drawable.Drawable p0);


	public void invalidateDrawable (android.graphics.drawable.Drawable p0)
	{
		n_invalidateDrawable (p0);
	}

	private native void n_invalidateDrawable (android.graphics.drawable.Drawable p0);


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);


	public void onSizeChanged (int p0, int p1, int p2, int p3)
	{
		n_onSizeChanged (p0, p1, p2, p3);
	}

	private native void n_onSizeChanged (int p0, int p1, int p2, int p3);


	public void onDraw (android.graphics.Canvas p0)
	{
		n_onDraw (p0);
	}

	private native void n_onDraw (android.graphics.Canvas p0);


	public int getBaseline ()
	{
		return n_getBaseline ();
	}

	private native int n_getBaseline ();


	public boolean isPressed ()
	{
		return n_isPressed ();
	}

	private native boolean n_isPressed ();


	public void setPressed (boolean p0)
	{
		n_setPressed (p0);
	}

	private native void n_setPressed (boolean p0);


	public boolean isActivated ()
	{
		return n_isActivated ();
	}

	private native boolean n_isActivated ();


	public void setActivated (boolean p0)
	{
		n_setActivated (p0);
	}

	private native void n_setActivated (boolean p0);


	public boolean isSelected ()
	{
		return n_isSelected ();
	}

	private native boolean n_isSelected ();


	public void setSelected (boolean p0)
	{
		n_setSelected (p0);
	}

	private native void n_setSelected (boolean p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
