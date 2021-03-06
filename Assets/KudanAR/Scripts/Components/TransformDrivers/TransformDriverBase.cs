using UnityEngine;
using System.Text;
using System.Collections;

namespace Kudan.AR
{
	/// <summary>
	/// The base transform driver that other transform drivers inherit from.
	/// </summary>
	public abstract class TransformDriverBase : MonoBehaviour
	{
		/// <summary>
		/// Reference to the base tracker class.
		/// </summary>
		protected TrackingMethodBase _trackerBase;

		/// <summary>
		/// Start this instance. Registers this trasnsform driver with its tracking method.
		/// </summary>
		public virtual void Start()
		{
			FindTracker();
			if (_trackerBase != null)
			{
				Register();
			}
		}

		/// <summary>
		/// Method called when the gameObject is destroyed. Unregisters this transform driver from its tracking method.
		/// </summary>
		public virtual void OnDestroy()
		{
			Unregister();
		}

		/// <summary>
		/// Update this transform driver.
		/// </summary>
		public virtual void Update()
		{
			if (_trackerBase == null)
			{
				FindTracker();
				if (_trackerBase != null)
				{
					Register();
				}
			}
		}

		/// <summary>
		/// Register this instance with the Tracking Method class for event handling.
		/// </summary>
		protected virtual void Register()
		{
		}

		/// <summary>
		/// Unregister this instance with the Tracking Method class for event handling.
		/// </summary>
		protected virtual void Unregister()
		{
		}

		/// <summary>
		/// Finds the tracker.
		/// </summary>
		protected abstract void FindTracker();
	}
};