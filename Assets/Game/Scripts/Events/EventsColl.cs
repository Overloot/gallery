using System.Collections;
using System.Collections.Generic;

namespace TogoEvents
{
    public static class EventsCol
    {
        public static EventsType LoadScene = new EventsType(nameof(LoadScene));
        public static EventsType UnloadScene = new EventsType(nameof(UnloadScene));
    }
}
