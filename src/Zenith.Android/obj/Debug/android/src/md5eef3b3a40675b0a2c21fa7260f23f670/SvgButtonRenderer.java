package md5eef3b3a40675b0a2c21fa7260f23f670;


public class SvgButtonRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ButtonRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XamSvg.XamForms.Droid.SvgButtonRenderer, XamSvg.XamForms.Droid", SvgButtonRenderer.class, __md_methods);
	}


	public SvgButtonRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SvgButtonRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgButtonRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SvgButtonRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SvgButtonRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgButtonRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public SvgButtonRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SvgButtonRenderer.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgButtonRenderer, XamSvg.XamForms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
