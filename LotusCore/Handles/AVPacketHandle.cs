using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace AtomicAkarin.LotusCore.Handles
{
    public class AVPacketHandle : SafeHandleMinusOneIsInvalid
    {
        /// <summary>
        /// LIBRARY_API(AVPacket *) lotus_packet_alloc()
        /// </summary>
        [DllImport(ShimUtil.LibraryName, EntryPoint = "lotus_packet_alloc")]
        private static extern IntPtr LotusPacketAllocate();

        /// <summary>
        /// LIBRARY_API(void) lotus_packet_free(AVPacket *pkt)
        /// </summary>
        [DllImport(ShimUtil.LibraryName, EntryPoint = "lotus_packet_free")]
        private static extern void LotusPacketFree(IntPtr packet);

        public AVPacketHandle(IntPtr packet, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(packet);
        }

        public AVPacketHandle() : this(LotusPacketAllocate(), true)
        {
        }

        protected override bool ReleaseHandle()
        {
            LotusPacketFree(handle);
            return true;
        }
    }
}