package mono.com.syncfusion.rangenavigator;


public class SfDateTimeRangeNavigator_OnMajorScaleLabelsCreateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.rangenavigator.SfDateTimeRangeNavigator.OnMajorScaleLabelsCreateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMajorScaleLabelsGenerated:(Ljava/util/ArrayList;)V:GetOnMajorScaleLabelsGenerated_Ljava_util_ArrayList_Handler:Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator/IOnMajorScaleLabelsCreateListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator+IOnMajorScaleLabelsCreateListenerImplementor, Syncfusion.SfChart.Android", SfDateTimeRangeNavigator_OnMajorScaleLabelsCreateListenerImplementor.class, __md_methods);
	}


	public SfDateTimeRangeNavigator_OnMajorScaleLabelsCreateListenerImplementor ()
	{
		super ();
		if (getClass () == SfDateTimeRangeNavigator_OnMajorScaleLabelsCreateListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator+IOnMajorScaleLabelsCreateListenerImplementor, Syncfusion.SfChart.Android", "", this, new java.lang.Object[] {  });
	}


	public void onMajorScaleLabelsGenerated (java.util.ArrayList p0)
	{
		n_onMajorScaleLabelsGenerated (p0);
	}

	private native void n_onMajorScaleLabelsGenerated (java.util.ArrayList p0);

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
