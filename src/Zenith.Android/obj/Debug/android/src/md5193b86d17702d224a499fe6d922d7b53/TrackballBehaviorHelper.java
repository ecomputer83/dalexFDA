package md5193b86d17702d224a499fe6d922d7b53;


public class TrackballBehaviorHelper
	extends com.syncfusion.charts.ChartTrackballBehavior
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScroll:(FFFF)V:GetOnScroll_FFFFHandler\n" +
			"n_onTouchDown:(FF)V:GetOnTouchDown_FFHandler\n" +
			"n_onTouchMove:(FF)V:GetOnTouchMove_FFHandler\n" +
			"n_onTouchUp:(FF)V:GetOnTouchUp_FFHandler\n" +
			"n_onDoubleTap:(FF)V:GetOnDoubleTap_FFHandler\n" +
			"n_getView:(Lcom/syncfusion/charts/ChartSeries;Lcom/syncfusion/charts/ChartDataPoint;I)Landroid/view/View;:GetGetView_Lcom_syncfusion_charts_ChartSeries_Lcom_syncfusion_charts_ChartDataPoint_IHandler\n" +
			"n_getAxisView:(Ljava/lang/String;Lcom/syncfusion/charts/ChartAxis;)Landroid/view/View;:GetGetAxisView_Ljava_lang_String_Lcom_syncfusion_charts_ChartAxis_Handler\n" +
			"";
		mono.android.Runtime.register ("Syncfusion.SfChart.XForms.Droid.TrackballBehaviorHelper, Syncfusion.SfChart.XForms.Android", TrackballBehaviorHelper.class, __md_methods);
	}


	public TrackballBehaviorHelper ()
	{
		super ();
		if (getClass () == TrackballBehaviorHelper.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfChart.XForms.Droid.TrackballBehaviorHelper, Syncfusion.SfChart.XForms.Android", "", this, new java.lang.Object[] {  });
	}


	public void onScroll (float p0, float p1, float p2, float p3)
	{
		n_onScroll (p0, p1, p2, p3);
	}

	private native void n_onScroll (float p0, float p1, float p2, float p3);


	public void onTouchDown (float p0, float p1)
	{
		n_onTouchDown (p0, p1);
	}

	private native void n_onTouchDown (float p0, float p1);


	public void onTouchMove (float p0, float p1)
	{
		n_onTouchMove (p0, p1);
	}

	private native void n_onTouchMove (float p0, float p1);


	public void onTouchUp (float p0, float p1)
	{
		n_onTouchUp (p0, p1);
	}

	private native void n_onTouchUp (float p0, float p1);


	public void onDoubleTap (float p0, float p1)
	{
		n_onDoubleTap (p0, p1);
	}

	private native void n_onDoubleTap (float p0, float p1);


	public android.view.View getView (com.syncfusion.charts.ChartSeries p0, com.syncfusion.charts.ChartDataPoint p1, int p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (com.syncfusion.charts.ChartSeries p0, com.syncfusion.charts.ChartDataPoint p1, int p2);


	public android.view.View getAxisView (java.lang.String p0, com.syncfusion.charts.ChartAxis p1)
	{
		return n_getAxisView (p0, p1);
	}

	private native android.view.View n_getAxisView (java.lang.String p0, com.syncfusion.charts.ChartAxis p1);

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
