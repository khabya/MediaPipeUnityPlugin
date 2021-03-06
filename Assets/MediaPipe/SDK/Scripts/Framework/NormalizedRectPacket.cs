using System;

namespace Mediapipe {
  public class NormalizedRectPacket : Packet<NormalizedRect> {
    public NormalizedRectPacket() : base() {}

    public override NormalizedRect GetValue() {
      var rectPtr = UnsafeNativeMethods.MpPacketGetNormalizedRect(ptr);
      var rect = SerializedProto.FromPtr<NormalizedRect>(rectPtr, NormalizedRect.Parser);

      UnsafeNativeMethods.MpSerializedProtoDestroy(rectPtr);

      return rect;
    }

    public override NormalizedRect ConsumeValue() {
      throw new NotSupportedException();
    }
  }
}
