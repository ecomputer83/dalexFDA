package md5cc784f1200c1e948427dbe259de07ed3;


public class PieLabelRenderer
	extends com.telerik.widget.chart.visualization.pieChart.PieSeriesLabelRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getLabelText:(Lcom/telerik/widget/chart/engine/dataPoints/DataPoint;)Ljava/lang/String;:GetGetLabelText_Lcom_telerik_widget_chart_engine_dataPoints_DataPoint_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.ChartRenderer.Android.PieLabelRenderer, Telerik.XamarinForms.Chart", PieLabelRenderer.class, __md_methods);
	}


	public PieLabelRenderer (com.telerik.widget.chart.visualization.pieChart.PieSeries p0)
	{
		super (p0);
		if (getClass () == PieLabelRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.PieLabelRenderer, Telerik.XamarinForms.Chart", "Com.Telerik.Widget.Chart.Visualization.PieChart.PieSeries, Telerik.Xamarin.Android.Chart", this, new java.lang.Object[] { p0 });
	}


	public java.lang.String getLabelText (com.telerik.widget.chart.engine.dataPoints.DataPoint p0)
	{
		return n_getLabelText (p0);
	}

	private native java.lang.String n_getLabelText (com.telerik.widget.chart.engine.dataPoints.DataPoint p0);

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
