using System;
using System.Net.Sockets;

// ReSharper disable once CheckNamespace
namespace System
{
    internal static class SocketExtension
	{
		public static void SafeShutdownClose(this System.Net.Sockets.Socket socket)
		{
			try
			{
				try
				{
					socket.Shutdown(SocketShutdown.Both);
				}
				catch { }

				socket.Close();
			}
			catch (ObjectDisposedException)
			{
			}
		}
	}
}
