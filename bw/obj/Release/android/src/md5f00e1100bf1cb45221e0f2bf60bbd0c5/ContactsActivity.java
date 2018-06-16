package md5f00e1100bf1cb45221e0f2bf60bbd0c5;


public class ContactsActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("bw.ContactsActivity, bw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ContactsActivity.class, __md_methods);
	}


	public ContactsActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ContactsActivity.class)
			mono.android.TypeManager.Activate ("bw.ContactsActivity, bw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
