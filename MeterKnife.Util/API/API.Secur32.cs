// Copyright (C) 2010 OfficeSIP Communications
// This source is subject to the GNU General Public License.
// Please see Notice.txt for details.

using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace NKnife.API
{
	#region Enums

	public enum SecurityStatus : uint
	{
		SEC_E_OK = 0x00000000,
		SEC_E_INSUFFICIENT_MEMORY = 0x80090300,
		SEC_E_INVALID_HANDLE = 0x80090301,
		SEC_E_UNSUPPORTED_FUNCTION = 0x80090302,
		SEC_E_TARGET_UNKNOWN = 0x80090303,
		SEC_E_INTERNAL_ERROR = 0x80090304,
		SEC_E_SECPKG_NOT_FOUND = 0x80090305,
		SEC_E_NOT_OWNER = 0x80090306,
		SEC_E_CANNOT_INSTALL = 0x80090307,
		SEC_E_INVALID_TOKEN = 0x80090308,
		SEC_E_CANNOT_PACK = 0x80090309,
		SEC_E_QOP_NOT_SUPPORTED = 0x8009030A,
		SEC_E_NO_IMPERSONATION = 0x8009030B,
		SEC_E_LOGON_DENIED = 0x8009030C,
		SEC_E_UNKNOWN_CREDENTIALS = 0x8009030D,
		SEC_E_NO_CREDENTIALS = 0x8009030E,
		SEC_E_MESSAGE_ALTERED = 0x8009030F,
		SEC_E_OUT_OF_SEQUENCE = 0x80090310,
		SEC_E_NO_AUTHENTICATING_AUTHORITY = 0x80090311,
		SEC_I_CONTINUE_NEEDED = 0x00090312,
		SEC_I_COMPLETE_NEEDED = 0x00090313,
		SEC_I_COMPLETE_AND_CONTINUE = 0x00090314,
		SEC_I_LOCAL_LOGON = 0x00090315,
		SEC_E_BAD_PKGID = 0x80090316,
		SEC_E_CONTEXT_EXPIRED = 0x80090317,
		SEC_E_INCOMPLETE_MESSAGE = 0x80090318,
		SEC_E_INCOMPLETE_CREDENTIALS = 0x80090320,
		SEC_E_BUFFER_TOO_SMALL = 0x80090321,
		SEC_I_INCOMPLETE_CREDENTIALS = 0x00090320,
		SEC_I_RENEGOTIATE = 0x00090321,
		SEC_E_WRONG_PRINCIPAL = 0x80090322,
		SEC_E_ALGORITHM_MISMATCH = 0x80090331,
		SEC_E_ENCRYPT_FAILURE = 0x80090329,
		SEC_E_DECRYPT_FAILURE = 0x80090330,

		SEC_E_UNKNOW_ERROR = 0xFFFFFFFF,
	}

	public enum CredentialUse
	{
		SECPKG_CRED_INBOUND = 1,
		SECPKG_CRED_OUTBOUND = 2,
		SECPKG_CRED_BOTH = 3,
	}

	public enum TargetDataRep
	{
		SECURITY_NETWORK_DREP = 0x00000000,
		SECURITY_NATIVE_DREP = 0x00000010
	}

	public enum ContextReq
	{
		ASC_REQ_DELEGATE = 0x00000001,
		ASC_REQ_MUTUAL_AUTH = 0x00000002,
		ASC_REQ_REPLAY_DETECT = 0x00000004,
		ASC_REQ_SEQUENCE_DETECT = 0x00000008,
		ASC_REQ_CONFIDENTIALITY = 0x00000010,
		ASC_REQ_USE_SESSION_KEY = 0x00000020,
		ASC_REQ_ALLOCATE_MEMORY = 0x00000100,
		ASC_REQ_USE_DCE_STYLE = 0x00000200,
		ASC_REQ_DATAGRAM = 0x00000400,
		ASC_REQ_CONNECTION = 0x00000800,
		ASC_REQ_CALL_LEVEL = 0x00001000,
		ASC_REQ_EXTENDED_ERROR = 0x00008000,
		ASC_REQ_STREAM = 0x00010000,
		ASC_REQ_INTEGRITY = 0x00020000,
		ASC_REQ_LICENSING = 0x00040000,
		ASC_REQ_IDENTIFY = 0x00080000,
		ASC_REQ_ALLOW_NULL_SESSION = 0x00100000,
		ASC_REQ_ALLOW_NON_USER_LOGONS = 0x00200000,
		ASC_REQ_ALLOW_CONTEXT_REPLAY = 0x00400000,
		ASC_REQ_FRAGMENT_TO_FIT = 0x00800000,
		ASC_REQ_FRAGMENT_SUPPLIED = 0x00002000,
		ASC_REQ_NO_TOKEN = 0x01000000,
	}

	public enum ContextAttr
	{
		ASC_RET_DELEGATE = 0x00000001,
		ASC_RET_MUTUAL_AUTH = 0x00000002,
		ASC_RET_REPLAY_DETECT = 0x00000004,
		ASC_RET_SEQUENCE_DETECT = 0x00000008,
		ASC_RET_CONFIDENTIALITY = 0x00000010,
		ASC_RET_USE_SESSION_KEY = 0x00000020,
		ASC_RET_ALLOCATED_MEMORY = 0x00000100,
		ASC_RET_USED_DCE_STYLE = 0x00000200,
		ASC_RET_DATAGRAM = 0x00000400,
		ASC_RET_CONNECTION = 0x00000800,
		ASC_RET_CALL_LEVEL = 0x00002000,
		ASC_RET_THIRD_LEG_FAILED = 0x00004000,
		ASC_RET_EXTENDED_ERROR = 0x00008000,
		ASC_RET_STREAM = 0x00010000,
		ASC_RET_INTEGRITY = 0x00020000,
		ASC_RET_LICENSING = 0x00040000,
		ASC_RET_IDENTIFY = 0x00080000,
		ASC_RET_NULL_SESSION = 0x00100000,
		ASC_RET_ALLOW_NON_USER_LOGONS = 0x00200000,
		ASC_RET_FRAGMENT_ONLY = 0x00800000,
		ASC_RET_NO_TOKEN = 0x01000000,
	}

	public enum BufferType
	{
		SECBUFFER_VERSION = 0,

		SECBUFFER_EMPTY = 0,				// Undefined, replaced by provider
		SECBUFFER_DATA = 1,					// Packet data
		SECBUFFER_TOKEN = 2,				// Security token
		SECBUFFER_PKG_PARAMS = 3,			// Package specific parameters
		SECBUFFER_MISSING = 4,				// Missing Data indicator
		SECBUFFER_EXTRA = 5,				// Extra data
		SECBUFFER_STREAM_TRAILER = 6,		// Security Trailer
		SECBUFFER_STREAM_HEADER = 7,		// Security Header
		SECBUFFER_NEGOTIATION_INFO = 8,		// Hints from the negotiation pkg
		SECBUFFER_PADDING = 9,				// non-data padding
		SECBUFFER_STREAM = 10,				// whole encrypted message
		SECBUFFER_MECHLIST = 11,
		SECBUFFER_MECHLIST_SIGNATURE = 12,
		SECBUFFER_TARGET = 13,				// obsolete
		SECBUFFER_CHANNEL_BINDINGS = 14,
		SECBUFFER_CHANGE_PASS_RESPONSE = 15,
	}

	public enum UlAttribute
	{
		SECPKG_ATTR_SIZES = 0,
		SECPKG_ATTR_NAMES = 1,
		SECPKG_ATTR_LIFESPAN = 2,
		SECPKG_ATTR_DCE_INFO = 3,
		SECPKG_ATTR_STREAM_SIZES = 4,
		SECPKG_ATTR_KEY_INFO = 5,
		SECPKG_ATTR_AUTHORITY = 6,
		SECPKG_ATTR_PROTO_INFO = 7,
		SECPKG_ATTR_PASSWORD_EXPIRY = 8,
		SECPKG_ATTR_SESSION_KEY = 9,
		SECPKG_ATTR_PACKAGE_INFO = 10,
		SECPKG_ATTR_USER_FLAGS = 11,
		SECPKG_ATTR_NEGOTIATION_INFO = 12,
		SECPKG_ATTR_NATIVE_NAMES = 13,
		SECPKG_ATTR_FLAGS = 14,
		// These attributes exist only in Win XP and greater
		SECPKG_ATTR_USE_VALIDATED = 15,
		SECPKG_ATTR_CREDENTIAL_NAME = 16,
		SECPKG_ATTR_TARGET_INFORMATION = 17,
		SECPKG_ATTR_ACCESS_TOKEN = 18,
		// These attributes exist only in Win2K3 and greater
		SECPKG_ATTR_TARGET = 19,
		SECPKG_ATTR_AUTHENTICATION_ID = 20,
		// These attributes exist only in Win2K3SP1 and greater
		SECPKG_ATTR_LOGOFF_TIME = 21,
	}

	public enum SchProtocols
	{
		ClientMask = -2147483478,
		Pct = 3,
		PctClient = 2,
		PctServer = 1,
		ServerMask = 0x40000055,
		Ssl2 = 12,
		Ssl2Client = 8,
		Ssl2Server = 4,
		Ssl3 = 0x30,
		Ssl3Client = 0x20,
		Ssl3Server = 0x10,
		Ssl3Tls = 240,
		Tls = 0xc0,
		TlsClient = 0x80,
		TlsServer = 0x40,
		UniClient = -2147483648,
		Unified = -1073741824,
		UniServer = 0x40000000,
		Zero = 0
	}

	#endregion

	#region Structs

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SchannelCred
	{
		public const int CurrentVersion = 4;
		public int version;
		public int cCreds;
		public IntPtr paCreds1;
		private readonly IntPtr rootStore;
		public int cMappers;
		private readonly IntPtr phMappers;
		public int cSupportedAlgs;
		private readonly IntPtr palgSupportedAlgs;
		public SchProtocols grbitEnabledProtocols;
		public int dwMinimumCipherStrength;
		public int dwMaximumCipherStrength;
		public int dwSessionLifespan;
		public Flags dwFlags;
		public int reserved;

		public SchannelCred(X509Certificate certificate, SchProtocols protocols)
			: this(CurrentVersion, certificate, SchannelCred.Flags.Zero, protocols)
		{
		}

		public SchannelCred(int version1, X509Certificate certificate, Flags flags, SchProtocols protocols)
		{
			paCreds1 = IntPtr.Zero;
			rootStore = phMappers = palgSupportedAlgs = IntPtr.Zero;
			cCreds = cMappers = cSupportedAlgs = 0;
			dwMinimumCipherStrength = dwMaximumCipherStrength = 0;
			dwSessionLifespan = reserved = 0;
			version = version1;
			dwFlags = flags;
			grbitEnabledProtocols = protocols;
			if (certificate != null)
			{
				paCreds1 = certificate.Handle;
				cCreds = 1;
			}
		}

		[Flags]
		public enum Flags
		{
			NoDefaultCred = 0x10,
			NoNameCheck = 4,
			NoSystemMapper = 2,
			ValidateAuto = 0x20,
			ValidateManual = 8,
			Zero = 0
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgInfo
	{
		public int fCapabilities;
		public short wVersion;
		public short wRPCID;
		public int cbMaxToken;
		IntPtr Name;
		IntPtr Comment;

		public string GetName()
		{
			return (Name != IntPtr.Zero) ? Marshal.PtrToStringAnsi(Name) : null;
		}

		public string GetComment()
		{
			return (Comment != IntPtr.Zero) ? Marshal.PtrToStringAnsi(Comment) : null;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SecHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CredHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CtxtHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct SecBuffer
	{
		internal int cbBuffer;
		internal int BufferType;
		internal IntPtr pvBuffer;

		public SecBuffer(BufferType type, int count, IntPtr buffer)
		{
			BufferType = (int)type;
			cbBuffer = count;
			pvBuffer = buffer;
		}
	}

	public struct SecBufferEx
	{
		public BufferType BufferType;
		public int Size;
		public int Offset;
		public object Buffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SecBufferDesc
	{
		internal int ulVersion;
		internal int cBuffers;
		internal IntPtr pBuffers;
	}

	public unsafe class SecBufferDescEx
	{
		internal SecBufferDesc SecBufferDesc;
		internal SecBuffer[] SecBuffers;

		private GCHandle[] Handles;
		private GCHandle DescHandle;

		public SecBufferEx[] Buffers;

		public SecBufferDescEx(SecBufferEx[] buffers)
		{
			SecBufferDesc.ulVersion = (int)BufferType.SECBUFFER_VERSION;
			SecBufferDesc.cBuffers = 0;
			SecBufferDesc.pBuffers = IntPtr.Zero;
			Handles = null;
			SecBuffers = null;
			Buffers = buffers;
		}

		public int GetBufferIndex(BufferType type, int from)
		{
			for (int i = from; i < Buffers.Length; i++)
				if (Buffers[i].BufferType == type)
					return i;
			return -1;
		}

		internal void Pin()
		{
			if (SecBuffers == null || SecBuffers.Length != Buffers.Length)
			{
				SecBuffers = new SecBuffer[Buffers.Length];
				Handles = new GCHandle[Buffers.Length];
			}

			for (int i = 0; i < Buffers.Length; i++)
			{
				if (Buffers[i].Buffer != null)
					Handles[i] = GCHandle.Alloc(Buffers[i].Buffer, GCHandleType.Pinned);

				SecBuffers[i].BufferType = (int)Buffers[i].BufferType;
				SecBuffers[i].cbBuffer = Buffers[i].Size;

				if (Buffers[i].Buffer == null)
					SecBuffers[i].pvBuffer = IntPtr.Zero;
				else
					SecBuffers[i].pvBuffer = AddToPtr(Handles[i].AddrOfPinnedObject(), Buffers[i].Offset);
			}

			DescHandle = GCHandle.Alloc(SecBuffers, GCHandleType.Pinned);

			SecBufferDesc.ulVersion = (int)BufferType.SECBUFFER_VERSION;
			SecBufferDesc.cBuffers = SecBuffers.Length;
			SecBufferDesc.pBuffers = DescHandle.AddrOfPinnedObject();
		}

		internal void Free()
		{
			object buffer = Buffers[0].Buffer;
			IntPtr bufferPtr = Handles[0].AddrOfPinnedObject();

			for (int i = 0; i < Buffers.Length; i++)
			{
				Buffers[i].BufferType = (BufferType)SecBuffers[i].BufferType;
				Buffers[i].Size = SecBuffers[i].cbBuffer;

				if (Buffers[i].Size == 0 || Buffers[i].BufferType == BufferType.SECBUFFER_EMPTY)
				{
					Buffers[i].Buffer = null;
					Buffers[i].Offset = 0;
				}
				else
				{
					Buffers[i].Buffer = buffer;
					if (SecBuffers[i].pvBuffer != IntPtr.Zero)
						Buffers[i].Offset = SubPtr(bufferPtr, SecBuffers[i].pvBuffer);
					else
					{
						// FIX: AcceptSecurityContext returns zero pointer for extra data
						// It looks like AcceptSecurityContext do not ready for extra data
						// if (i == 1 && Buffers.Length == 2)
						// {
						//     Buffers[i].Offset = Buffers[i - 1].Offset
						//         + Buffers[i - 1].Size - Buffers[i].Size;
						// }
						// else
						// throw new InvalidOperationException("I do not know ho to fix SSPI WinApi return data");
					}
				}
			}

			for (int i = 0; i < Buffers.Length; i++)
			{
				if (Handles[i].IsAllocated)
					Handles[i].Free();
			}

			DescHandle.Free();
		}

		private int SubPtr(IntPtr begin, IntPtr current)
		{
			return (int)((long)current - (long)begin);
		}

		private IntPtr AddToPtr(IntPtr begin, int offset)
		{
			return (IntPtr)((long)begin + offset);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgContext_Sizes
	{
		public int cbMaxToken;
		public int cbMaxSignature;
		public int cbBlockSize;
		public int cbSecurityTrailer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgContext_Names
	{
		public IntPtr sUserName;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SecPkgContext_StreamSizes
	{
		public int cbHeader;
		public int cbTrailer;
		public int cbMaximumMessage;
		public int cBuffers;
		public int cbBlockSize;
	};

	#endregion

	#region SafeHandles

	[SuppressUnmanagedCodeSecurity]
	public class SafeCredHandle : SafeHandle
	{
		internal CredHandle Handle;

		public SafeCredHandle(CredHandle credHandle)
			: base(IntPtr.Zero, true)
		{
			this.Handle = credHandle;
		}

		public override bool IsInvalid
		{
			get { return Handle.IsInvalid; }
		}

		protected override bool ReleaseHandle()
		{
			return Secur32Dll.FreeCredentialsHandle(ref Handle) == 0;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	public class SafeCtxtHandle : SafeHandle
	{
		internal CtxtHandle Handle;

		public SafeCtxtHandle()
			: base(IntPtr.Zero, true)
		{
		}

		public SafeCtxtHandle(CtxtHandle ctxtHandle)
			: base(IntPtr.Zero, true)
		{
			this.Handle = ctxtHandle;
		}

		public override bool IsInvalid
		{
			get { return Handle.IsInvalid; }
		}

		protected override bool ReleaseHandle()
		{
			return Secur32Dll.DeleteSecurityContext(ref Handle) == 0;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	public class SafeContextBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		public SafeContextBufferHandle()
			: base(true)
		{
		}

		public SafeContextBufferHandle(IntPtr handle)
			: base(true)
		{
			SetHandle(handle);
		}

		override protected bool ReleaseHandle()
		{
			return Secur32Dll.FreeContextBuffer(handle) == 0;
		}

		public SecPkgInfo GetItem<T>(int index)
		{
			var address = (IntPtr)(DangerousGetHandle().ToInt64()
				+ Marshal.SizeOf(typeof(T)) * index);

			return (SecPkgInfo)Marshal.PtrToStructure(address, typeof(T));
		}
	}

	#endregion

    [Serializable]
	class SspiException : Win32Exception
	{
		public SspiException(int error, string function)
			: base(error, string.Format(@"SSPI error, function call {0} return 0x{1:x8}", function, error))
		{
		}

		public SecurityStatus SecurityStatus
		{
			get { return Sspi.Convert(base.ErrorCode); }
		}
	}

	static class Sspi
	{
		public static bool Succeeded(SecurityStatus result)
		{
			return (int)result >= 0;
		}

		public static bool Failed(SecurityStatus result)
		{
			return (int)result < 0;
		}

		internal static bool Succeeded(int result)
		{
			return result >= 0;
		}

		internal static bool Failed(int result)
		{
			return result < 0;
		}

		public static SecurityStatus EnumerateSecurityPackages(out int packages, out SafeContextBufferHandle secPkgInfos)
		{
			return Convert(
				Secur32Dll.EnumerateSecurityPackagesA(out packages, out secPkgInfos));
		}

		public static unsafe void AcquireCredentialsHandle(
			CredentialUse credentialUse,
			SchannelCred authData,
			out SafeCredHandle credential,
			out long expiry)
		{
			CredHandle handle;
			GCHandle paCredHandle = new GCHandle();
			IntPtr[] paCred = null;

			if (authData.cCreds > 0)
			{
				paCred = new IntPtr[] { authData.paCreds1 };
				paCredHandle = GCHandle.Alloc(paCred, GCHandleType.Pinned);
				authData.paCreds1 = paCredHandle.AddrOfPinnedObject();
			}

			try
			{
				int error = Secur32Dll.AcquireCredentialsHandleA(
					null,
					Secur32Dll.UNISP_NAME,
					(int)credentialUse,
					null,
					&authData,
					null,
					null,
					out handle,
					out expiry);

				credential = new SafeCredHandle(handle);

				if (error != 0)
					throw new SspiException(error, @"AcquireCredentialsHandleA");
			}
			finally
			{
				if (paCredHandle.IsAllocated)
					paCredHandle.Free();

				if (paCred != null)
					authData.paCreds1 = paCred[0];
			}
		}

		public static unsafe SecurityStatus SafeAcceptSecurityContext(
			ref SafeCredHandle credential,
			ref SafeCtxtHandle context,
			ref SecBufferDescEx input,
			int contextReq,
			TargetDataRep targetDataRep,
			ref SafeCtxtHandle newContext,
			ref SecBufferDescEx output,
			out int contextAttr,
			out long timeStamp)
		{
			try
			{
				input.Pin();
				output.Pin();

				fixed (void* fixedContext = &context.Handle)
				{
					int error = Secur32Dll.AcceptSecurityContext(
						ref credential.Handle,
						(context.IsInvalid) ? null : fixedContext,
						ref input.SecBufferDesc,
						contextReq,
						(int)targetDataRep,
						ref newContext.Handle,
						ref output.SecBufferDesc,
						out contextAttr,
						out timeStamp);

					return Convert(error);
				}
			}
			catch
			{
				contextAttr = 0;
				timeStamp = 0;
				return SecurityStatus.SEC_E_UNKNOW_ERROR;
			}
			finally
			{
				input.Free();
				output.Free();
			}
		}

		public unsafe static SecurityStatus SafeDecryptMessage(
			ref SafeCtxtHandle context,
			ref SecBufferDescEx message,
			uint MessageSeqNo,
			void* pfQOP)
		{
			try
			{
				message.Pin();

				int error = Secur32Dll.DecryptMessage(
					ref context.Handle,
					ref message.SecBufferDesc,
					MessageSeqNo,
					pfQOP);

				return Convert(error);
			}
			catch
			{
				return SecurityStatus.SEC_E_UNKNOW_ERROR;
			}
			finally
			{
				message.Free();
			}
		}

		public unsafe static void EncryptMessage(
			ref SafeCtxtHandle context,
			ref SecBufferDescEx message,
			uint MessageSeqNo,
			void* pfQOP)
		{
			try
			{
				message.Pin();

				int error = Secur32Dll.EncryptMessage(
					ref context.Handle,
					pfQOP,
					ref message.SecBufferDesc,
					MessageSeqNo);

				if (error != 0)
					throw new SspiException(error, @"EncryptMessage");
			}
			finally
			{
				message.Free();
			}
		}

		public unsafe static void QueryContextAttributes(
			ref SafeCtxtHandle context,
			out SecPkgContext_StreamSizes streamSizes)
		{
			fixed (void* buffer = &streamSizes)
				QueryContextAttributes(ref context, UlAttribute.SECPKG_ATTR_STREAM_SIZES, buffer);
		}

		public unsafe static void QueryContextAttributes(
			ref SafeCtxtHandle context,
			UlAttribute attribute,
			void* buffer)
		{
			int error = Secur32Dll.QueryContextAttributesA(
				ref context.Handle,
				(uint)attribute,
				buffer);

			if (error != 0)
				throw new SspiException(error, @"QueryContextAttributesA");
		}

		public unsafe static SecurityStatus SafeQueryContextAttributes(
			ref SafeCtxtHandle context,
			out SecPkgContext_StreamSizes streamSizes)
		{
			fixed (void* buffer = &streamSizes)
				return SafeQueryContextAttributes(ref context, UlAttribute.SECPKG_ATTR_STREAM_SIZES, buffer);
		}

		public unsafe static SecurityStatus SafeQueryContextAttributes(
			ref SafeCtxtHandle context,
			UlAttribute attribute,
			void* buffer)
		{
			try
			{
				int error = Secur32Dll.QueryContextAttributesA(
					ref context.Handle,
					(uint)attribute,
					buffer);

				return Convert(error);
			}
			catch
			{
				return SecurityStatus.SEC_E_UNKNOW_ERROR;
			}
		}

		public static SecurityStatus Convert(int error)
		{
			if (Enum.IsDefined(typeof(SecurityStatus), (uint)error))
				return (SecurityStatus)error;
			return SecurityStatus.SEC_E_UNKNOW_ERROR;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	static class Secur32Dll
	{
		private const string SECUR32 = @"secur32.dll";
		public const string UNISP_NAME = "Microsoft Unified Security Protocol Provider";

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int FreeContextBuffer(
			[In] IntPtr pvContextBuffer);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int FreeCredentialsHandle(
			[In] ref CredHandle phCredential);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		public static extern int EnumerateSecurityPackagesA(
			[Out] out int pcPackages,
			[Out] out SafeContextBufferHandle ppPackageInfo);

		[DllImport(SECUR32, ExactSpelling = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true, SetLastError = true)]
		public unsafe static extern int AcquireCredentialsHandleA(
			[In, MarshalAs(UnmanagedType.LPStr)] string pszPrincipal,
			[In, MarshalAs(UnmanagedType.LPStr)] string pszPackage,
			[In] int fCredentialUse,
			[In] void* pvLogonID,
			[In] void* pAuthData,
			[In] void* pGetKeyFn,
			[In] void* pvGetKeyArgument,
			[Out] out CredHandle phCredential,
			[Out] out long ptsExpiry);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int AcceptSecurityContext(
			[In] ref CredHandle phCredential,
			[In, Out] void* phContext,
			[In] ref SecBufferDesc pInput,
			[In] int fContextReq,
			[In] int TargetDataRep,
			[In, Out] ref CtxtHandle phNewContext,
			[In, Out] ref SecBufferDesc pOutput,
			[Out] out int pfContextAttr,
			[Out] out long ptsTimeStamp);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int DeleteSecurityContext(
			[In] ref CtxtHandle phContext);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int QueryContextAttributesA(
			[In] ref CtxtHandle phContext,
			[In] uint ulAttribute,
			[Out] void* pBuffer);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int MakeSignature(
			[In] ref SecHandle phContext,
			[In] int fQOP,
			[In, Out] ref SecBufferDesc pMessage,
			[In] int MessageSeqNo);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int VerifySignature(
			[In] ref SecHandle phContext,
			[In] ref SecBufferDesc pMessage,
			[In] int MessageSeqNo,
			[Out] out int pfQOP);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int DecryptMessage(
			[In] ref CtxtHandle phContext,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint MessageSeqNo,
			[Out] out uint pfQOP);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int DecryptMessage(
			[In] ref CtxtHandle phContext,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint MessageSeqNo,
			[Out] void* pfQOP);

		[DllImport(SECUR32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int EncryptMessage(
			[In] ref CtxtHandle phContext,
			[Out] void* pfQOP,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint MessageSeqNo);
	}
}
