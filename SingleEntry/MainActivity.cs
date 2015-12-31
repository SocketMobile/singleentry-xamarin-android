using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Timers;

using SocketMobile.ScanAPI;
using ScanApiHelper;

namespace SingleEntry
{
	[Activity (Label = "SingleEntry", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ScanApiHelper.ScanApiHelper.ScanApiHelperNotification
	{
		int count = 1;
		EditText decodedDataField;
		ScanApiHelper.ScanApiHelper _scanApi = new ScanApiHelper.ScanApiHelper(); // Different than iOS version!
		Timer _timer = new Timer();
		TextView status;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			decodedDataField = FindViewById<EditText> (Resource.Id.editDecodedData);
			status = FindViewById<TextView> (Resource.Id.textViewStatus);		
//			button.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);

			_scanApi.SetNotification(this);
			_scanApi.Open ();
			// we will take care of the UI thread issue in the called routine instead of here...
			//_timer.Elapsed+= (object sender, ElapsedEventArgs e) => InvokeOnMainThread (() => _scanApi.DoScanAPIReceive ());
			_timer.Elapsed+= (object sender, ElapsedEventArgs e) => _scanApi.DoScanAPIReceive ();
			_timer.Interval = 200;
			_timer.Start ();


//			};
		}

		/**
             * called each time a device connects to the host
             * @param result contains the result of the connection
             * @param newDevice contains the device information
             */
		public void OnDeviceArrival(long result, DeviceInfo newDevice)
		{
			RunOnUiThread (() => status.Text = newDevice.Name);  // Use RunOnUiThread for Android instead of InvokeOnMainThread()
		}

		/**
             * called each time a device disconnect from the host
             * @param deviceRemoved contains the device information
             */
		public void OnDeviceRemoval(DeviceInfo deviceRemoved)
		{
			RunOnUiThread (() => status.Text = "No Scanner Connected");  // Use RunOnUiThread for Android instead of InvokeOnMainThread()
		}

		/**
             * called each time ScanAPI is reporting an error
             * @param result contains the error code
             */
		public void OnError(long result, string errMsg)
		{
			status.Text = String.Format ("Error {0} : {1}", result, errMsg);
		}

		/**
             * called each time ScanAPI receives decoded data from scanner
             * @param deviceInfo contains the device information from which
             * the data has been decoded
             * @param decodedData contains the decoded data information
             */
		public void OnDecodedData(DeviceInfo device, ISktScanDecodedData decodedData)
		{
			// The following throws an exception - not sure why - something is null
			//RunOnUiThread (() => decodedDataField.Text = new string(decodedData.GetData ()));
			//
			// This seems to take care of the exception:
			string s = new string(decodedData.GetData());
			RunOnUiThread (() => decodedDataField.Text = s); // Use RunOnUiThread for Android instead of InvokeOnMainThread()
		}

		/**
             * called when ScanAPI initialization has been completed
             * @param result contains the initialization result
             */
		public void OnScanApiInitializeComplete(long result)
		{
			status.Text = "Waiting for Scanner";
		}

		/**
             * called when ScanAPI has been terminated. This will be
             * the last message received from ScanAPI
             */
		public void OnScanApiTerminated()
		{
			status.Text = "ScanAPI ready to be closed";
		}

		/**
             * called when an error occurs during the retrieval
             * of a ScanObject from ScanAPI.
             * @param result contains the retrieval error code
             */
		public void OnErrorRetrievingScanObject(long result)
		{
			status.Text = String.Format ("Error {0} while retrieving ScanObject", result);
		}
	}
}


