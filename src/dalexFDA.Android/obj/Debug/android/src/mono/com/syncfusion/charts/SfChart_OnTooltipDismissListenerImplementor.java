package mono.com.syncfusion.charts;


public class SfChart_OnTooltipDismissListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.charts.SfChart.OnTooltipDismissListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDismiss:(Lcom/syncfusion/charts/SfChart;Lcom/syncfusion/charts/TooltipView;)V:GetOnDismiss_Lcom_syncfusion_charts_SfChart_Lcom_syncfusion_charts_TooltipView_Handler:Com.Syncfusion.Charts.SfChart/IOnTooltipDismissListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Charts.SfChart+IOnTooltipDismissListenerImplementor, Syncfusion.SfChart.Android", SfChart_OnTooltipDismissListenerImplementor.class, __md_methods);
	}


	public SfChart_OnTooltipDismissListenerImplementor ()
	{
		super ();
		if (getClass () == SfChart_OnTooltipDismissListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Charts.SfChart+IOnTooltipDismissListenerImplementor, Syncfusion.SfChart.Android", "", this, new java.lang.Object[] {  });
	}


	public void onDismiss (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.TooltipView p1)
	{
		n_onDismiss (p0, p1);
	}

	private native void n_onDismiss (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.TooltipView p1);

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
