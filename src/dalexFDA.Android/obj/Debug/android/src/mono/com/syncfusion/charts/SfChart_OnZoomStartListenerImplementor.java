package mono.com.syncfusion.charts;


public class SfChart_OnZoomStartListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.charts.SfChart.OnZoomStartListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnZoomStartListener:(Lcom/syncfusion/charts/SfChart;Lcom/syncfusion/charts/ChartZoomStartEvent;)V:GetOnZoomStartListener_Lcom_syncfusion_charts_SfChart_Lcom_syncfusion_charts_ChartZoomStartEvent_Handler:Com.Syncfusion.Charts.SfChart/IOnZoomStartListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Charts.SfChart+IOnZoomStartListenerImplementor, Syncfusion.SfChart.Android", SfChart_OnZoomStartListenerImplementor.class, __md_methods);
	}


	public SfChart_OnZoomStartListenerImplementor ()
	{
		super ();
		if (getClass () == SfChart_OnZoomStartListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Charts.SfChart+IOnZoomStartListenerImplementor, Syncfusion.SfChart.Android", "", this, new java.lang.Object[] {  });
	}


	public void OnZoomStartListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartZoomStartEvent p1)
	{
		n_OnZoomStartListener (p0, p1);
	}

	private native void n_OnZoomStartListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartZoomStartEvent p1);

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
