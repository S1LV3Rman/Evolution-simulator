namespace Source
{
    public static class MapCoordExtension
    {
        public static MapCoord[] GetRhomb(this MapCoord centerCoord, int size)
        {
            var area = 4 * size * (size + 1) + 1;
            var coords = new MapCoord[area];

            var n = 0;
            
            for (var t = -size; t <= size; ++t)
            for (var b = -size; b <= size; ++b)
                coords[n++] = new MapCoord(t, b) + centerCoord;

            return coords;
        }
        
        public static MapCoord[] GetCircle(this MapCoord centerCoord, int radius)
        {
            var area = radius * 3 * (radius + 1) + 1;
            var coords = new MapCoord[area];

            var n = 0;
            
            for (var t =  -radius; t <= radius; ++t)
            {
                var min = t > 0 ? t - radius : -radius;
                var max = t > 0 ? radius : t + radius;
                for (var b = min; b <= max; ++b)
                {
                    var coord = new MapCoord(t, b) + centerCoord;
                    coords[n++] = coord;
                }
            }

            return coords;
        }
        
        public static void InvertOverEdge(this MapCoord[] coords, int edge)
        {
            foreach (var coord in coords)
                coord.InvertOverEdge(edge);
        }
    }
}