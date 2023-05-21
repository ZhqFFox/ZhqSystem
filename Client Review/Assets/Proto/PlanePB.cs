// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: PlanePB.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
#region Messages
public sealed class Pos : pb::IMessage {
  private static readonly pb::MessageParser<Pos> _parser = new pb::MessageParser<Pos>(() => new Pos());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Pos> Parser { get { return _parser; } }

  /// <summary>Field number for the "x" field.</summary>
  public const int XFieldNumber = 1;
  private float x_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float X {
    get { return x_; }
    set {
      x_ = value;
    }
  }

  /// <summary>Field number for the "y" field.</summary>
  public const int YFieldNumber = 2;
  private float y_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Y {
    get { return y_; }
    set {
      y_ = value;
    }
  }

  /// <summary>Field number for the "z" field.</summary>
  public const int ZFieldNumber = 3;
  private float z_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Z {
    get { return z_; }
    set {
      z_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (X != 0F) {
      output.WriteRawTag(13);
      output.WriteFloat(X);
    }
    if (Y != 0F) {
      output.WriteRawTag(21);
      output.WriteFloat(Y);
    }
    if (Z != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(Z);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (X != 0F) {
      size += 1 + 4;
    }
    if (Y != 0F) {
      size += 1 + 4;
    }
    if (Z != 0F) {
      size += 1 + 4;
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 13: {
          X = input.ReadFloat();
          break;
        }
        case 21: {
          Y = input.ReadFloat();
          break;
        }
        case 29: {
          Z = input.ReadFloat();
          break;
        }
      }
    }
  }

}

public sealed class Rot : pb::IMessage {
  private static readonly pb::MessageParser<Rot> _parser = new pb::MessageParser<Rot>(() => new Rot());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Rot> Parser { get { return _parser; } }

  /// <summary>Field number for the "x" field.</summary>
  public const int XFieldNumber = 1;
  private float x_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float X {
    get { return x_; }
    set {
      x_ = value;
    }
  }

  /// <summary>Field number for the "y" field.</summary>
  public const int YFieldNumber = 2;
  private float y_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Y {
    get { return y_; }
    set {
      y_ = value;
    }
  }

  /// <summary>Field number for the "z" field.</summary>
  public const int ZFieldNumber = 3;
  private float z_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Z {
    get { return z_; }
    set {
      z_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (X != 0F) {
      output.WriteRawTag(13);
      output.WriteFloat(X);
    }
    if (Y != 0F) {
      output.WriteRawTag(21);
      output.WriteFloat(Y);
    }
    if (Z != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(Z);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (X != 0F) {
      size += 1 + 4;
    }
    if (Y != 0F) {
      size += 1 + 4;
    }
    if (Z != 0F) {
      size += 1 + 4;
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 13: {
          X = input.ReadFloat();
          break;
        }
        case 21: {
          Y = input.ReadFloat();
          break;
        }
        case 29: {
          Z = input.ReadFloat();
          break;
        }
      }
    }
  }

}

public sealed class S2C_SyncPlaneInfo : pb::IMessage {
  private static readonly pb::MessageParser<S2C_SyncPlaneInfo> _parser = new pb::MessageParser<S2C_SyncPlaneInfo>(() => new S2C_SyncPlaneInfo());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<S2C_SyncPlaneInfo> Parser { get { return _parser; } }

  /// <summary>Field number for the "pos" field.</summary>
  public const int PosFieldNumber = 1;
  private global::Pos pos_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Pos Pos {
    get { return pos_; }
    set {
      pos_ = value;
    }
  }

  /// <summary>Field number for the "rot" field.</summary>
  public const int RotFieldNumber = 2;
  private global::Rot rot_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Rot Rot {
    get { return rot_; }
    set {
      rot_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (pos_ != null) {
      output.WriteRawTag(10);
      output.WriteMessage(Pos);
    }
    if (rot_ != null) {
      output.WriteRawTag(18);
      output.WriteMessage(Rot);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (pos_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Pos);
    }
    if (rot_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Rot);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          if (pos_ == null) {
            pos_ = new global::Pos();
          }
          input.ReadMessage(pos_);
          break;
        }
        case 18: {
          if (rot_ == null) {
            rot_ = new global::Rot();
          }
          input.ReadMessage(rot_);
          break;
        }
      }
    }
  }

}

public sealed class C2S_OnConnected : pb::IMessage {
  private static readonly pb::MessageParser<C2S_OnConnected> _parser = new pb::MessageParser<C2S_OnConnected>(() => new C2S_OnConnected());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<C2S_OnConnected> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
      }
    }
  }

}

#endregion


#endregion Designer generated code
