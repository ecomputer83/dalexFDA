package mono.com.syncfusion.charts;


public class ChartSeries_OnDataMarkerLabelCreatedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.charts.ChartSeries.OnDataMarkerLabelCreatedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLabelCreated:(Lcom/syncfusion/charts/DataMarkerLabel;)V:GetOnLabelCreated_Lcom_syncfusion_charts_DataMarkerLabel_Handler:Com.Syncfusion.Charts.ChartSeries/IOnDataMarkerLabelCreatedListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Charts.ChartSeries+IOnDataMarkerLabelCreatedListenerImplementor, Syncfusion.SfChart.Android", ChartSeries_OnDataMarkerLabelCreatedListenerImplementor.class, __md_methods);
	}


	public ChartSeries_OnDataMarkerLabelCreatedListenerImplementor ()
	{
		super ();
		if (getClass () == ChartSeries_OnDataMarkerLabelCreatedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Charts.ChartSeries+IOnDataMarkerLabelCreatedListenerImplementor, Syncfusion.SfChart.Android", "", this, new java.lang.Object[] {  });
	}


	public void onLabelCreated (com.syncfusion.charts.DataMarkerLabel p0)
	{
		n_onLabelCreated (p0);
	}

	private native void n_onLabelCreated (com.syncfusion.charts.DataMarkerLabel p0);

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
