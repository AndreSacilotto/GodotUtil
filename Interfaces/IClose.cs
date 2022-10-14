using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>Used to dispose things that do not create memory leaks, but memory holding</summary>
public interface IClose
{
	/// <summary>Close and/or free memory holders</summary>
	void Close();
}
