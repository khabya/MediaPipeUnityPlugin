using System;

using GpuBufferPtr = System.IntPtr;

namespace Mediapipe {
  public class GpuBuffer : ResourceHandle {
    private bool _disposed = false;
 
    public GpuBuffer(GpuBufferPtr ptr, bool isOwner = true) : base(ptr, isOwner) {}

    // TODO: investigate glTextureBuffer's state after initialization
    public GpuBuffer(GlTextureBuffer glTextureBuffer) : base(UnsafeNativeMethods.MpGpuBufferCreate(glTextureBuffer.GetPtr())) {}

    protected override void Dispose(bool disposing) {
      if (_disposed) return;

      if (OwnsResource()) {
        UnsafeNativeMethods.MpGpuBufferDestroy(ptr);
      }

      ptr = IntPtr.Zero;

      _disposed = true;
    }

    public GpuBufferFormat Format() {
      return (GpuBufferFormat)UnsafeNativeMethods.MpGpuBufferFormat(ptr);
    }

    public int Width() {
      return UnsafeNativeMethods.MpGpuBufferWidth(ptr);
    }

    public int Height() {
      return UnsafeNativeMethods.MpGpuBufferHeight(ptr);
    }
  }
}
