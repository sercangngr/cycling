using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Kabuk
{

	public static class ShareAndRate
	{
		const string iosAppId = "1382208971";
		const string androidPackage = "com.yush.android";

		public static void Rate()
		{
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + androidPackage);
#elif UNITY_IPHONE
			Application.OpenURL("itms-apps://itunes.apple.com/app/id" + iosAppId);
#endif

		}
	}
  

}
