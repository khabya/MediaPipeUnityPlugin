using System;

using MpCalculatorGraph = System.IntPtr;
using MpPacket = System.IntPtr;

namespace Mediapipe {
  public class CalculatorGraph : ResourceHandle {
    private bool _disposed = false;
    private CalculatorGraphConfig graphConfig;

    public CalculatorGraph(MpCalculatorGraph ptr) : base(ptr) {}

    public CalculatorGraph(string configText) : base(UnsafeNativeMethods.MpCalculatorGraphCreate()) {
      graphConfig = new CalculatorGraphConfig(configText);

      var status = Initialize(graphConfig);

      status.AssertOk();
    }

    protected override void Dispose(bool disposing) {
      if (_disposed) return;

      if (OwnsResource()) {
        UnsafeNativeMethods.MpCalculatorGraphDestroy(ptr);
      }

      ptr = IntPtr.Zero;
      graphConfig = null;

      _disposed = true;
    }

    public Status StartRun(SidePacket sidePacket) {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphStartRun(ptr, sidePacket.GetPtr()));
    }

    public Status WaitUntilDone() {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphWaitUntilDone(ptr));
    }

    public Status CloseInputStream(string name) {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphCloseInputStream(ptr, name));
    }

    private Status Initialize(CalculatorGraphConfig config) {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphInitialize(ptr, config.GetPtr()));
    }

    public StatusOrPoller<T> AddOutputStreamPoller<T>(string name) {
      return new StatusOrPoller<T>(UnsafeNativeMethods.MpCalculatorGraphAddOutputStreamPoller(ptr, name));
    }

    public Status AddPacketToInputStream<T>(string name, Packet<T> packet) {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphAddPacketToInputStream(ptr, name, packet.GetPtr()));
    }

    public GpuResources GetGpuResources() {
      return new GpuResources(UnsafeNativeMethods.MpCalculatorGraphGetGpuResources(ptr));
    }

    public Status SetGpuResources(GpuResources gpuResources) {
      return new Status(UnsafeNativeMethods.MpCalculatorGraphSetGpuResources(ptr, gpuResources.GetPtr()));
    }
  }
}
