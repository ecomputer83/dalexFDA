package md5eef3b3a40675b0a2c21fa7260f23f670;


public class SvgImageRenderer_GenericMenuClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.MenuItem.OnMenuItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMenuItemClick:(Landroid/view/MenuItem;)Z:GetOnMenuItemClick_Landroid_view_MenuItem_Handler:Android.Views.IMenuItemOnMenuItemClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("XamSvg.XamForms.Droid.SvgImageRenderer+GenericMenuClickListener, XamSvg.XamForms.Droid", SvgImageRenderer_GenericMenuClickListener.class, __md_methods);
	}


	public SvgImageRenderer_GenericMenuClickListener ()
	{
		super ();
		if (getClass () == SvgImageRenderer_GenericMenuClickListener.class)
			mono.android.TypeManager.Activate ("XamSvg.XamForms.Droid.SvgImageRenderer+GenericMenuClickListener, XamSvg.XamForms.Droid", "", this, new java.lang.Object[] {  });
	}


	public boolean onMenuItemClick (android.view.MenuItem p0)
	{
		return n_onMenuItemClick (p0);
	}

	private native boolean n_onMenuItemClick (android.view.MenuItem p0);

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
