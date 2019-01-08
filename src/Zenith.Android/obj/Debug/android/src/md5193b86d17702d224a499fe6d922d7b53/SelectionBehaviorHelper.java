package md5193b86d17702d224a499fe6d922d7b53;


public class SelectionBehaviorHelper
	extends com.syncfusion.charts.ChartSelectionBehavior
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSelectionChanging:(Lcom/syncfusion/charts/ChartSelectionChangingEvent;)V:GetOnSelectionChanging_Lcom_syncfusion_charts_ChartSelectionChangingEvent_Handler\n" +
			"n_onSelectionChanged:(Lcom/syncfusion/charts/ChartSelectionEvent;)V:GetOnSelectionChanged_Lcom_syncfusion_charts_ChartSelectionEvent_Handler\n" +
			"n_onTouchDown:(FF)V:GetOnTouchDown_FFHandler\n" +
			"n_onTouchMove:(FF)V:GetOnTouchMove_FFHandler\n" +
			"n_onTouchUp:(FF)V:GetOnTouchUp_FFHandler\n" +
			"n_onDoubleTap:(FF)V:GetOnDoubleTap_FFHandler\n" +
			"";
		mono.android.Runtime.register ("Syncfusion.SfChart.XForms.Droid.SelectionBehaviorHelper, Syncfusion.SfChart.XForms.Android", SelectionBehaviorHelper.class, __md_methods);
	}


	public SelectionBehaviorHelper ()
	{
		super ();
		if (getClass () == SelectionBehaviorHelper.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfChart.XForms.Droid.SelectionBehaviorHelper, Syncfusion.SfChart.XForms.Android", "", this, new java.lang.Object[] {  });
	}


	public void onSelectionChanging (com.syncfusion.charts.ChartSelectionChangingEvent p0)
	{
		n_onSelectionChanging (p0);
	}

	private native void n_onSelectionChanging (com.syncfusion.charts.ChartSelectionChangingEvent p0);


	public void onSelectionChanged (com.syncfusion.charts.ChartSelectionEvent p0)
	{
		n_onSelectionChanged (p0);
	}

	private native void n_onSelectionChanged (com.syncfusion.charts.ChartSelectionEvent p0);


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
