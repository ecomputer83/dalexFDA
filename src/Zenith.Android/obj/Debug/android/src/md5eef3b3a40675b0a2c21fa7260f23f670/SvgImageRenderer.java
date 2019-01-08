package md5eef3b3a40675b0a2c21fa7260f23f670;


public class SvgImageRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"";
		mono.android.Runtime.register ("XamSvg.XamForms.Droid.SvgImageRenderer, XamSvg.XamForms.Droid", SvgImageRenderer.class, __md_methods);
	}


	public SvgImageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SvgImageRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgImageRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SvgImageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SvgImageRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgImageRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public SvgImageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SvgImageRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgImageRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();

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
