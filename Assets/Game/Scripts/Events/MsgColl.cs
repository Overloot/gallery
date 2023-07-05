using System.Collections;
using System.Collections.Generic;

namespace TogoEvents
{
    public static class MsgColl
    {
        public static MsgType LoadingScreen = new MsgType(nameof(LoadingScreen));
        public static MsgType Gallery = new MsgType(nameof(Gallery));
        public static MsgType FullScreenImage = new MsgType(nameof(FullScreenImage));
    }
}
