package mono.com.syncfusion.rangenavigator;


public class SfDateTimeRangeNavigator_OnMinorScaleLabelsCreateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.rangenavigator.SfDateTimeRangeNavigator.OnMinorScaleLabelsCreateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMinorScaleLabelsGenerated:(Ljava/util/ArrayList;)V:GetOnMinorScaleLabelsGenerated_Ljava_util_ArrayList_Handler:Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator/IOnMinorScaleLabelsCreateListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator+IOnMinorScaleLabelsCreateListenerImplementor, Syncfusion.SfChart.Android", SfDateTimeRangeNavigator_OnMinorScaleLabelsCreateListenerImplementor.class, __md_methods);
	}


	public SfDateTimeRangeNavigator_OnMinorScaleLabelsCreateListenerImplementor ()
	{
		super ();
		if (getClass () == SfDateTimeRangeNavigator_OnMinorScaleLabelsCreateListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Rangenavigator.SfDateTimeRangeNavigator+IOnMinorScaleLabelsCreateListenerImplementor, Syncfusion.SfChart.Android", "", this, new java.lang.Object[] {  });
	}


	public void onMinorScaleLabelsGenerated (java.util.ArrayList p0)
	{
		n_onMinorScaleLabelsGenerated (p0);
	}

	private native void n_onMinorScaleLabelsGenerated (java.util.ArrayList p0);

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
