using ProtoBuf;

namespace DataTable
{
    [ProtoContract]
    public class Vector2
    {
        [ProtoMember(1)]
        public float x { get; set; }
        [ProtoMember(2)]
        public float y { get; set; }
    }

    [ProtoContract]
    public class Vector3
    {
        [ProtoMember(1)]
        public float x { get; set; }
        [ProtoMember(2)]
        public float y { get; set; }
        [ProtoMember(3)]
        public float z { get; set; }
    }

    [ProtoContract]
    public class Color
    {
        [ProtoMember(1)]
        public float r { get; set; }
        [ProtoMember(2)]
        public float g { get; set; }
        [ProtoMember(3)]
        public float b { get; set; }
        [ProtoMember(4)]
        public float a { get; set; }
    }
}
