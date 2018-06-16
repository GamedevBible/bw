package md5f00e1100bf1cb45221e0f2bf60bbd0c5;


public class ThanksFragment
	extends android.support.v7.app.AppCompatDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("bw.ThanksFragment, bw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ThanksFragment.class, __md_methods);
	}


	public ThanksFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ThanksFragment.class)
			mono.android.TypeManager.Activate ("bw.ThanksFragment, bw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

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
